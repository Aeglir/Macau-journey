using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers.Archive
{
    public class LoadPresenter : ArchivePresenter
    {
        public LoadPresenter(List<ArchiveInfo> infoList, Dictionary<string, ArchiveBank> archives) : base(infoList, archives)
        {
        }
        private void loadArchive(int targetTag)
        {
            if (targetTag >= infoList.Count || GameManager.Instance.ArchiveManager == null)
            {
                return;
            }
            GameManager.Instance.ArchiveManager.loadArchive(infoList[targetTag]);
            finish();
        }
        /// <summary>
        /// 点击响应
        /// </summary>
        /// <param name="targetTag">存档tag</param>
        public override void cilckAction(int targetTag)
        {
            if (targetTag >= infoList.Count)
            {
                return;
            }
            loadArchive(targetTag);
        }
        public override void finish() => SceneManager.LoadScene("MainMenuScene");
    }
}