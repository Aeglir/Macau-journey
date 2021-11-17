using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;

namespace MainMenu.ConfigMenu
{
    public class ConfigPanel : MonoBehaviour
    {
        private List<Tuple<int, int>> resolutionList;
        [SerializeField]
        private string link;
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
            resolutionList = new List<Tuple<int, int>>();
            resolutionList.Add(new Tuple<int, int>(1920, 1080));
            resolutionList.Add(new Tuple<int, int>(1280, 720));
            //获取当前列表位置
            position = getPosition();
            //根据位置初始化文本内容
            resolutionText.text = ConfigManager.Instance.width + link + ConfigManager.Instance.height;
            mainSlider.value = ConfigManager.Instance.mainVolume;
            bgmSlider.value = ConfigManager.Instance.bgmVolume;
            seSlider.value = ConfigManager.Instance.seVolume;
            maintoogle.isOn = ConfigManager.Instance.MVEnable;
            bgmtoogle.isOn = ConfigManager.Instance.BGMEnable;
            setoogle.isOn = ConfigManager.Instance.SEEnable;
            fulltoogle.isOn = ConfigManager.Instance.isFull;
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
            switch (ConfigManager.Instance.width)
            {
                case 1920:
                    return 0 + resolutionList.Count;
                case 1280:
                    return 1 + resolutionList.Count;
                default:
                    return 0 + resolutionList.Count;
            }
        }

        private void switchResolution(Tuple<int, int> resoluTuple)
        {
            //确保位置数值大小不为负数
            if (position < resolutionList.Count)
            {
                position += resolutionList.Count;
            }
            //更改文本内容
            resolutionText.text = resoluTuple.Item1 + link.ToString() + resoluTuple.Item2;
            ConfigManager.Instance.width = resoluTuple.Item1;
            ConfigManager.Instance.height = resoluTuple.Item2;
        }
    }
}


