using System.Collections;
using MainMenu.ConfigMenu;
using Managers;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Universal;

public class ConfigButton : ButtonPanel
{
    public ResolutionSwitcher switcher;
    public Toggle fullToggle;
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
        SceneManager.LoadScene("MAINMENU");
        ConfigManager.Instance.setResolution(switcher.getWidth(), switcher.getHeight(), fullToggle.isOn);
        ConfigManager.Instance.saveData();
    }
}