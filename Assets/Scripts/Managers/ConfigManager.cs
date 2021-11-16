using System;
using System.IO;
using Managers.Archive;
using UnityEngine;

namespace Managers.Config
{
    /// <summary>
    /// 设置管理器
    /// </summary>
    public class ConfigManager : ArchiveDataManager
    {
        #region c# properties
        /// <summary>
        /// 全屏设置
        /// </summary>
        /// <value>bool</value>
        public bool isFull
        {
            get
            {
                return configData.isFull;
            }
            set
            {
                configData.isFull = value;
                Screen.SetResolution(Screen.width, Screen.height, value);
            }
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
        public ConfigData configData;
        #endregion

        private void Awake()
        {
            //初始化文件路径和文件名、创建设置数据实例
            dirPath = Application.persistentDataPath + "/Saves";
            fileNmae = type + ".json";
            //创建文件路径
            DirectoryInfo di = new DirectoryInfo(dirPath);
            di.Create();
            //加载设置数据
            if (!load())
            {
                configData = new ConfigData();
                save();
            }
        }
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <returns></returns>
        public override string getArchiveType() => type;
        /// <summary>
        /// 获取设置存档数据实例
        /// </summary>
        /// <returns></returns>
        public override ArchiveData getArchiveData() => configData;
        /// <summary>
        /// 获取设置存档数据类型
        /// </summary>
        /// <returns></returns>
        public override Type getArchiveDataType() => configData.GetType();
        /// <summary>
        /// 尝试加载设置存档数据
        /// </summary>
        /// <param name="path"></param>
        /// <returns>bool</returns>
        public override bool tryToLoad(string path)
        {
            //检测文件是否存在
            if (!new FileInfo(path + "/" + fileNmae).Exists)
            {
                return false;
            }
            //加载数据
            ConfigData data = loadData<ConfigData>(path, type);
            if (data != null)
            {
                configData = data;
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 保存设置存档数据
        /// </summary>
        public void save() => saveData(dirPath, getArchiveType(), getArchiveData());
        /// <summary>
        /// 加载设置存档数据
        /// </summary>
        /// <returns></returns>
        public bool load() => tryToLoad(dirPath);
        /// <summary>
        /// 设置屏幕分辨率
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">长度</param>
        public void setResolution(int width, int height)
        {
            configData.width = width;
            configData.height = height;
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
            configData.width = width;
            configData.height = height;
            configData.isFull = isFull;
            Screen.SetResolution(width, height, isFull);
        }

        public class ConfigData : ArchiveData
        {
            #region c# vriables
            /// <summary>
            /// 屏幕宽度数据
            /// </summary>
            public int width = 1920;
            /// <summary>
            /// 屏幕高度数据
            /// </summary>
            public int height = 1080;
            /// <summary>
            /// 屏幕全屏flag
            /// </summary>
            public bool isFull = true;
            #endregion
        }
    }
}

