using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace MainMenu
{
    public class MainMenuButton : BottonPanel
    {

        new private void Awake()
        {
            base.Awake();
            //设置回调函数
            settingListerners(StartButtonClicked, ContinueButtonClicked, ConfigButtonClicked, ExitButtonClicked);

        }
        new private void Start()
        {
            base.Start();
        }

        void StartButtonClicked()
        {
            //切换至游戏场景
            SceneManager.LoadScene("GameScene");
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
        void ExitButtonClicked()
        {
            //退出应用
            Application.Quit();
        }
    }
}