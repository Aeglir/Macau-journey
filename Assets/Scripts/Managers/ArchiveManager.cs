using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Managers
{
    public class ArchiveManager : MonoBehaviour
    {
        #region c# vriable
        //各个数据对应的管理器
        public List<ArchiveDataManager> archiveDataManagers;

        private string basePath;

        //存储路径字段
        private string m_path;
        //存储路径属性
        public string path
        {
            get
            {
                return m_path;
            }
        }
        //存储文件名
        private string saveName;
        #endregion

        private void Awake()
        {
            //初始化路径
            basePath = Application.persistentDataPath + "/Saves";
            //若文件路径不存在则创建文件路径，否则无影响
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(basePath);
            di.Create();
            //getLocalSaveCount();
        }

        public void init()
        {
            ConfigManager.Instance.setVriable<int>(ConfigManager.SAVECOUNT
            , ConfigManager.Instance.getVriable<int>(ConfigManager.SAVECOUNT) + 1);
            saveName = "/save" + ConfigManager.Instance.getVriable<int>(ConfigManager.SAVECOUNT);
            m_path = basePath + saveName;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(m_path);
            di.Create();
            saveGame();
            loadGame();
        }

        public void saveGame()
        {
            foreach (ArchiveDataManager m in archiveDataManagers)
            {
                m.saveData(m_path);
            }
        }

        public void loadGame()
        {
            foreach (ArchiveDataManager m in archiveDataManagers)
            {
                reflectMethod(m, "loadData", new System.Object[2] { path, m.getArchiveType() });
            }
        }

        public void reflectMethod(ArchiveDataManager manager, string methodName, params System.Object[] o)
        {
            MethodInfo methodInfo = manager.GetType().GetMethod(methodName).MakeGenericMethod(new Type[] { manager.getArchiveDataType() });
            methodInfo.Invoke(manager, o);
        }

        public int getLocalSaveCount()
        {
            string[] file = Directory.GetFiles(path, "*.json");
            int count = 0;
            // for (int i = 0; i < file.Length; i++)
            // {
            // }
            return count;
        }
    }

    [System.Serializable]
    abstract public class ArchiveData
    {
        /// <summary>
        /// 把类转化为json序列字符串
        /// </summary>
        /// <returns></returns>
        public string getJson()
        {
            return JsonUtility.ToJson(this, true);
        }
        public static T getObject<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }
        /// <summary>
        /// 给类中指定名字的字段赋值
        /// name：字段名
        /// value:赋予的值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        public void SetValue<T>(string name, T value)
        {
            this.GetType().GetField(name).SetValue(this, value);
        }
        /// <summary>
        /// 读取类中指定名字的字段的值
        /// name：字段名
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValue<T>(string name)
        {
            return (T)this.GetType().GetField(name).GetValue(this);
        }
    }

    abstract public class ArchiveDataManager : MonoBehaviour
    {
        public abstract bool testToLoad(string path);
        public abstract bool saveData(string path);
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="data"></param>
        public T loadData<T>(string path, string type)
        {
            StreamReader sr = new StreamReader(path + "/" + type + ".json", Encoding.UTF8);
            if (sr == null)
            {
                return default(T);
            }
            T c = JsonUtility.FromJson<T>(sr.ReadToEnd().Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", ""));
            if (c != null)
            {
                return c;
            }
            else
            {
                return default(T);
            }
        }
        /// <summary>
        /// 返回数据类型
        /// </summary>
        /// <returns></returns>
        public abstract string getArchiveType();
        /// <summary>
        /// 初始化数据实例
        /// </summary>
        /// <returns></returns>
        public abstract ArchiveData initArchiveData();
        /// <summary>
        /// 返回数据实例
        /// </summary>
        /// <returns></returns>
        public abstract ArchiveData getArchiveData();
        public abstract Type getArchiveDataType();
        /// <summary>
        /// 给类中指定名字的字段赋值
        /// name：字段名
        /// value:赋予的值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        public void setVriable<T>(string name, T value)
        {
            getArchiveData().SetValue<T>(name, value);
        }
        /// <summary>
        /// 读取类中指定名字的字段的值
        /// name：字段名
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T getVriable<T>(string name)
        {
            return getArchiveData().GetValue<T>(name);
        }
    }
}


