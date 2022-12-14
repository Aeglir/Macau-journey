using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Managers;
using System.Collections.Generic;

namespace MainMenu
{
    public class MainMenuButton : MonoBehaviour
    {
        /// <summary>
        /// 按钮列表
        /// </summary>
        [SerializeField]
        private List<Button> buttonList;
        /// <summary>
        /// 等待时间
        /// </summary>
        [SerializeField]
        private int time;
        private bool isPressed;
        /// <summary>
        /// 载入窗体
        /// </summary>
        [SerializeField]
        private GameObject configPanel;
        [Header("加载窗体")]
        public GameObject loadPanel;
        public async void startButton()
        {
            if (isPressed)
            {
                return;
            }
            isPressed=true;
            await System.Threading.Tasks.Task.Delay(time);
            AsyncOperation operation =  SceneManager.LoadSceneAsync(1);
            while(!operation.isDone)
            {
                await System.Threading.Tasks.Task.Yield();
            }
            GameManager.Instance.NewGame();
            isPressed=false;
        }
        public async void continueButton()
        {
            if (isPressed || GameManager.Instance.ArchiveManager == null)
            {
                return;
            }
            isPressed=true;
            await System.Threading.Tasks.Task.Delay(time);
            loadPanel.SetActive(true);
            isPressed=false;
        }
        public void collectionButton()
        {
            // if (ArchiveManager.Instance == null)
            // {
            //     return;
            // }
            // setPressed(button);
            // StartCoroutine(WAIT.onWaiting(time, () =>
            // {
            //     ArchiveManager.Instance.enableLoadScene(false);
            // }));
            // reloadButton();
        }
        public async void configButton()
        {
            if (isPressed)
            {
                return;
            }
            isPressed=true;
            await System.Threading.Tasks.Task.Delay(time);
            configPanel.SetActive(!configPanel.activeSelf);
            isPressed=false;
        }
        public void ExitButton()
        {
            Application.Quit();
        }
    }
}


