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
        #region private Defaults
        private const string FileName = "Config.ini";
        private const string Section = "Config";
        private const string MAINVolumeKey = "main volume";
        private const string BGMVolumeKey = "bgm volume";
        private const string SEVolumeKey = "se volume";
        private const string FullScreenKey = "fullscreen";
        private const string DPIKey = "dpi";
        #endregion
        #region private fields
        private string path;
        private Action saveAction;
        private Action<ConfigData> loadAction;
        #endregion
        #region properties
        public string Path { get => path; }
        public Action SaveAction { set => saveAction = value; }
        public Action<ConfigData> LoadAction { set => loadAction = value; }
        #endregion
        #region private method
        private string getFullName()
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if (!di.Exists)
            {
                di.Create();
            }

            return path + "/" + FileName;
        }
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
        public DataSaver()
        {
            path = Application.persistentDataPath;
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <returns></returns>
        public async void load()
        {
            ConfigData data = new ConfigData();
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
                ConfigData data = o as ConfigData;
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
