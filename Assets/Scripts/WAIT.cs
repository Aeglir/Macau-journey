using System.Collections;
using UnityEngine;

public class WAIT : MonoBehaviour
{
    public static IEnumerator onWaiting(float time, UnityEngine.Events.UnityAction action)
    {
        //协同等待time秒
        yield return new WaitForSeconds(time);
        //等待完成后可选执行action（）函数
        if (action != null)
            action();
    }
}
