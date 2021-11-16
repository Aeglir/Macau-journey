using System;
using System.IO;
using System.Text;
using UnityEngine;

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
            if (Instance == null)
            {
                instance = this;
            }
            //初始化文件路径和文件名、创建设置数据实例
            dirPath = Application.persistentDataPath + "/Saves";
            fileNmae = type + ".json";
            //创建文件路径
            DirectoryInfo di = new DirectoryInfo(dirPath);
            di.Create();
            //加载设置数据
            configData = new ConfigData();
            if (!loadData())
            {
                saveData();
            }
            setResolution(width, height, isFull);
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
            return true;
        }
        /// <summary>
        /// 设置屏幕分辨率
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">长度</param>
        public void setResolution(int width, int height)
        {
            this.width = width;
            this.height = height;
            Screen.SetResolution(width, height, Screen.fullScreenMode);
        }
        /// <summary>
        /// 设置屏幕分辨率
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">长度</param>
        /// <param name="isFull">全屏</param>
        public void setResolution(int width, int height, bool isFull)
        {
            this.width = width;
            this.height = height;
            this.isFull = isFull;
            Screen.SetResolution(width, height, isFull);
        }

        public override Type getArchiveDataType() => configData.GetType();

        [Serializable]
        public class ConfigData : ArchiveData
        {
            #region c# vriables
            /// <summary>
            /// 屏幕宽度数据
            /// </summary>
            public int width = 1280;
            /// <summary>
            /// 屏幕高度数据
            /// </summary>
            public int height = 720;
            /// <summary>
            /// 屏幕全屏flag
            /// </summary>
            public bool isFull = false;
            #endregion
        }
    }
}

