using System.Collections.Generic;
using Managers.Archive;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 存档管理器
/// </summary>
namespace Managers
{
    public class ArchiveManager : MonoBehaviour
    {
        public const string InfoKey = "Info";
        public const string InfoFileName = "Head.index";
        public const string DefaultInfoName = "存档 ";
        [Header("存档信息列表")]
        public List<ArchiveInfo> infos;
        public Dictionary<string, ArchiveBank> archives;
        [Header("当前存档信息")]
        public ArchiveInfo currentInfo;
        #region private method
        private void Awake()
        {
            ArchiveInfoStream.loadInfoData();
        }
        #endregion
        /// <summary>
        /// 创建新存档
        /// </summary>
        public void createNewAchive()
        {
            currentInfo = new ArchiveInfo(infos.Count, DefaultInfoName + infos.Count, false);
            archives = new Dictionary<string, ArchiveBank>();
            archives.Add(InfoKey, currentInfo);
        }
        /// <summary>
        /// 保存当前存档
        /// </summary>
        public void saveData()
        {
            if (GameManager.Instance == null)
            {
                return;
            }
            ArchiveInfoStream.saveInfoData(infos);
            ArchiveStream.saveData(archives);
        }
        /// <summary>
        /// 加载指定存档
        /// </summary>
        /// <param name="data">存档信息</param>
        public void loadArchive(ArchiveInfo data)
        {
            if (data.isInvalid)
            {
                return;
            }
            ArchiveStream.loadData(data.tag, d =>
            {
                foreach (ArchiveInfo info in infos)
                {
                    if (info.tag == d)
                    {
                        infos.Remove(info);
                        ArchiveInfoStream.saveInfoData(infos);
                    }
                }
            });
        }
        /// <summary>
        /// 添加存档数据
        /// </summary>
        /// <param name="data">存档数据</param>
        /// <param name="key">键</param>
        /// <returns>添加是否成功</returns>
        public bool addArchiveData(ArchiveBank data, string key)
        {
            if (archives == null)
            {
                return false;
            }
            archives.Add(key, data);
            return true;
        }
        /// <summary>
        /// 获取控制器
        /// </summary>
        /// <param name="isLoad">是否为加载</param>
        /// <returns>存档控制器</returns>
        public ArchivePresenter GetPresenter(bool isLoad)
        {
            if (isLoad)
            {
                return new LoadPresenter(infos, archives);
            }
            else
            {
                return new SavePresenter(infos, archives, currentInfo);
            }
        }
    }
}