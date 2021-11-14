using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Universal
{
    /// <summary>
    /// 自动获取布局中的所有button compotent
    /// 可按顺序设置监听器
    /// 可返回指定button
    /// </summary>
    abstract public class ButtonPanel : MonoBehaviour
    {

        #region c# variables
        public List<Button> buttonList;
        protected List<UnityEngine.Events.UnityAction> callBackList;

        #endregion

        #region C# base Fuction
        /// <summary>
        /// 根据callBackList为所有button设置onClick函数
        /// 按钮不足时多余的回调将被抛弃
        /// </summary>
        protected void Start()
        {
            //添加监听器
            int len = buttonList.Count < callBackList.Count ? buttonList.Count : callBackList.Count;
            for (int i = 0; i < len; i++)
            {
                buttonList[i].onClick.RemoveAllListeners();
                buttonList[i].onClick.AddListener(callBackList[i]);
            }
        }

        #endregion

        /// <summary>
        /// 设置callBackList中的回调函数
        /// 支持传入多个形参并且将被顺序设置
        /// </summary>
        /// <param name="call"></param>
        /// <returns></returns>
        protected bool settingListerners(params UnityEngine.Events.UnityAction[] call)
        {
            if ((callBackList = new List<UnityEngine.Events.UnityAction>(call)) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取指定索引的按钮
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Button getButton(int position)
        {
            return buttonList[position];
        }

        /// <summary>
        /// 返回指定索引的回调函数
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public UnityEngine.Events.UnityAction getAction(int position)
        {
            return callBackList[position];
        }
    }
}

