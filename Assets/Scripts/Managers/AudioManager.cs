using System.Collections.Generic;
using Managers.Audio;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        #region 单例
        private AudioManager instance;
        public AudioManager Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion
        public List<Data> audioList;
        private List<string> tagList;
        private Dictionary<string, AudioSource> sourceList;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                tagList = new List<string>();
                sourceList = new Dictionary<string, AudioSource>();
                foreach (Data data in audioList)
                {
                    tagList.Add(data.tag);
                }
                Presenter presenter = new Presenter(audioList, tagList, sourceList, gameObject);
                presenter.turnOnAudio("op");
            }
        }

        public Presenter GetPresenter()
        {
            return new Presenter(audioList, tagList, sourceList, gameObject);
        }
    }
}
