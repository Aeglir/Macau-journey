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

        public void newGame(){
            //开始新游戏则初始化存档
            archiveManager.init();
        }

        public void gameSave()
        {
            //存档。。。
            archiveManager.saveGame();
        }

        public bool gameLoad()
        {
            //载入存档
            return false;
        }
    }
}


