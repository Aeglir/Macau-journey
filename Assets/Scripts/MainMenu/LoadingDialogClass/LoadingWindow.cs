using System.Collections.Generic;
using Managers;
using UnityEngine.UI;
using Universal;

public class LoadingWindow : ButtonPanel
{
    public List<Text> textList;
    private int saveCount;

    private new void Start()
    {
        //base.Start();
        //saveCount = ConfigManager.Instance.getVriable<int>(ConfigManager.SAVECOUNT);
    }
}
