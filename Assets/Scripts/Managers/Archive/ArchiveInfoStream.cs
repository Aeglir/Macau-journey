using System.Diagnostics;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Managers.Archive
{
    public class ArchiveInfoStream
    {
        /// <summary>
        /// 保存存档信息数据
        /// </summary>
        /// <param name="data">存档信息列表</param>
        public static void saveInfoData(List<ArchiveInfo> data)
        {
            string str = JsonConvert.SerializeObject(data, Formatting.Indented);
            FileStream.saveInfo(str);
        }
        /// <summary>
        /// 加载存档信息数据
        /// </summary>
        public static async void loadInfoData()
        {
            ArchiveManager manager = GameManager.Instance.ArchiveManager;
            if(manager==null)
            {
                return;
            }
            string json = await FileStream.getInfo();
            if (json == null)
            {
                manager.infos = new List<ArchiveInfo>();
                return;
            }
            List<ArchiveInfo> list = JsonConvert.DeserializeObject<List<ArchiveInfo>>(json);
            if (list == null)
            {
                manager.infos = new List<ArchiveInfo>();
                return;
            }
            manager.infos = list;
        }
    }
}