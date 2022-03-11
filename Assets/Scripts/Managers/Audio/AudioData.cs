using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Managers.Audio
{
    [Serializable]
    public class AudioData
    {
        [Header("tag")]
        public string tag;
        [Header("音乐源文件")]
        public AudioClip audio;
        [Header("输出组")]
        public AudioMixerGroup OutPutGroup;
        [Header("是否开启自动播放")]
        public bool PlayOnAwake;
        [Header("是否循环")]
        public bool loop;
        [Header("音量")]
        [Range(0,1)]
        public float Volume;
        public AudioData(string tag, AudioClip audio, AudioMixerGroup outPutGroup = null, bool playOnAwake = true, bool loop = false, float volume = 1)
        {
            this.tag = tag;
            this.audio = audio;
            OutPutGroup = outPutGroup;
            PlayOnAwake = playOnAwake;
            this.loop = loop;
            Volume = volume;
        }
    }
}