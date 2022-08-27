using UnityEngine;

namespace MiniGame.Bar.Datas
{
    [System.Serializable]
    public class AudioData{
        public string name;
        public AudioClip clip;
    }
    [CreateAssetMenu(fileName = " AudioAsset", menuName = "MiniGame/Bar/AudioAsset")]
    public class AudioAsset:ScriptableObject
    {
        public AudioData[] clips;
    }
}
