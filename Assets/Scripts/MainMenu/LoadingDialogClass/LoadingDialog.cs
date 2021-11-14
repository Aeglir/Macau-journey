
using Universal;

namespace Scripts.MainMenu.LoadDialog
{
    public class LoadingDialog : ButtonPanel
    {
        private void Awake()
        {
            //初始化active
            gameObject.SetActive(false);
        }
        new void Start()
        {
            //设置监听器
            settingListerners(() => switchActive());
            base.Start();
        }

        public void switchActive()
        {
            //切换active状态
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }
    }
}