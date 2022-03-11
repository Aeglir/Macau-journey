using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using UnityEngine.Audio;
using Managers.Config;

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
        private Toggle fulltoogle;
        [SerializeField]
        private AudioMixer audioMixer;
        private ConfigPresenter presenter;
        private void Awake()
        {
            //初始化分辨率元组
            resolutionList = new List<string>();
            resolutionList.Add(ConfigManager.DefaultDPI);
            resolutionList.Add(ConfigManager.HD);
        }
        private void OnEnable()
        {
            ConfigManager manager = GameManager.Instance.ConfigManager;
            if (manager != null)
            {
                presenter = manager.presenter;
                initGUI();
                position = getPosition();
            }
        }
        private int getPosition()
        {
            //根据当前屏幕数据初始化位置
            switch (presenter.DPI)
            {
                case "1280X720":
                    return 0 + resolutionList.Count;
                case "1920X1080":
                    return 1 + resolutionList.Count;
                default:
                    return 0 + resolutionList.Count;
            }
        }
        private void initGUI()
        {
            fulltoogle.isOn = presenter.IsFull;
            resolutionText.text = presenter.DPI;
            mainSlider.value = presenter.MAINVOLUME;
            bgmSlider.value = presenter.BGM;
            seSlider.value = presenter.SE;
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

        private void switchResolution(string dpi)
        {
            //确保位置数值大小不为负数
            if (position < resolutionList.Count)
            {
                position += resolutionList.Count;
            }
            //更改文本内容
            resolutionText.text = dpi;
            if (presenter != null)
            {
                presenter.ChangeSetting(ConfigManager.TYPE.DPI, dpi);
            }
        }
        public void switchFullscreen(bool value)
        {
            if (presenter != null)
            {
                presenter.ChangeSetting(ConfigManager.TYPE.FULLSCREEN, value);
            }
        }
        public void confirmSetting()
        {
            if (presenter != null)
            {
                presenter.ConfirmSetting();
            }
        }
        public void cancelSetting()
        {
            if (presenter != null)
            {
                presenter.CancelSetting();
            }
        }
        public void changeMainVolume(float value)
        {
            if (presenter != null)
            {
                audioMixer.SetFloat("MAINVolume", SettingUpdater.transitionToVolume(value));
                presenter.ChangeSetting(ConfigManager.TYPE.MAINVOLUME, value);
            }
        }
        public void changeBgmVolume(float value)
        {
            if (presenter != null)
            {
                audioMixer.SetFloat("BGMVolume", SettingUpdater.transitionToVolume(value));
                presenter.ChangeSetting(ConfigManager.TYPE.BGM, value);
            }
        }
        public void changeSeVolume(float value)
        {
            if (presenter != null)
            {
                audioMixer.SetFloat("SEVolume", SettingUpdater.transitionToVolume(value));
                presenter.ChangeSetting(ConfigManager.TYPE.SE, value);
            }
        }
    }
}


