using UnityEngine;
using UnityEngine.UI;

namespace Scripts.MainMenu.ConfigMenu
{
    public class FullScrreen : MonoBehaviour
    {
        Toggle toggle;

        private void Awake()
        {
            //获取开关控件
            toggle = GetComponent<Toggle>();
            //根据当前是否全屏初始化开关
            if (!Screen.fullScreen)
            {
                toggle.isOn = false;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            //添加当开关数值方生改变时触发的监听器
            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener((isOn) =>
            {
            //切换全屏
            Screen.SetResolution(Screen.width, Screen.height, !Screen.fullScreen);
            });
        }
    }
}
