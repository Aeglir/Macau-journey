using System;
using System.Collections.Generic;
using Universal;
using UnityEngine;
using UnityEngine.UI;
using Managers;

namespace MainMenu.ConfigMenu
{
    public class ResolutionSwitcher : ButtonPanel
    {
        private List<Tuple<int, int>> resolutionList;
        private char link;
        private int position;
        private Text resolutionText;
        private void Awake()
        {
            //初始化分辨率元组
            resolutionList = new List<Tuple<int, int>>();
            resolutionList.Add(new Tuple<int, int>(1920, 1080));
            resolutionList.Add(new Tuple<int, int>(1280, 720));
            //初始化连接符号
            link = 'X';
            //获取分辨率显示文本控件
            resolutionText = GetComponentsInChildren<Text>()[1];
            //获取当前列表位置
            position = getPosition();
            //根据位置初始化文本内容
            resolutionText.text = resolutionList[position % resolutionList.Count].Item1
             + link.ToString() + resolutionList[position % resolutionList.Count].Item2;
        }
        new void Start()
        {
            //设置向左切换的监听器
            settingListerners(() =>
            {
            //列表位置减一后更改分辨率和分辨率文本
            position--;
                switchResolution(resolutionList[position % resolutionList.Count]);
            },
            //设置向右切换的监听器
            () =>
            {
            //列表位置加一后更改分辨率和分辨率文本
            position++;
                switchResolution(resolutionList[position % resolutionList.Count]);
            }
            );
            base.Start();
        }

        private int getPosition()
        {
            //根据当前屏幕数据初始化位置
            switch (Screen.width)
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
            //更新全局设置值
            GameManager.Instance.configManager.configData.width = resoluTuple.Item1;
            GameManager.Instance.configManager.configData.height = resoluTuple.Item2;
            //根据元组切换分辨率
            switch (resoluTuple.Item1)
            {
                case 1920:
                    Screen.SetResolution(1920, 1080, Screen.fullScreen);
                    break;
                case 1280:
                    Screen.SetResolution(1280, 720, Screen.fullScreen);
                    break;
                default:
                    Screen.SetResolution(1920, 1080, Screen.fullScreen);
                    break;
            }
        }
    }
}


