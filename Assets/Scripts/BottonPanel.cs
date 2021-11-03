using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class BottonPanel : MonoBehaviour
{
    protected List<Button> buttonList;
    protected List<UnityEngine.Events.UnityAction> callBackList;

    protected void Awake()
    {
        //获取子组件列表
        buttonList = new List<Button>(this.transform.GetComponentsInChildren<Button>());
    }
    protected void Start()
    {
        //添加监听器
        int len=buttonList.Count<callBackList.Count?buttonList.Count:callBackList.Count;
        for(int i=0;i<len;i++){
            buttonList[i].onClick.AddListener(callBackList[i]);
        }
    }

    //设置回调函数列表
    protected bool settingListerners(params UnityEngine.Events.UnityAction[] call){
        if((callBackList = new List<UnityEngine.Events.UnityAction>(call))!=null){
            return true;
        }else{
            return false;
        }
    }
}
