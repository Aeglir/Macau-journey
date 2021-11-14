
using Universal;

namespace Scripts.MainMenu.LoadDialog;

public class LoadingDialog : BottonPanel
{
    private new void Awake()
    {
        gameObject.SetActive(false);
        base.Awake();
    }
    new void Start()
    {
        settingListerners(() => switchActive());
        base.Start();
    }

    public void switchActive()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
