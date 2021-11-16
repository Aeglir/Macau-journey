using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Universal;
/// <summary>
/// 存档加载窗口
/// </summary>
public class LoadingWindow : ButtonPanel
{
    #region c# vriables
    /// <summary>
    /// Text控件列表
    /// </summary>
    [SerializeField]
    private List<Text> textList;
    /// <summary>
    /// 存档名字典
    /// </summary>
    private Dictionary<string, int> saveDictionary;
    #endregion
    private void Awake()
    {
        //初始化字典
        saveDictionary = new Dictionary<string, int>();
        string[] saveNames = ArchiveManager.Instance.getSaveNames();
        for (int i = 0; i < saveNames.Length; i++)
        {
            saveDictionary.Add(saveNames[i], i);
        }
        //根据存档数量初始化控件active状态
        for (int i = 0; i < saveDictionary.Count; i++)
        {
            buttonList[i].enabled = true;
            textList[i].text = saveDictionary.Keys.ToList()[i];
        }
    }
    private new void Start()
    {
        //初始化回调函数列表
        callBackList = new List<UnityEngine.Events.UnityAction>();
        for (int i = 0; i < buttonList.Count; i++)
        {
            callBackList.Add(getSaveName);
        }
        base.Start();
    }
    /// <summary>
    /// 获取存档名称回调函数
    /// </summary>
    private void getSaveName()
    {
        //根据当前点击按钮获取索引并设置存档管理器的索引
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        ArchiveManager.Instance.position = buttonList.IndexOf(button);
        //设置好索引后加载存档
        ArchiveManager.Instance.loadGame();
    }


}
