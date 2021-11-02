using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WAIT : MonoBehaviour
{
    public static IEnumerator onWaiting(float time,UnityEngine.Events.UnityAction action){
        yield return new WaitForSeconds(time);
        action();
    }
}
