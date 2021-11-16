using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.ConfigMenu
{
    public class FullScrreen : MonoBehaviour
    {
        /// <summary>
        /// 开关控件
        /// </summary>
        [SerializeField]
        private Toggle toggle;

        private void Awake()
        {
            //根据当前是否全屏初始化开关
            toggle.isOn = Screen.fullScreen;
        }
    }
}


