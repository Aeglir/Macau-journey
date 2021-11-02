using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        private void SwitchScene(string sceneName){
            Invoke("SwitchToGameScene",1);
        }

        void StartButtonClicked()
        {
            //切换至游戏场景
            Button button = getSourceButton(StartButtonClicked);
            Invoke("SwitchToGameScene",1);
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