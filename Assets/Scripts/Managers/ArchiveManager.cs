using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

/// <summary>
/// 存档管理器
/// </summary>
namespace Managers.Archive
{
    public class ArchiveManager : MonoBehaviour
    {
        #region c# vriable
        /// <summary>
        /// 各个数据对应的管理器
        /// </summary>
        public List<ArchiveDataManager> archiveDataManagers;
        //存档根目录
        private string basePath;
        private string m_path;
        /// <summary>
        /// 
        /// </summary>
        /// <value>路径属性</value>
        public string path
        {
            get
            {
                return m_path;
            }
            set
            {
                m_path = basePath + "/" + value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <value>现有存档数</value>
        public int savesCount
        {
            get
            {
                return headData.savesCount;
            }
            set
            {
                //与首部数据同步
                headData.savesCount = value;
            }
        }
        private int m_position;
        /// <summary>
        /// 
        /// </summary>
        /// <value>当前存档索引</value>
        public int position
        {
            get
            {
                return m_position;
            }
            set
            {
                //设置对应的文件名和文件路径、存档名
                m_position = value;
                m_fileNmae = headData.flieName[value];
                path = m_fileNmae;
                m_saveName = headData.saveName[value];
            }
        }

        //首部数据对象
        private HeadData headData;
        private string m_fileNmae;
        /// <summary>
        /// 
        /// </summary>
        /// <value>文件名</value>
        public string fileNmae
        {
            get
            {
                return m_fileNmae;
            }
            set
            {
                //设置对应文件路径、若首部数据中不包含该文件名则建立新建flag并设置对应索引
                m_fileNmae = value;
                path = value;
                if (!headData.flieName.Contains(value))
                {
                    isNew = true;
                    m_position = headData.savesCount;
                }
            }
        }
        private string m_saveName;
        /// <summary>
        /// 
        /// </summary>
        /// <value>存档名</value>
        public string saveName
        {
            get
            {
                return m_saveName;
            }
            set
            {
                //若为新建存档则初始化索引、文件名以及文件路径
                m_saveName = value;
                if (isNew)
                {
                    m_position = headData.savesCount;
                    m_fileNmae = "/save" + savesCount;
                    path = m_fileNmae;
                }
            }
        }

        /// <summary>
        /// 存档新建flag
        /// </summary>
        public bool isNew = false;
        /// <summary>
        /// 存档覆盖flag
        /// </summary>
        public bool isOverride = false;
        #endregion

        private void Awake()
        {
            //初始化路径
            basePath = Application.persistentDataPath + "/Saves";
            //若文件路径不存在则创建文件路径，否则无影响
            initPath(basePath);
            //若加载首部数据失败则新建首部并保存
            if (!loadHead())
            {
                headData = new HeadData();
                saveHead();
            }
        }

        /// <summary>
        /// 存档初始化
        /// </summary>
        public void init()
        {
            //建立新建flag并设置存档名称和初始化文件名
            isNew = true;
            saveName = "/save" + savesCount;
            fileNmae = "/save" + savesCount;
        }

        /// <summary>
        /// 更改存档名称
        /// </summary>
        /// <param name="name">新名称</param>
        /// <returns>bool</returns>
        public bool changeSaveName(string name)
        {
            if (headData.savesCount > position)
            {
                headData.saveName[position] = name;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 自动存档
        /// </summary>
        public void autoSave()
        {
            string saveName = "AutoSave";
            //若首部数据中不存在该文件名则添加索引、文件名、存档名、创建时间和最后修改时间
            //否则更新最后修改时间
            if (!headData.flieName.Contains(saveName))
            {
                headData.savesCount++;
                headData.flieName.Add(saveName);
                headData.saveName.Add(saveName);
                headData.createTime.Add(System.DateTime.Now.Ticks);
                headData.lastModifiedTime.Add(System.DateTime.Now.Ticks);
            }
            else
            {
                int v = headData.flieName.IndexOf(saveName);
                headData.lastModifiedTime[v] = System.DateTime.Now.Ticks;
            }
            //保存首部文件
            saveHead();
            //初始化存档路径
            initPath(basePath + "/" + saveName);
            //遍历数据管理器保存数据
            foreach (ArchiveDataManager m in archiveDataManagers)
            {
                m.saveData(basePath + "/" + saveName, m.getArchiveType(), m.getArchiveData());
            }
        }

        /// <summary>
        /// 返回存档名称数组
        /// </summary>
        /// <returns>string</returns>
        public string[] getSaveNames() => headData.saveName.ToArray();

        /// <summary>
        /// 初始化文件路径
        /// </summary>
        /// <param name="path">需要初始化的路径</param>
        private void initPath(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            di.Create();
        }

        /// <summary>
        /// 保存游戏存档
        /// </summary>
        public void saveGame()
        {
            //若为新存档则更新首部数据
            //否则更新存档名和最后修改时间
            if (isNew || !headData.flieName.Contains(fileNmae))
            {
                headData.savesCount++;
                headData.flieName.Add(fileNmae);
                headData.saveName.Add(saveName);
                headData.createTime.Add(System.DateTime.Now.Ticks);
                headData.lastModifiedTime.Add(System.DateTime.Now.Ticks);
            }
            else
            {
                headData.saveName[position] = saveName;
                headData.lastModifiedTime[position] = System.DateTime.Now.Ticks;
            }
            //保存首部数据
            saveHead();
            //初始化路径
            initPath(path);
            //遍历数据管理器保存数据
            foreach (ArchiveDataManager m in archiveDataManagers)
            {
                m.saveData(path, m.getArchiveType(), m.getArchiveData());
            }
        }

        /// <summary>
        /// 加载游戏存档
        /// </summary>
        /// <returns>bool</returns>
        public bool loadGame()
        {
            //遍历数据管理器
            foreach (ArchiveDataManager m in archiveDataManagers)
            {
                //尝试获取存档
                if (!m.tryToLoad(m_path))
                {
                    //失败时进行的action
                    failToLoad();
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 存档读取失败时采取的行为
        /// </summary>
        public void failToLoad()
        {
            //删除对应头部数据
            headData.savesCount--;
            headData.flieName.Remove(fileNmae);
            headData.saveName.Remove(saveName);
            headData.createTime.Remove(headData.createTime[position]);
            headData.lastModifiedTime.Remove(headData.lastModifiedTime[position]);
        }

        /// <summary>
        /// 保存头部数据
        /// </summary>
        /// <returns>bool</returns>
        public bool saveHead()
        {
            //设置数据输出流
            StreamWriter sw = new StreamWriter(basePath + "/head.json"
            , false, Encoding.UTF8);
            if (sw == null)
            {
                return false;
            }
            //序列化json字符串
            string json = headData.getJson();
            sw.WriteLine(json);
            //关闭流
            sw.Close();
            return true;
        }

        /// <summary>
        /// 读取首部数据
        /// </summary>
        /// <returns>bool</returns>
        private bool loadHead()
        {
            //判断文件是否存在
            FileInfo fileInfo = new FileInfo(basePath + "/head.json");
            if (!fileInfo.Exists)
            {
                return false;
            }
            //建立输入流
            StreamReader sr = new StreamReader(fileInfo.FullName, Encoding.UTF8);
            if (sr == null)
            {
                return false;
            }
            //json字符串逆序列化
            HeadData data = JsonUtility.FromJson<HeadData>(sr.ReadToEnd().Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", ""));
            if (data != null)
            {
                headData = data;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 首部数据类
        /// </summary>
        private class HeadData : ArchiveData
        {
            /// <summary>
            /// 当前存档数
            /// </summary>
            public int savesCount;
            /// <summary>
            /// 文件名列表
            /// </summary>
            public List<string> flieName;
            /// <summary>
            /// 存档名列表
            /// </summary>
            public List<string> saveName;
            /// <summary>
            /// 创建时间戳列表
            /// </summary>
            public List<long> createTime;
            /// <summary>
            /// 最后修改时间戳列表
            /// </summary>
            public List<long> lastModifiedTime;

            public HeadData()
            {
                savesCount = 0;
                flieName = new List<string>();
                saveName = new List<string>();
                createTime = new List<long>();
                lastModifiedTime = new List<long>();
            }
        }
    }

    /// <summary>
    /// 存档数据基类
    /// </summary>
    abstract public class ArchiveData
    {
        /// <summary>
        /// 把类转化为json序列字符串
        /// </summary>
        /// <returns>string</returns>
        public string getJson() => JsonUtility.ToJson(this, true);
        /// <summary>
        /// 把json序列字符串反序列化为类
        /// </summary>
        /// <returns>T</returns>
        public static T getObject<T>(string json) => JsonUtility.FromJson<T>(json);
    }
    /// <summary>
    /// 存档数据管理器，需要至少拥有一个存档数据基类或其子类的实体
    /// </summary>
    abstract public class ArchiveDataManager : MonoBehaviour
    {
        /// <summary>
        /// 尝试读取存档
        /// </summary>
        /// <param name="path">存档路径</param>
        /// <returns>bool</returns>
        public abstract bool tryToLoad(string path);
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="path">存档目录</param>
        /// <param name="type">存档类型</param>
        /// <param name="data">存档数据基类或子类其实体</param>
        /// <returns></returns>
        public bool saveData(string path, string type, ArchiveData data)
        {
            //创建输出流
            StreamWriter sw = new StreamWriter(path + "/" + type + ".json"
            , false, Encoding.UTF8);
            if (sw == null)
            {
                return false;
            }
            //序列化object
            string json = data.getJson();
            sw.WriteLine(json);
            //关闭输出流
            sw.Close();
            return true;
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="path">存档目录</param>
        /// <param name="type">存档类型</param>
        /// <typeparam name="T">存档数据类型，例如ArchiveData</typeparam>
        /// <returns>T</returns>
        public T loadData<T>(string path, string type)
        {
            if (!new FileInfo(path + "/" + type + ".json").Exists)
            {
                return default(T);
            }
            //创建输入流
            StreamReader sr = new StreamReader(path + "/" + type + ".json", Encoding.UTF8);
            if (sr == null)
            {
                return default(T);
            }
            //json反序列化
            T data = JsonUtility.FromJson<T>(sr.ReadToEnd().Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", ""));
            //关闭流
            sr.Close();
            if (data != null)
            {
                return data;
            }
            else
            {
                return default(T);
            }
        }
        /// <summary>
        /// 返回存档类型
        /// </summary>
        /// <returns>string</returns>
        public abstract string getArchiveType();
        /// <summary>
        /// 返回存档数据实例
        /// </summary>
        /// <returns>ArchiveData</returns>
        /// public abstract ref T getArchiveData<T>();
        /// <summary>
        /// 放回存档数据类型，例如ArchiveData.GetType()
        /// </summary>
        /// <returns>Type</returns>
        public abstract Type getArchiveDataType();
        public abstract ArchiveData getArchiveData();
    }
}