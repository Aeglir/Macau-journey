using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ChoiceButton : MonoBehaviour
{
    [Header("按钮组件")]
    public Button button1; //轮盘选项
    public Button button2;
    public Button button3;
    public Button button4;

    //对话选项
    private class Choice
    {
        public Sprite isChosen;//被选中
        public Sprite notChosen;//未被选中
        public int score;//分数
        public int flag;//是否被选择过
    }

    [Header("选项组件")]
    public List<Sprite> isChosenList = new List<Sprite>(); //选中选项图片列表
    public List<Sprite> notChosenList = new List<Sprite>(); //未选中选项图片列表
    private List<int> choiceFirstNO = new List<int>();//本次轮盘中被选中的6个对话的列表
    private List<int> choiceSecondNO = new List<int>();//轮盘中每轮被选中的4个对话的列表
    private Choice[] Choices = new Choice[12];

    [Header("标志组件")]
    private int isChosen = 1; //轮盘标志位，默认第一个按钮选中
    public int turn = 1; //对话轮次标志位，默认为第一轮
    private float changeSpeed = 1; //轮盘变换速度

    [Header("分数组件")]
    private int score = 0; //玩家得分
    private int npcScore;

    [Header("立绘组件")]
    public Image npc;
    public Image player;
    private Sprite npcNormal;//正常表情
    private Sprite playerNormal;
    private Sprite npcFail;//失败表情
    private Sprite playerFail;
    private Sprite npcSucceed;//成功表情
    private Sprite playerSucceed;

    [Header("倒计时组件")]
    private int TotalTime = 30;//30s倒计时

    [Header("画布组件")]
    public Canvas GameCanvas;

    // Start is called before the first frame update
    void Start()
    {
        var NPCImageList = LoadData();
        var PlayerImageList = LoadPlayerData();

        // 绑定npc图片
        npcNormal = NPCImageList[0];
        npcSucceed = NPCImageList[1];
        npcFail = NPCImageList[2];

        //绑定player图片
        playerNormal = PlayerImageList[0];
        playerSucceed = PlayerImageList[1];
        playerFail = PlayerImageList[2];

        npc.sprite = npcNormal;//设置默认表情
        player.sprite = playerNormal;

        //初始化选项数据
        for (int i = 0; i < 12; i++)
        {
            Choices[i] = new Choice();
            Choices[i].isChosen = isChosenList[i];//绑定选中图片
            Choices[i].notChosen = notChosenList[i];//绑定未选中图片
            //设置对话分数
            if (i <= 3)
                Choices[i].score = 1;
            else if (i > 3 && i <= 7)
                Choices[i].score = 0;
            else Choices[i].score = -1;
            //设置对话标志,0表示未被选择过
            Choices[i].flag = 0;
        }

        SetFirstChoices();
        StartCoroutine("ChangeChosenButton");
        StartCoroutine("Time");//设置30s倒计时
    }

    // Update is called once per frame


    void Update()
    {
        //检测倒计时是否完毕
        if (TotalTime == 0)
        {
            GameCanvas.enabled = false;
        }
        //检测是否按下回车键或者鼠标
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            if (button1.GetComponent<UnityEngine.UI.Button>().interactable == true)
                RecordScore(0);
            if (button2.GetComponent<UnityEngine.UI.Button>().interactable == true)
                RecordScore(1);
            if (button3.GetComponent<UnityEngine.UI.Button>().interactable == true)
                RecordScore(2);
            if (button4.GetComponent<UnityEngine.UI.Button>().interactable == true)
                RecordScore(3);
            turn++;
            if (turn > 3)//分数判定并退出游戏
            {
                //分数达标
                if (score >= npcScore)
                {

                }
                GameCanvas.enabled = false;
            }
            StopCoroutine("ChangeChosenButton");
            ChangeChoiceImage(turn);//如果目前不是最后一轮对话，则更换对话内容
        }
    }

    //轮盘选项自动转换函数
    IEnumerator ChangeChosenButton()
    {
        //轮盘实现滚动
        while (true)
        {
            SetInteractable();//开始禁用所有按钮
            if (isChosen > 4) isChosen = 1;//如果标志位isChosen>4，则重置为1
            switch (isChosen)
            {
                case 1:
                    button1.GetComponent<UnityEngine.UI.Button>().interactable = true; //启用按钮
                    button1.image.sprite = Choices[choiceSecondNO[0]].isChosen;
                    break;
                case 2:
                    button2.GetComponent<UnityEngine.UI.Button>().interactable = true; //启用按钮
                    button2.image.sprite = Choices[choiceSecondNO[1]].isChosen;
                    break;
                case 3:
                    button3.GetComponent<UnityEngine.UI.Button>().interactable = true; //启用按钮
                    button3.image.sprite = Choices[choiceSecondNO[2]].isChosen;
                    break;
                case 4:
                    button4.GetComponent<UnityEngine.UI.Button>().interactable = true; //启用按钮
                    button4.image.sprite = Choices[choiceSecondNO[3]].isChosen;
                    break;
            }
            yield return new WaitForSeconds(changeSpeed);
            isChosen++; //标志位+1
        }
    }

    //禁用所有按钮函数
    private void SetInteractable()
    {
        button1.GetComponent<UnityEngine.UI.Button>().interactable = false;
        button2.GetComponent<UnityEngine.UI.Button>().interactable = false;
        button3.GetComponent<UnityEngine.UI.Button>().interactable = false;
        button4.GetComponent<UnityEngine.UI.Button>().interactable = false;
        button1.image.sprite = Choices[choiceSecondNO[0]].notChosen;
        button2.image.sprite = Choices[choiceSecondNO[1]].notChosen;
        button3.image.sprite = Choices[choiceSecondNO[2]].notChosen;
        button4.image.sprite = Choices[choiceSecondNO[3]].notChosen;
    }

    //倒计时函数
    IEnumerator Time()
    {
        while (TotalTime >= 0)
        {
            yield return new WaitForSeconds(1);
            TotalTime--;
        }
    }
    public System.Collections.Generic.List<Sprite> LoadPlayerData() => MiniGame.Volunteer.CharacterSystem.Instance.asset.Player.painting;
    public System.Collections.Generic.List<Sprite> LoadData()
    {
        //int tag = MiniGame.Volunteer.CharacterSystem.Instance.Tag;
        //tag
        int tag = 1;
        foreach (var info in MiniGame.Volunteer.CharacterSystem.Instance.asset.Npcs)
        {
            if (info.tag == tag)
            {
                npcScore = info._sorceNeed;
                return info.painting;
            }
        }
        return null;
    }

    //设置第一轮选项
    private void SetFirstChoices()
    {
        //选出本次轮盘中将会出现的6个对话选项
        int i = 0, mark1 = 0;
        while (i < 6) 
        {
            if (i<=1)
            {
                Random a = new Random(System.DateTime.Now.Millisecond);
                int RandKey = a.Next(0, 3);
                foreach (var z in choiceFirstNO)
                {
                    if (RandKey == z) mark1 = 1;
                }
                if (mark1 == 0)
                {
                    choiceFirstNO.Add(RandKey);
                    i++;
                }
                mark1 = 0;
                
            }
            else if (i>1 && i<=3)
            {
                Random a = new Random(System.DateTime.Now.Millisecond);
                int RandKey = a.Next(4, 7);
                foreach (var z in choiceFirstNO)
                {
                    if (RandKey == z) mark1 = 1;
                }
                if (mark1 == 0)
                {
                    choiceFirstNO.Add(RandKey);
                    i++;
                }
                mark1 = 0;
            }
            else
            {
                Random a = new Random(System.DateTime.Now.Millisecond);
                int RandKey = a.Next(8, 11);
                foreach (var z in choiceFirstNO)
                {
                    if (RandKey == z) mark1 = 1;
                }
                if (mark1 == 0)
                {
                    choiceFirstNO.Add(RandKey);
                    i++;
                }
                mark1 = 0;
            }
        }
        //选出第一轮对话中将会出现的4个对话选项
        int j = 0, mark2 = 0;
        while (j < 4)
        {
            Random a = new Random(System.DateTime.Now.Millisecond);
            int RandKey = a.Next(0, 5);
            foreach (var z in choiceSecondNO)
            {
                if (choiceFirstNO[RandKey] == z) mark2 = 1;
            }
            if (mark2 == 0)
            {
                choiceSecondNO.Add(choiceFirstNO[RandKey]);
                j++;
            }
            mark2 = 0;
        }
        //绑定第一轮对话中的选项
        button1.image.sprite = Choices[choiceSecondNO[0]].notChosen;
        button2.image.sprite = Choices[choiceSecondNO[1]].notChosen;
        button3.image.sprite = Choices[choiceSecondNO[2]].notChosen;
        button4.image.sprite = Choices[choiceSecondNO[3]].notChosen;
    }

    //选择选项后对应变化
    private void RecordScore(int i)
    {
        score += Choices[choiceSecondNO[i]].score;//分数变动
        //差劲对话
        if (Choices[choiceSecondNO[i]].score < 0)
        {
            TotalTime -= 5;
            npc.sprite = npcFail;
            player.sprite = playerFail;
        }
        //一般对话
        else if (Choices[choiceSecondNO[i]].score == 0)
        {
            npc.sprite = npcNormal;
            player.sprite = playerNormal;
        }
        //优秀对话
        else
        {
            npc.sprite = npcSucceed;
            player.sprite = playerSucceed;
        }
        Choices[choiceSecondNO[i]].flag = 1;//对对话的标志位进行改变，1为已选择过
    }

    //更换对话轮次选项函数
    private void ChangeChoiceImage(int turn)
    {
        int i = 0, mark = 0;
        switch (turn)
        {
            case 2:
                while (i < 4)
                {
                    Random a = new Random(System.DateTime.Now.Millisecond);
                    int RandKey = a.Next(0, 5);
                    for (int z = 0; z <= i; z++)
                    {
                        if (choiceFirstNO[RandKey] == choiceSecondNO[z])
                            mark = 1;
                    }
                    if (mark == 0 && Choices[choiceFirstNO[RandKey]].flag == 0)
                    {
                        choiceSecondNO[i] = choiceFirstNO[RandKey];
                        i++;
                    }
                    mark = 0;
                }
                break;
            case 3:
                while (i < 4)
                {
                    foreach (var z in choiceFirstNO)
                    {
                        if (Choices[z].flag == 0)
                        {
                            choiceSecondNO[i] = z;
                            i++;
                        }
                    }
                }
                break;
        }
        //绑定对话中的选项
        button1.image.sprite = Choices[choiceSecondNO[0]].notChosen;
        button2.image.sprite = Choices[choiceSecondNO[1]].notChosen;
        button3.image.sprite = Choices[choiceSecondNO[2]].notChosen;
        button4.image.sprite = Choices[choiceSecondNO[3]].notChosen;
        isChosen = 1;
        StartCoroutine("ChangeChosenButton");
    }
}

