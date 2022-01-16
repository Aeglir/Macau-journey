using System.Threading;
using System;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using UnityEngine;
using Universal;

namespace Managers.Config
{

    public class DataSaver
    {
        private readonly static string FileName = "Config.ini";
        private readonly static string Section = "Config";
        private readonly static string MAINVolumeKey = "main volume";
        private readonly static string BGMVolumeKey = "bgm volume";
        private readonly static string SEVolumeKey = "se volume";
        private readonly static string FullScreenKey = "fullscreen";
        private readonly static string DPIKey = "dpi";
        public string path;
        private Action saveAction;
        private Action<Data> loadAction;

        #region init
        public DataSaver()
        {
            path = Application.persistentDataPath;
        }
        /// <summary>
        /// 设置保存回调
        /// </summary>
        /// <param name="action">回调函数</param>
        public void setSaveAction(Action action)
        {
            saveAction = action;
        }
        /// <summary>
        /// 设置加载回调
        /// </summary>
        /// <param name="action">回调函数</param>
        public void setLoadAction(Action<Data> action)
        {
            loadAction = action;
        }
        /// <summary>
        /// 获取文件完整路径（包括文件名及其后缀）
        /// </summary>
        /// <returns></returns>
        private string getFullName()
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if (!di.Exists)
            {
                di.Create();
            }

            return path + "/" + FileName;
        }
        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="fileName">文件完整路径</param>
        /// <returns>文件存在返回ture，否则返回false</returns>
        private bool checkFile(string fileName)
        {
            FileInfo info = new FileInfo(fileName);

            if (!info.Exists)
            {
                return false;
            }

            return true;
        }
        #endregion
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <returns></returns>
        public async void load()
        {
            Data data = new Data();
            await Task.Run(() =>
            {
                string fileName = getFullName();
                if (!checkFile(fileName))
                {
                    INIWriter.Write(Section, MAINVolumeKey, data.mainVolume.ToString(), fileName);
                    INIWriter.Write(Section, BGMVolumeKey, data.bgm.ToString(), fileName);
                    INIWriter.Write(Section, SEVolumeKey, data.se.ToString(), fileName);
                    INIWriter.Write(Section, FullScreenKey, data.isFull.ToString(), fileName);
                    INIWriter.Write(Section, DPIKey, data.dpi, fileName);
                }
                else
                {
                    data.mainVolume = float.Parse(INIWriter.Read(Section, MAINVolumeKey, ConfigManager.DefaultVolume.ToString(), fileName));
                    data.bgm = float.Parse(INIWriter.Read(Section, BGMVolumeKey, ConfigManager.DefaultVolume.ToString(), fileName));
                    data.se = float.Parse(INIWriter.Read(Section, SEVolumeKey, ConfigManager.DefaultVolume.ToString(), fileName));
                    data.isFull = bool.Parse(INIWriter.Read(Section, FullScreenKey, ConfigManager.DefaultFullScreen.ToString(), fileName));
                    data.dpi = INIWriter.Read(Section, DPIKey, ConfigManager.DefaultDPI, fileName);
                }
            });
            if (loadAction != null)
                loadAction.Invoke(data);
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="o">待保存数据</param>
        /// <returns></returns>
        public async void save(System.Object o)
        {
            await Task.Run(() =>
            {
                Data data = o as Data;
                string fileName = getFullName();

                INIWriter.Write(Section, MAINVolumeKey, data.mainVolume.ToString(), fileName);
                INIWriter.Write(Section, BGMVolumeKey, data.bgm.ToString(), fileName);
                INIWriter.Write(Section, SEVolumeKey, data.se.ToString(), fileName);
                INIWriter.Write(Section, FullScreenKey, data.isFull.ToString(), fileName);
                INIWriter.Write(Section, DPIKey, data.dpi, fileName);
            });
            if (saveAction != null)
                saveAction.Invoke();
        }
    }
}
