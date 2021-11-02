using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;       //进行控制的Mixer变量

    public void SetMasterVolume(Slider s)   //控制主音量的函数
    {
        audioMixer.SetFloat("MasterVolume", s.value); 
        // MasterVolue为我们暴露出来的Master的参数
    }

    public void SetMusicVolume(Slider s)   //控制背景音乐音量的函数
    {
        audioMixer.SetFloat("MusicVolume", s.value);
        // MasterVolue为我们暴露出来的Music的参数
    }

    public void SetSoundEffectVolume(Slider s)   //控制音效音量的函数
    {
        audioMixer.SetFloat("SoundEffectVolume", s.value);
        // MasterVolue为我们暴露出来的SoundEffect的参数
    }
}
