using System.Collections.Generic;

namespace Managers.Archive
{
    public class SavePresenter : ArchivePresenter
    {
        public SavePresenter(List<ArchiveInfo> infoList, Dictionary<string, ArchiveBank> archives, ArchiveInfo currentInfo) : base(infoList, archives, currentInfo)
        {
        }
        /// <summary>
        /// 点击响应回调
        /// </summary>
        /// <param name="targetTag">存档tag</param>
        public override void cilckAction(int targetTag)
        {
            SaveArchive(targetTag);
        }
        private void SaveArchive(int targetTag)
        {
            ArchiveManager manager = GameManager.Instance.ArchiveManager;
            if (manager == null)
            {
                return;
            }
            if (currentInfo.tag != targetTag)
            {
                if (currentInfo.tag < infoList.Count)
                {
                    infoList[currentInfo.tag] = new ArchiveInfo(currentInfo.tag, currentInfo.name, false);
                }
                currentInfo.tag = targetTag;
                addFiller(targetTag);
                infoList[currentInfo.tag] = currentInfo;
            }
            manager.saveData();
        }
        private void addFiller(int targetIndex)
        {
            while (infoList.Count <= targetIndex)
            {
                infoList.Add(new ArchiveInfo(infoList.Count, "Invalid", true));
            }
        }
        private void ArchiveInfoCopy(ArchiveInfo source, ArchiveInfo target)
        {
            target.name = source.name;
            target.isInvalid = source.isInvalid;
        }

        public override void finish()
        {
        }
    }
}