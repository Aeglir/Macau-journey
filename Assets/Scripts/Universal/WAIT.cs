using System.Collections;
using UnityEngine;

namespace Universal
{
    /// <summary>
    /// 协同等待
    /// </summary>
    public sealed class WAIT : MonoBehaviour
    {
        /// <summary>
        /// 等待函数，等待指定时间后自动执行传入的回调函数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerator onWaiting(float time, UnityEngine.Events.UnityAction action)
        {
            //协同等待time秒
            yield return new WaitForSeconds(time);
            //等待完成后可选执行action（）函数
            if (action != null)
                action();
        }
    }
}


