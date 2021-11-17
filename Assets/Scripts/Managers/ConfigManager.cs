using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    /// <summary>
    /// 设置管理器
    /// </summary>
    public class ConfigManager : ArchiveDataManager
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
        /// 类型
        /// </summary>
        private string type = "Config";
        /// <summary>
        /// 文件名
        /// </summary>
        private string fileNmae;
        /// <summary>
        /// 设置数据对象
        /// </summary>
        [SerializeField]
        private ConfigData configData;
        #endregion

        private void Awake()
        {
            if (!Instance)
            {
                instance = this;
                //初始化文件路径和文件名、创建设置数据实例
                dirPath = Application.persistentDataPath + "/Saves";
                fileNmae = type + ".json";
                //创建文件路径
                DirectoryInfo di = new DirectoryInfo(dirPath);
                di.Create();
                //加载设置数据
                if (!loadData())
                {
                    saveData();
                }
            }
        }
        private void setConfig()
        {
            Screen.SetResolution(width, height, isFull);
            if (AudioManager.Instance)
            {
                AudioManager.Instance.setAllVolume(mainVolume, bgmVolume, seVolume);
                AudioManager.Instance.enableAudioSource(MVEnable & BGMEnable);
            }
        }
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <returns>string</returns>
        public override string getArchiveType() => type;
        /// <summary>
        /// 获取设置存档数据实例
        /// </summary>
        /// <returns>ArchiveData</returns>
        public override ArchiveData getArchiveData() => configData;
        public void loadConfig() => loadData();
        public void saveConfig() => saveData();
        /// <summary>
        /// 加载本地数据文件
        /// </summary>
        /// <returns>bool</returns>
        public bool loadData()
        {
            if (!new FileInfo(dirPath + "/config.json").Exists)
            {
                return false;
            }
            //创建输入流
            StreamReader sr = new StreamReader(dirPath + "/config.json", Encoding.UTF8);
            if (sr == null)
            {
                return false;
            }
            //json反序列化
            JsonUtility.FromJsonOverwrite(sr.ReadToEnd().Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", ""), configData);
            //关闭流
            sr.Close();
            setConfig();
            return true;
        }
        /// <summary>
        /// 保存数据到本地
        /// </summary>
        /// <returns>bool</returns>
        public bool saveData()
        {
            new DirectoryInfo(dirPath).Create();
            //创建输出流
            StreamWriter sw = new StreamWriter(dirPath + "/config.json"
            , false, Encoding.UTF8);
            if (sw == null)
            {
                return false;
            }
            //序列化object
            string json = configData.getJson();
            sw.WriteLine(json);
            //关闭输出流
            sw.Close();
            setConfig();
            return true;
        }
        public override Type getArchiveDataType() => configData.GetType();

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

