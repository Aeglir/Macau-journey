using Universal;

namespace Scripts.MainMenu.LoadDialog
{
    public class LoadingDialog : ButtonPanel
    {
        new void Start()
        {
            //设置监听器
            settingListerners(() => switchActive());
            base.Start();
        }

        //切换active状态
        public void switchActive() => gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}