using System.Collections.Generic;

namespace Managers.Archive
{
    public abstract class ArchivePresenter
    {
        protected List<ArchiveInfo> infoList;
        protected Dictionary<string, ArchiveBank> archives;
        protected ArchiveInfo currentInfo;
        public int Count{get=>infoList.Count;}
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="infoList">存档信息数据列表</param>
        /// <param name="archives">存档数据字典</param>
        /// <param name="currentInfo">当前存档信息数据</param>
        public ArchivePresenter(List<ArchiveInfo> infoList, Dictionary<string, ArchiveBank> archives, ArchiveInfo currentInfo = null)
        {
            this.infoList = infoList;
            this.archives = archives;
            this.currentInfo = currentInfo;
        }

        protected ArchivePresenter(List<ArchiveInfo> infoList)
        {
            this.infoList = infoList;
        }
        /// <summary>
        /// 获取存档名称
        /// </summary>
        /// <param name="tag">tag</param>
        /// <returns></returns>
        public string getName(int tag) => infoList[tag].name;
        public abstract void cilckAction(int targetTag);
        /// <summary>
        /// 是否为无效存档
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>是否为无效存档</returns>
        public bool isInvalid(int tag) => infoList[tag].isInvalid;
        public abstract void finish();
    }
}