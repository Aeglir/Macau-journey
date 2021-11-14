
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Managers
{
    public class ConfigManager : ArchiveDataManager
    {
        #region c# properties
        //对象单例化
        private static ConfigManager instance = null;
        public static ConfigManager Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion
        #region c# vriables
        //返回数据实例中的各字段名称
        public static string WIDTH
        {
            get
            {
                return "width";
            }
        }
        public static string HEIGHT
        {
            get
            {
                return "height";
            }
        }

        public static string SAVECOUNT
        {
            get
            {
                return "saveCount";
            }
        }
        //初始化数据类型
        private string type = "Config";
        //数据实例
        private ConfigData configData;
        #endregion

        private void Awake()
        {
            if (instance)
            {
                DestroyImmediate(gameObject);
                return;
            }

            instance = this;
            configData = new ConfigData();
        }
        public override string getArchiveType()
        {
            return type;
        }
        public override bool saveData(string path)
        {
            StreamWriter sw = new StreamWriter(path + "/" + type + ".json"
            , false, Encoding.UTF8);
            if (sw == null)
            {
                return false;
            }
            sw.WriteLine(configData.getJson());
            sw.Close();
            return true;
        }

        public override bool testToLoad(string path)
        {
            StreamReader sr = new StreamReader(path + "/" + type + ".json", Encoding.UTF8);
            if (sr == null)
            {
                return false;
            }
            ConfigData c = JsonUtility.FromJson<ConfigData>(sr.ReadToEnd().Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", ""));
            if (c != null)
            {
                return true;
            }
            return false;
        }
        public override ArchiveData getArchiveData(){
            return configData;
        }

        public override ArchiveData initArchiveData()
        {
            configData = new ConfigData();
            return getArchiveData();
        }

        public override Type getArchiveDataType()
        {
            return configData.GetType();
        }

        [System.Serializable]
        private class ConfigData : ArchiveData
        {
            #region c# vriables
            //现已存档数
            public int saveCount = 0;
            //设置屏幕宽度数据
            public int width = 1920;
            //设置屏幕高度数据
            public int height = 1080;
            //设置屏幕全屏flag
            public bool isFull = true;
            #endregion
        }
    }
}

