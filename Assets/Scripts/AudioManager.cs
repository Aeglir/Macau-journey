using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;       //���п��Ƶ�Mixer����

    public void SetMasterVolume(Slider s)   //�����������ĺ���
    {
        audioMixer.SetFloat("MasterVolume", s.value); 
        // MasterVolueΪ���Ǳ�¶������Master�Ĳ���
    }

    public void SetMusicVolume(Slider s)   //���Ʊ������������ĺ���
    {
        audioMixer.SetFloat("MusicVolume", s.value);
        // MasterVolueΪ���Ǳ�¶������Music�Ĳ���
    }

    public void SetSoundEffectVolume(Slider s)   //������Ч�����ĺ���
    {
        audioMixer.SetFloat("SoundEffectVolume", s.value);
        // MasterVolueΪ���Ǳ�¶������SoundEffect�Ĳ���
    }
}
