using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class MainMenuButton : BottonPanel
    {      
        private float time;//等待时间
        new private void Awake()
        {
            base.Awake();
            //设置回调函数
            time=1.5f;
            settingListerners(() =>
            {
                setPressed();
                StartCoroutine(WAIT.onWaiting(time, () => { SceneManager.LoadScene("GameScene"); }));
            }, () =>
            {
                setPressed();
                StartCoroutine(WAIT.onWaiting(time, () => { SceneManager.LoadScene("ContinueScene"); }));
            }, () =>
            {
                setPressed();
                StartCoroutine(WAIT.onWaiting(time, () => { SceneManager.LoadScene("ConfigScene"); }));
            }, () =>
            {
                setPressed();
                StartCoroutine(WAIT.onWaiting(time, () => { Application.Quit(); }));
            });

        }

        private void setPressed()
        {
            //从全局点击事件中获取正在被选择的按钮
            Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            //禁用按钮防止二次点击
            button.enabled = false;
            //禁用按钮后会重置所有animator trigger，因此需要激活trigger
            button.animator.SetTrigger("Pressed");
        }
    }
}