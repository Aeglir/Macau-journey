using Scripts.MainMenu.LoadDialog;
using Universal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Managers;

namespace MainMenu
{
    public class MainMenuButton : ButtonPanel
    {
        private float time;//等待时间
        public LoadingDialog loadingPanel;
        private void Awake()
        {
            //设置回调函数
            time = 0.5f;
            settingListerners(() =>
            {
                setPressed();
                StartCoroutine(WAIT.onWaiting(time, () => { 
                    GameManager.Instance.newGame();
                    SceneManager.LoadScene("GameScene"); }));
                reloadButton();
            }, () =>
            {
                setPressed();
                loadingPanel.switchActive();
                reloadButton();
            }, () =>
            {
                setPressed();
                StartCoroutine(WAIT.onWaiting(time, () => { SceneManager.LoadScene("ConfigScene"); }));
                reloadButton();
            }, () =>
            {
                setPressed();
                StartCoroutine(WAIT.onWaiting(time, () => { Application.Quit(); }));
                reloadButton();
            });
        }

        private void setPressed()
        {
            //禁用按钮防止二次点击
            foreach (Button bt in buttonList)
            {
                bt.enabled = false;
            }
            //从全局点击事件中获取正在被选择的按钮
            Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            //禁用按钮后会重置所有animator trigger，因此需要激活trigger
            button.animator.SetTrigger("Pressed");
        }

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


