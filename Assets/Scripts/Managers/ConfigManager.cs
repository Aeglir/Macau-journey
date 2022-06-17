using System.Threading.Tasks;
using Managers.Config;
using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    /// <summary>
    /// 设置管理器
    /// </summary>
    public class ConfigManager : MonoBehaviour
    {
        public enum TYPE { FULLSCREEN, DPI, MAINVOLUME, BGM, SE };
        public AudioMixer audioMixer;
        #region default setting
        [Header("全屏默认设置")]
        public const bool DefaultFullScreen = false;
        [Header("HD分辨率")]
        public const string HD = "1920X1080";
        [Header("默认分辨率")]
        public const string DefaultDPI = "1280X720";
        [Header("默认音量")]
        public const float DefaultVolume = 100;
        [Header("起始音量")]
        public const float StartVolume = 0;
        [Header("音量转换因子")]
        public const float DefaultVolumeFactor = 4.5f;
        #endregion
        #region porperties
        public bool IsFull { get => data.isFull; }
        public string DPI { get => data.dpi; }
        public float MAINVOLUME { get => data.mainVolume; }
        public float BGM { get => data.bgm; }
        public float SE { get => data.se; }
        public ConfigData Data { get => data; }
        public DataSaver DataSaver { get => dataSaver; }
        public DataChanger DataChanger { get => dataChanger; }
        public SettingUpdater Updater { get => updater; }
        public ConfigPresenter presenter { get => new ConfigPresenter(this); }
        #endregion
        #region private field
        [Header("设置")]
        [SerializeField]
        private ConfigData data;
        private DataSaver dataSaver;
        private DataChanger dataChanger;
        private SettingUpdater updater;
        #endregion
        #region private methods
        private void Awake()
        {
            if (GameManager.Instance.hasLoad)
            {
                DestroyImmediate(this);
                return;
            }
            Application.targetFrameRate = 60;
            dataSaver = new DataSaver();
            dataSaver.LoadAction=(d =>
            {
                data = d;
                dataChanger = new DataChanger(data);
                updater = new SettingUpdater(data, audioMixer);
                updater.apply();
            });
            dataSaver.load();
        }
        #endregion
    }
}