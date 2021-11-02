using UnityEngine;
using UnityEngine.EventSystems;
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
            settingListerners(() =>
            {
                Button sourceButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
                StartCoroutine(WAIT.onWaiting(1.0f, () => { SceneManager.LoadScene("GameScene"); }));
            }, () =>
            {
                StartCoroutine(WAIT.onWaiting(1.0f, () => { SceneManager.LoadScene("ContinueScene"); }));
            }, () =>
            {
                StartCoroutine(WAIT.onWaiting(1.0f, () => { SceneManager.LoadScene("ConfigScene"); }));
            }, () =>
            {
                StartCoroutine(WAIT.onWaiting(1.0f, () => { Application.Quit(); }));
            });

        }
    }
}