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
        private AudioSource audioSource;       //���п��Ƶ�Mixer����
        private void Awake()
        {
            if (!Instance)
            {
                instance = this;
                if (ConfigManager.Instance)
                {
                    setAllVolume(ConfigManager.Instance.mainVolume, ConfigManager.Instance.bgmVolume, ConfigManager.Instance.seVolume);
                    enableAudioSource(ConfigManager.Instance.MVEnable & ConfigManager.Instance.BGMEnable);
                }
            }
        }
        public void SetMasterVolume(float value)   //�����������ĺ���
        {
            audioSource.volume = ConfigManager.Instance.bgmVolume * value;
            // MasterVolueΪ���Ǳ�¶������Master�Ĳ���
        }

        public void SetMusicVolume(float value)   //���Ʊ������������ĺ���
        {
            audioSource.volume = ConfigManager.Instance.mainVolume * value;
            // MasterVolueΪ���Ǳ�¶������Music�Ĳ���
        }

        public void SetSoundEffectVolume(float value)   //������Ч�����ĺ���
        {
            // MasterVolueΪ���Ǳ�¶������SoundEffect�Ĳ���
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
