using System.Collections.Generic;
using UnityEngine;

namespace Managers.Audio
{
    public class AudioPresenter
    {
        #region private fields
        private List<AudioData> audioList;
        private List<string> tagList;
        private Dictionary<string, AudioSource> sourceList;
        private GameObject gameObject;
        #endregion
        public AudioPresenter(List<AudioData> audioList, List<string> tagList, Dictionary<string, AudioSource> sourceList, GameObject gameObject)
        {
            this.audioList = audioList;
            this.tagList = tagList;
            this.sourceList = sourceList;
            this.gameObject = gameObject;
        }
        /// <summary>
        /// 开启音频播放
        /// </summary>
        /// <param name="tag">待开启音频tag</param>
        /// <returns>是否开启成功</returns>
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
            AudioData data = audioList[index];
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
        /// <summary>
        /// 关闭音频播放
        /// </summary>
        /// <param name="tag">待关闭音频tag</param>
        /// <returns>是否关闭成功</returns>
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