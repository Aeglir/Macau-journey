using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T1, T2> : MonoBehaviour where T1 : Component where T2 : Object
{
    protected static T1 _instance;
    public static T2 Instance { get => _instance as T2; }
}
