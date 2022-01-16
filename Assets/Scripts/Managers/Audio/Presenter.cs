using System.Collections.Generic;
using UnityEngine;

namespace Managers.Audio
{
    public class Presenter
    {
        public List<Data> audioList;
        private List<string> tagList;
        private Dictionary<string, AudioSource> sourceList;
        private GameObject gameObject;
        public Presenter(List<Data> audioList, List<string> tagList, Dictionary<string, AudioSource> sourceList, GameObject gameObject)
        {
            this.audioList = audioList;
            this.tagList = tagList;
            this.sourceList = sourceList;
            this.gameObject = gameObject;
        }

        public bool turnOnAudio(string tag)
        {
            if (!tagList.Contains(tag))
            {
                return false;
            }
            if (sourceList.ContainsKey(tag))
            {
                return true;
            }
            int index = tagList.IndexOf(tag);
            Data data = audioList[index];
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            sourceList.Add(tag, audioSource);
            audioSource.clip = data.audio;
            audioSource.outputAudioMixerGroup = data.OutPutGroup;
            audioSource.playOnAwake = data.PlayOnAwake;
            audioSource.loop = data.loop;
            audioSource.volume = data.Volume;
            audioSource.enabled = true;
            if (data.PlayOnAwake)
            {
                audioSource.Play();
            }
            return true;
        }

        public bool turnOffAudio(string tag)
        {
            if (!tagList.Contains(tag))
            {
                return false;
            }
            if (!sourceList.ContainsKey(tag))
            {
                return true;
            }
            AudioSource audioSource = sourceList[tag];
            audioSource.Pause();
            audioSource.enabled = false;
            sourceList.Remove(tag);
            Component.Destroy(audioSource);
            return true;
        }
    }
}