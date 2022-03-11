using System.Collections.Generic;
using Managers.Audio;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public List<AudioData> audioList;
        #region fields
        private List<string> tagList;
        private Dictionary<string, AudioSource> sourceList;
        #endregion
        #region properties
        public List<string> TagList { get => tagList; }
        public Dictionary<string, AudioSource> SourceList { get => sourceList; }
        public AudioPresenter presenter { get => new AudioPresenter(audioList, tagList, sourceList, gameObject); }
        #endregion
        #region private methods
        private void Awake()
        {
            tagList = new List<string>();
            sourceList = new Dictionary<string, AudioSource>();
            foreach (AudioData data in audioList)
            {
                tagList.Add(data.tag);
            }
            AudioPresenter presenter = new AudioPresenter(audioList, tagList, sourceList, gameObject);
            presenter.turnOnAudio("op");
        }
        #endregion
    }
}
