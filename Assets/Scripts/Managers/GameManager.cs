using UnityEngine;

namespace Managers
{
    [DefaultExecutionOrder(-1)]
    public class GameManager : MonoBehaviour
    {
        #region c# properties
        //对象单例化
        private static GameManager instance = null;
        public static GameManager Instance
        {
            get => instance;
        }
        #endregion

        #region c# vriables
        //数据存储管理器
        [SerializeField]
        private ArchiveManager archiveManager;
        [SerializeField]
        private ConfigManager configManager;
        [SerializeField]
        private AudioManager audioManager;
        #endregion
        private void Awake()
        {
            Application.targetFrameRate = 60;
            //若已存在实例则销毁当前新建的gameObject
            if (Instance)
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
    }
}


