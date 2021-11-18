using System;
using System.IO;
using System.Text;
using UnityEngine;
using Universal;

namespace Managers
{
    /// <summary>
    /// 设置管理器
    /// </summary>
    public class ConfigManager : MonoBehaviour
    {
        #region c# properties
        //对象单例化
        private static ConfigManager instance = null;
        public static ConfigManager Instance
        {
            get => instance;
        }
        /// <summary>
        /// 全屏设置
        /// </summary>
        /// <value>bool</value>
        public bool isFull
        {
            get => configData.isFull;
            set => configData.isFull = value;
        }
        public int width
        {
            get => configData.width;
            set => configData.width = value;
        }
        public int height
        {
            get => configData.height;
            set => configData.height = value;
        }
        public float mainVolume
        {
            get => configData.mainVolume;
            set => configData.mainVolume = value;
        }
        public float bgmVolume
        {
            get => configData.bgmVolume;
            set => configData.bgmVolume = value;
        }
        public float seVolume
        {
            get => configData.seVolume;
            set => configData.seVolume = value;
        }
        public bool MVEnable
        {
            get => configData.mainVolumeEnable;
            set => configData.mainVolumeEnable = value;
        }
        public bool BGMEnable
        {
            get => configData.bgmVolumeEnable;
            set => configData.bgmVolumeEnable = value;
        }
        public bool SEEnable
        {
            get => configData.seVolumeEnable;
            set => configData.seVolumeEnable = value;
        }
        #endregion
        #region c# vriables
        /// <summary>
        /// 文件路径
        /// </summary>
        private string dirPath;
        /// <summary>
        /// 文件名
        /// </summary>
        private string fileNmae = "Config.ini";
        /// <summary>
        /// 设置数据对象
        /// </summary>
        [SerializeField]
        private ConfigData configData;
        private string sectionResolution = "Resolution";
        private string sectionFullScreen = "FullScreen";
        private string sectionVolume = "Volume";
        private string keyResolutionWidth = "Width";
        private string keyResolutionHeight = "Height";
        private string keyFullScreen = "isFull";
        private string keyMainVolume = "Main";
        private string keyBGMVolume = "BGM";
        private string keySEVolume = "SE";
        private string keyMainEnable = "Main Enable";
        private string keyBGMEnable = "BGM Enable";
        private string keySEEnable = "SE Enable";
        #endregion

        private void Awake()
        {
            if (!Instance)
            {
                instance = this;
                //初始化文件路径、创建设置数据实例
                dirPath = Application.persistentDataPath;
                //创建文件路径
                INIWriter.CheckPath(dirPath);
            }
            if (loadINI())
            {
                setConfig();
            }
            else
            {
                saveINI();
            }
        }
        public void setConfig()
        {
            Screen.SetResolution(width, height, isFull);
            if (AudioManager.Instance)
            {
                AudioManager.Instance.setAllVolume(mainVolume, bgmVolume, seVolume);
                AudioManager.Instance.enableAudioSource(MVEnable & BGMEnable);
            }
        }

        private bool loadINI()
        {
            string filePath = dirPath + "/" + fileNmae;
            INIWriter.CheckPath(dirPath);
            if (!INIWriter.CheckFile(filePath))
            {
                return false;
            }
            width = int.Parse(INIWriter.Read(sectionResolution, keyResolutionWidth, "1280", filePath));
            height = int.Parse(INIWriter.Read(sectionResolution, keyResolutionHeight, "720", filePath));
            isFull = bool.Parse(INIWriter.Read(sectionFullScreen, keyFullScreen, "false", filePath));
            mainVolume = float.Parse(INIWriter.Read(sectionVolume, keyMainVolume, "1.0", filePath));
            bgmVolume = float.Parse(INIWriter.Read(sectionVolume, keyBGMVolume, "1.0", filePath));
            seVolume = float.Parse(INIWriter.Read(sectionVolume, keySEVolume, "1.0", filePath));
            MVEnable = bool.Parse(INIWriter.Read(sectionVolume, keyMainEnable, "true", filePath));
            BGMEnable = bool.Parse(INIWriter.Read(sectionVolume, keyBGMEnable, "true", filePath));
            SEEnable = bool.Parse(INIWriter.Read(sectionVolume, keySEEnable, "true", filePath));
            return true;
        }

        public void saveINI()
        {
            string filePath = dirPath + "/" + fileNmae;
            if (!INIWriter.CheckFile(filePath))
            {
                new FileInfo(filePath).Create();
            }
            INIWriter.CheckPath(dirPath);
            INIWriter.Write(sectionResolution, keyResolutionWidth, width.ToString(), filePath);
            INIWriter.Write(sectionResolution, keyResolutionHeight, height.ToString(), filePath);
            INIWriter.Write(sectionFullScreen, keyFullScreen, isFull.ToString(), filePath);
            INIWriter.Write(sectionVolume, keyMainVolume, mainVolume.ToString(), filePath);
            INIWriter.Write(sectionVolume, keyBGMVolume, bgmVolume.ToString(), filePath);
            INIWriter.Write(sectionVolume, keySEVolume, seVolume.ToString(), filePath);
            INIWriter.Write(sectionVolume, keyMainEnable, MVEnable.ToString(), filePath);
            INIWriter.Write(sectionVolume, keyBGMEnable, BGMEnable.ToString(), filePath);
            INIWriter.Write(sectionVolume, keySEEnable, SEEnable.ToString(), filePath);
        }

        [Serializable]
        public class ConfigData : ArchiveData
        {
            #region c# vriables
            /// <summary>
            /// 屏幕宽度数据
            /// </summary>
            public int width;
            /// <summary>
            /// 屏幕高度数据
            /// </summary>
            public int height;
            /// <summary>
            /// 屏幕全屏flag
            /// </summary>
            public bool isFull;
            [Range(0, 1)]
            public float mainVolume;
            [Range(0, 1)]
            public float bgmVolume;
            [Range(0, 1)]
            public float seVolume;
            public bool mainVolumeEnable;
            public bool bgmVolumeEnable;
            public bool seVolumeEnable;
            #endregion
        }
    }
}

