using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.ConfigMenu
{
    public class FullScrreen : MonoBehaviour
    {
        /// <summary>
        /// 开关控件
        /// </summary>
        public Toggle toggle;

        private void Awake()
        {
            //根据当前是否全屏初始化开关
            if (!GameManager.Instance.configManager.isFull)
            {
                toggle.isOn = false;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            //添加当开关数值方生改变时触发的监听器
            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener((isOn) => GameManager.Instance.configManager.isFull = isOn);
        }
    }
}


