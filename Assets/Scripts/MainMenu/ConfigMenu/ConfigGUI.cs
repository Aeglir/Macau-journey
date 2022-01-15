using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;

namespace MainMenu.Config
{
    public class ConfigGUI : MonoBehaviour
    {
        private List<string> resolutionList;
        private int position;
        [SerializeField]
        private Text resolutionText;
        [SerializeField]
        private Slider mainSlider;
        [SerializeField]
        private Slider bgmSlider;
        [SerializeField]
        private Slider seSlider;
        [SerializeField]
        private Toggle maintoogle;
        [SerializeField]
        private Toggle bgmtoogle;
        [SerializeField]
        private Toggle setoogle;
        [SerializeField]
        private Toggle fulltoogle;
        private void Awake()
        {
            //初始化分辨率元组
            resolutionList = new List<string>();
            resolutionList.Add(ConfigManager.DefaultDPI);
            resolutionList.Add(ConfigManager.HD);
            //获取当前列表位置
            // position = getPosition();
            //根据位置初始化文本内容
            // resolutionText.text = ConfigManager.Instance.width + link + ConfigManager.Instance.height;
            // mainSlider.value = ConfigManager.Instance.mainVolume;
            // bgmSlider.value = ConfigManager.Instance.bgmVolume;
            // seSlider.value = ConfigManager.Instance.seVolume;
            // maintoogle.isOn = ConfigManager.Instance.MVEnable;
            // bgmtoogle.isOn = ConfigManager.Instance.BGMEnable;
            // setoogle.isOn = ConfigManager.Instance.SEEnable;
        }

        private void OnEnable()
        {
            ConfigManager manager = ConfigManager.Instance;
            if (manager != null)
            {
                manager.init();
                initGUI(manager);
                position = getPosition();
            }
        }
        private void initGUI(ConfigManager manager)
        {
            fulltoogle.isOn = manager.IsFull;
            resolutionText.text = manager.DPI;
        }
        public void leftButtonClick()
        {
            position--;
            switchResolution(resolutionList[position % resolutionList.Count]);
        }
        public void rightButtonClick()
        {
            position++;
            switchResolution(resolutionList[position % resolutionList.Count]);
        }

        private int getPosition()
        {
            //根据当前屏幕数据初始化位置
            switch (ConfigManager.Instance.DPI)
            {
                case "1280X720":
                    return 0 + resolutionList.Count;
                case "1920X1080":
                    return 1 + resolutionList.Count;
                default:
                    return 0 + resolutionList.Count;
            }
        }

        private void switchResolution(string dpi)
        {
            ConfigManager manager = ConfigManager.Instance;
            //确保位置数值大小不为负数
            if (position < resolutionList.Count)
            {
                position += resolutionList.Count;
            }
            //更改文本内容
            resolutionText.text = dpi;
            if (manager != null)
            {
                manager.changeSetting(ConfigManager.TYPE.DPI, dpi);
            }
        }

        public void switchFullscreen(bool value)
        {
            ConfigManager manager = ConfigManager.Instance;
            if (manager != null)
            {
                manager.changeSetting(ConfigManager.TYPE.FULLSCREEN, value);
            }
        }

        public void confirmSetting()
        {
            ConfigManager manager = ConfigManager.Instance;
            if (manager != null)
            {
                manager.confirmSetting();
            }
        }

        public void cancelSetting()
        {
            ConfigManager manager = ConfigManager.Instance;
            if (manager != null)
            {
                manager.cancelSetting();
            }
        }
    }
}


