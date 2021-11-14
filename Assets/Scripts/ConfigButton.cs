using UnityEngine.SceneManagement;
using Scripts.Universal;

public class ConfigButton : BottonPanel
{

    new private void Awake()
    {
        base.Awake();
        //���ûص�����
        settingListerners(ReturnButtonClicked);

    }
    new private void Start()
    {
        base.Start();
    }

    void ReturnButtonClicked()
    {
        //�л�����Ϸ����
        SceneManager.LoadScene("MAINMENU");
    }
}