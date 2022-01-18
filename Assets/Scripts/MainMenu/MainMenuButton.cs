using Universal;
using UnityEngine;
using UnityEngine.EventSystems;
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
        private float time = 0.5f;
        /// <summary>
        /// 载入窗体
        /// </summary>
        [SerializeField]
        private GameObject configPanel;
        [Header("加载窗体")]
        public GameObject loadPanel;
        public void startButton(Button button)
        {
            setPressed(button);
            StartCoroutine(WAIT.onWaiting(time, () =>
            {
                GameManager.Instance.newGame();
                // SceneManager.LoadSceneAsync("GameScene");
            }));
            reloadButton();
        }
        public void continueButton(Button button)
        {
            if (ArchiveManager.Instance == null)
            {
                return;
            }
            setPressed(button);
            StartCoroutine(WAIT.onWaiting(time, () =>
            {
                loadPanel.SetActive(true);
            }));
            reloadButton();
        }
        public void collectionButton(Button button)
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
        public void configButton(Button button)
        {
            setPressed(button);
            StartCoroutine(WAIT.onWaiting(time, () => { configPanel.SetActive(!configPanel.activeSelf); }));
            reloadButton();
        }
        public void exitButton() => Application.Quit();
        /// <summary>
        /// 禁用按钮防止二次点击并激活animator trigger
        /// </summary>
        private void setPressed(Button button)
        {
            //禁用按钮防止二次点击
            foreach (Button bt in buttonList)
            {
                bt.enabled = false;
            }
            //禁用按钮后会重置所有animator trigger，因此需要激活trigger
            button.animator.SetTrigger("Pressed");
        }
        /// <summary>
        /// 重置按钮状态为开启
        /// </summary>
        private void reloadButton()
        {
            //重置按钮
            foreach (Button bt in buttonList)
            {
                bt.enabled = true;
            }
        }
    }
}


