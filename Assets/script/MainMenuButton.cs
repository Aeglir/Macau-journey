using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace MainMenu
{
    public class MainMenuButton : MonoBehaviour
    {
        List<Button> ButtonList;

        private void Awake()
        {
            //获取子组件列表
            ButtonList = new List<Button>(this.transform.GetComponentsInChildren<Button>());
        }
        private void Start()
        {
            //添加监听器
            ButtonList[0].onClick.AddListener(StartButtonClicked);
            ButtonList[1].onClick.AddListener(ContinueButtonClicked);
            ButtonList[2].onClick.AddListener(ConfigButtonClicked);
            ButtonList[3].onClick.AddListener(QuitButtonClicked);
        }

        void StartButtonClicked()
        {
            //切换至游戏场景
            SceneManager.LoadScene("GameScene");
        }
        void QuitButtonClicked()
        {
            //退出应用
            Application.Quit();
        }
        void ContinueButtonClicked()
        {
            //打开读档界面
            SceneManager.LoadScene("ContinueScene");
        }
        void ConfigButtonClicked()
        {
            //打开设置界面
            SceneManager.LoadScene("ConfigScene");
        }
    }
}