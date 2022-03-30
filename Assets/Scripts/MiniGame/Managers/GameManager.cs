using System;
using UnityEngine;
using UnityEngine.Events;
namespace Managers
{
    [DefaultExecutionOrder(-1)]
    public class GameManager : Singleton<GameManager,GameManager>
    {
        #region private fields
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
        [Header("输入管理器")]
        private InputManager inputManager;
        [SerializeField]
        [Header("控制台管理器")]
        private ConsoleManager consoleManager;
        [Header("小游戏管理器")]
        private MiniGameManager miniGameMananger;
        [SerializeField]
        [Header("开始游戏事件")]
        private UnityEvent newGameEvent;
        [SerializeField]
        [Header("自动保存事件")]
        private UnityEvent autoSaveEvent;
        private bool isNew;
        #endregion
        #region properties
        public bool isNewGame { get => isNew; }
        public ArchiveManager ArchiveManager { get => archiveManager; }
        public ConfigManager ConfigManager { get => configManager; }
        public AudioManager AudioManager { get => audioManager; }
        public InputSetable InputManager { get => inputManager; }
        public ConsolePresenter ConsoleManager { get => consoleManager; }
        public UnityEvent NewGameEvent { get => newGameEvent; }
        public UnityEvent AutoSaveEvent { get => autoSaveEvent; }
        #endregion
        #region private methods
        private void Awake()
        {
            //若已存在实例则销毁当前新建的gameObject
            if (_instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            _instance = this;
            //防止被销毁
            DontDestroyOnLoad(gameObject);
<<<<<<< HEAD:Assets/Scripts/Managers/GameManager.cs
            InstallCommand();
=======
            miniGameMananger = new MiniGameManager();
            InstallCommand();
        }
        private async void InstallCommand()
        {
            await System.Threading.Tasks.Task.Delay(100);
            ConsoleManager.RegisterCommand(AutoSave);
            ConsoleManager.RegisterCommand(miniGameMananger.VolunteerGameStart);
        }
        private void OnDestroy()
        {
            ConsoleManager.RemoveCommand(AutoSave);
            ConsoleManager.RemoveCommand(miniGameMananger.VolunteerGameStart);
>>>>>>> MFK:Assets/Scripts/MiniGame/Managers/GameManager.cs
        }
        #endregion
        /// <summary>
        /// 开始新游戏
        /// </summary>
        public void NewGame()
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
        public void AutoSave()
        {
            autoSaveEvent.Invoke();
        }
    }
}


