using System;
using System.IO;
using UnityEngine;

namespace Managers
{
    public class LogManager : MonoBehaviour
    {

        private StreamWriter SW;
        private string path;

        private string LogFileName;
        private void Awake()
        {
            path = Application.persistentDataPath + "/Logs";
            LogFileName = path + "/" + "Log" + ".txt";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
            di.Create();
            SW = new StreamWriter(LogFileName, true);
        }

        private void Start()
        {
        }

        [Obsolete]
        private void OnEnable() => Application.RegisterLogCallback(HandleLog);

        [Obsolete]
        private void OnDisable() => Application.RegisterLogCallback(null);

        private void HandleLog(string condition, string stackTrace, LogType type)
        {
            if (type == LogType.Exception || type == LogType.Error)
            {
                SW.WriteLine("Logged at: " + System.DateTime.Now.ToString()
                + " - Log Desc: " + condition + " - Trance: " + stackTrace + " - Type: " + type.ToString());
            }
        }

        private void OnDestroy()
        {
            SW.Close();
        }
    }
}


