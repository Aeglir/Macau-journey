using UnityEngine;
using UnityEngine.InputSystem;

public class SceneAudioManager : MonoBehaviour, AudioManagable
{
    public System.Collections.Generic.Dictionary<string, AudioSource> audioSource;
    public System.Collections.Generic.Dictionary<string, AudioClip> asset;
    private void Awake()
    {
        audioSource = new System.Collections.Generic.Dictionary<string, AudioSource>();
    }
    public void Paues(string clip)
    {
        if (audioSource.ContainsKey(clip))
            audioSource[clip].Pause();
    }
    public void Play(string clip)
    {
        if (!audioSource.ContainsKey(clip))
        {
            audioSource[clip] = gameObject.AddComponent<AudioSource>();
            audioSource[clip].clip = asset[clip];
        }
        audioSource[clip].Play();
    }
    public void Stop(string clip)
    {
        if (audioSource.ContainsKey(clip))
            audioSource[clip].Stop();
    }
    public void SetBack(string clip)
    {
        if (!audioSource.ContainsKey(clip))
        {
            audioSource[clip] = gameObject.AddComponent<AudioSource>();
            audioSource[clip].clip = asset[clip];
            audioSource[clip].loop = true;
        }
        audioSource[clip].Play();
    }
    public void Clear()
    {
        foreach (var a in audioSource)
        {
            a.Value.Stop();
            audioSource.Remove(a.Key);
            Destroy(a.Value);
        }
    }
}
