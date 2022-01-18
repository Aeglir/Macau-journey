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
                AudioPresenter presenter = new AudioPresenter(audioList, tagList, sourceList, gameObject);
                presenter.turnOnAudio("op");
            }
        }
        /// <summary>
        /// 获取音频控制器
        /// </summary>
        /// <returns>音频控制器</returns>
        public AudioPresenter GetPresenter()
        {
            return new AudioPresenter(audioList, tagList, sourceList, gameObject);
        }
    }
}
