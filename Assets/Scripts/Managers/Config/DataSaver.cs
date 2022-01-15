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
        public readonly static string FileName = "Config.ini";
        public readonly static string Section = "Config";
        public readonly static string FullScreenKey = "fullscreen";
        public readonly static string DPIKey = "dpi";
        public string path;
        private Action saveAction;
        private Action<Data> loadAction;

        #region init
        public DataSaver()
        {
            path = Application.persistentDataPath;
        }
        public void setSaveAction(Action action)
        {
            saveAction = action;
        }
        public void setLoadAction(Action<Data> action)
        {
            loadAction = action;
        }
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
        public async void load()
        {
            Data data = new Data();
            await Task.Run(() =>
            {
                string fileName = getFullName();
                if (!checkFile(fileName))
                {
                    INIWriter.Write(Section, FullScreenKey, data.isFull.ToString(), fileName);
                    INIWriter.Write(Section, DPIKey, data.dpi, fileName);
                }
                else
                {
                    data.isFull = bool.Parse(INIWriter.Read(Section, FullScreenKey, ConfigManager.DefaultFullScreen.ToString(), fileName));
                    data.dpi = INIWriter.Read(Section, DPIKey, ConfigManager.DefaultDPI, fileName);
                }
            });
            if (loadAction != null)
                loadAction.Invoke(data);
        }
        public async void save(System.Object o)
        {
            await Task.Run(() =>
            {
                Data data = o as Data;
                string fileName = getFullName();

                INIWriter.Write(Section, FullScreenKey, data.isFull.ToString(), fileName);
                INIWriter.Write(Section, DPIKey, data.dpi, fileName);
            });
            if (saveAction != null)
                saveAction.Invoke();
        }
    }
}
