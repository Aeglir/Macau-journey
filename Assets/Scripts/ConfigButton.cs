using UnityEngine.SceneManagement;
using Universal;

public class ConfigButton : ButtonPanel
{

    private void Awake()
    {
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