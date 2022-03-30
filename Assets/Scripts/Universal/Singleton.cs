using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T1, T2> : MonoBehaviour where T1 : Component where T2 : Object
{
    protected static T1 _instance;
    public static T2 Instance
    {
        get
        {
            if (_instance == null)
            {
                var objs = FindObjectsOfType(typeof(T1)) as T1[];
                if (objs.Length > 0)
                    _instance = objs[0];
                if (objs.Length > 1)
                {
                    Debug.LogError("There is more than one " + typeof(T1).Name + "in the scene.");
                }
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = obj.AddComponent<T1>();
                }
            }
            return _instance as T2;
        }
    }
}
