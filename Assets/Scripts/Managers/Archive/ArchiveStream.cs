using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Managers.Archive
{
    public class ArchiveStream
    {
        private const string filePrefix = "save_";
        /// <summary>
        /// 返回完整文件名不包括后缀
        /// </summary>
        /// <param name="tag">存档tag</param>
        /// <returns>完整文件名不包括后缀</returns>
        private static string getFileName(int tag) => filePrefix + tag + "." + ArchiveChecker.fileSuffix;
        /// <summary>
        /// 保存存档数据
        /// </summary>
        /// <param name="data">保存的存档数据字典</param>
        public static void saveData(Dictionary<string, ArchiveBank> data)
        {
            string str = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });
            FileStream.saveData(getFileName((data[ArchiveManager.InfoKey] as ArchiveInfo).tag), str);
        }
        /// <summary>
        /// 加载存档数据
        /// </summary>
        /// <param name="tag">存档tag</param>
        /// <param name="nullAction">当存档为不存在时执行该回调</param>
        public static async void loadData(int tag, Action<int> nullAction = null)
        {
            ArchiveManager manager = GameManager.Instance.ArchiveManager;
            if (manager == null)
            {
                return;
            }
            string fullName = getFileName(tag);
            string json = await FileStream.getData(fullName);
            if (json == null)
            {
                if (nullAction != null)
                {
                    nullAction.Invoke(tag);
                }
                return;
            }
            Dictionary<string, ArchiveBank> dic = JsonConvert.DeserializeObject<Dictionary<string, ArchiveBank>>(json, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });
            if (dic == null)
            {
                if (nullAction != null)
                {
                    nullAction.Invoke(tag);
                }
                return;
            }
            manager.archives = dic;
            manager.currentInfo = dic[ArchiveManager.InfoKey] as ArchiveInfo;
        }
    }
}