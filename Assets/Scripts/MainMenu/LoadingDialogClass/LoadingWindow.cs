using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 存档加载窗口
/// </summary>
public class LoadingWindow : MonoBehaviour
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
    private List<Button> buttonList;
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
    /// <summary>
    /// 获取存档名称回调函数
    /// </summary>
    public void getSaveName(int index)
    {
        ArchiveManager.Instance.position = index;
        //设置好索引后加载存档
        ArchiveManager.Instance.loadGame();
    }


}
