using System;
using UnityEngine;
using UnityEngine.Events;
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
        [Header("存档管理器")]
        private ArchiveManager archiveManager;
        [SerializeField]
        [Header("设置管理器")]
        private ConfigManager configManager;
        [SerializeField]
        [Header("音频管理器")]
        private AudioManager audioManager;
        [SerializeField]
        [Header("开始游戏事件")]
        private UnityEvent newGameEvent;
        [SerializeField]
        [Header("自动保存事件")]
        private UnityEvent autoSaveEvent;
        private bool isNew;
        public bool isNewGame
        {
            get
            {
                return isNew;
            }
        }
        #endregion
        private void Awake()
        {
            //若已存在实例则销毁当前新建的gameObject
            if (Instance)
            {
                DestroyImmediate(gameObject);
                return;
            }

            instance = this;
            isNew = false;

            //防止被销毁
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// 开始新游戏
        /// </summary>
        public void newGame()
        {
            isNew = true;
            if (newGameEvent != null)
            {
                newGameEvent.Invoke();
            }
        }
        /// <summary>
        /// 自动保存
        /// </summary>
        public void autoSave()
        {
            autoSaveEvent.Invoke();
        }
    }
}


