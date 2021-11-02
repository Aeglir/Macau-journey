using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ConfigButton : BottonPanel
{

    new private void Awake()
    {
        base.Awake();
        //设置回调函数
        settingListerners(ReturnButtonClicked);

    }
    new private void Start()
    {
        base.Start();
    }

    void ReturnButtonClicked()
    {
        //切换至游戏场景
        SceneManager.LoadScene("MAINMENU");
    }
}