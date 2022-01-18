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
        private static ConfigManager instance = null;
        [Header("全屏默认设置")]
        public readonly static bool DefaultFullScreen = false;
        [Header("HD分辨率")]
        public readonly static string HD = "1920X1080";
        [Header("默认分辨率")]
        public readonly static string DefaultDPI = "1280X720";
        [Header("默认音量")]
        public readonly static float DefaultVolume = 100;
        [Header("起始音量")]
        public readonly static float StartVolume = 0;
        [Header("音量转换因子")]
        public readonly static float VolumeFactor = 4.5f;
        public static ConfigManager Instance
        {
            get => instance;
        }
        public bool IsFull
        {
            get
            {
                return data.isFull;
            }
        }
        public string DPI
        {
            get
            {
                return data.dpi;
            }
        }
        public float MAINVOLUME
        {
            get
            {
                return data.mainVolume;
            }
        }
        public float BGM
        {
            get
            {
                return data.bgm;
            }
        }
        public float SE
        {
            get
            {
                return data.se;
            }
        }
        public enum TYPE
        {
            FULLSCREEN,
            DPI,
            MAINVOLUME,
            BGM,
            SE,
        };
        [Header("设置")]
        [SerializeField]
        private Data data;
        public AudioMixer audioMixer;
        private DataSaver dataSaver;
        private DataChanger dataChanger;
        private SettingUpdater updater;
        private void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(gameObject);
                return;
            }
            instance = this;
            Application.targetFrameRate = 60;
            dataSaver = new DataSaver();
            dataSaver.setLoadAction(d =>
            {
                data = d;
                dataChanger = new DataChanger(data);
                updater = new SettingUpdater(data, audioMixer);
                updater.apply();
            });
            dataSaver.load();
        }
        public ConfigPresenter getPresenter()
        {
            return new ConfigPresenter(data, dataSaver, dataChanger, updater);
        }
    }
}