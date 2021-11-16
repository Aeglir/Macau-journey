using Managers.Archive;
using Managers.Config;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region c# properties
        //对象单例化
        private static GameManager instance = null;
        public static GameManager Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion

        #region c# vriables
        //日志输出管理器
        public LogManager logManager;
        //数据存储管理器
        public ArchiveManager archiveManager;

        public ConfigManager configManager;
        #endregion
        private void Awake()
        {
            //若已存在实例则销毁当前新建的gameObject
            if (instance)
            {
                DestroyImmediate(gameObject);
                return;
            }

            instance = this;

            //防止被销毁
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// 开始新游戏
        /// </summary>
        public void newGame()
        {
            archiveManager.init();
        }
        /// <summary>
        /// 保存存档
        /// </summary>
        public void gameSave() => archiveManager.saveGame();

        /// <summary>
        /// 加载存档
        /// </summary>
        /// <returns></returns>
        public bool gameLoad() => archiveManager.loadGame();

        /// <summary>
        /// 自动存档
        /// </summary>
        public void autoSave() => archiveManager.autoSave();

        /// <summary>
        /// 保存设置
        /// </summary>
        public void configSave() => configManager.save();


        /// <summary>
        /// 加载设置
        /// </summary>
        public void configLoad() => configManager.load();
        /// <summary>
        /// 设置屏幕分辨率
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">长度</param>
        public void setResolution(int width, int height)
        {
            configManager.configData.width = width;
            configManager.configData.height = height;
            Screen.SetResolution(width, height, Screen.fullScreenMode);
        }
        /// <summary>
        /// 设置屏幕分辨率
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">长度</param>
        /// <param name="isFull">全屏</param>
        public void setResolution(int width, int height, bool isFull)
        {
            configManager.configData.width = width;
            configManager.configData.height = height;
            configManager.configData.isFull = isFull;
            Screen.SetResolution(width, height, isFull);
        }
    }
}


