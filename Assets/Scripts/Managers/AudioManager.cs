using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        #region c# properties
        //对象单例化
        private static AudioManager instance = null;
        public static AudioManager Instance
        {
            get => instance;
        }
        #endregion
        [SerializeField]
        private AudioSource audioSource;
        private void Awake()
        {
            if (!Instance)
            {
                instance = this;
                if (ConfigManager.Instance)
                {
                    // setAllVolume(ConfigManager.Instance.mainVolume, ConfigManager.Instance.bgmVolume, ConfigManager.Instance.seVolume);
                    // enableAudioSource(ConfigManager.Instance.MVEnable & ConfigManager.Instance.BGMEnable);
                }
            }
        }
        public void SetMasterVolume(float value)
        {
            // audioSource.volume = ConfigManager.Instance.bgmVolume * value;
            // ConfigManager.Instance.mainVolume = value;
        }

        public void SetMusicVolume(float value) 
        {
            // audioSource.volume = ConfigManager.Instance.mainVolume * value;
            // ConfigManager.Instance.bgmVolume = value;
        }

        public void SetSoundEffectVolume(float value)
        {
            // ConfigManager.Instance.seVolume = value;
        }

        public void setAllVolume(float mainVolume, float bgmVolume, float seVolume)
        {
            audioSource.volume = mainVolume * bgmVolume;
        }

        public void enableAudioSource(bool enable)
        {
            audioSource.enabled = enable;
        }
    }
}
