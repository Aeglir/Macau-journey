using Managers;
using UnityEngine.SceneManagement;
using Universal;

public class ConfigButton : ButtonPanel
{

    private void Awake()
    {
        settingListerners(SaveButtonClicked, ReturnButtonClicked);

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

    void SaveButtonClicked()
    {
        GameManager.Instance.configSave();
        SceneManager.LoadScene("MAINMENU");
    }
}