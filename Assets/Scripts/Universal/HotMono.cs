using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotMono : MonoBehaviour
{
#if DEBUGMODE
    public bool debug;
#endif
    protected void Awake()
    {
        OnAwake();
        OnAwakeAsync();
#if DEBUGMODE
        if(!debug)
            gameObject.SetActive(false);
#else
        gameObject.SetActive(false);
#endif
        AwakeFinish();
    }
    protected virtual void OnAwake() { }
    protected virtual void OnAwakeAsync() { }
    protected virtual void AwakeFinish() { }
}
