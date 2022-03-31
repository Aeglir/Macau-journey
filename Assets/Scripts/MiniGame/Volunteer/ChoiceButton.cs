using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ChoiceButton : MonoBehaviour
{
    [Header("��ť���")]
    public Button button1; //����ѡ��
    public Button button2;
    public Button button3;
    public Button button4;

    //�Ի�ѡ��
    private class Choice
    {
        public Sprite isChosen;//��ѡ��
        public Sprite notChosen;//δ��ѡ��
        public int score;//����
        public int flag;//�Ƿ�ѡ���
    }

    [Header("ѡ�����")]
    public List<Sprite> isChosenList = new List<Sprite>(); //ѡ��ѡ��ͼƬ�б�
    public List<Sprite> notChosenList = new List<Sprite>(); //δѡ��ѡ��ͼƬ�б�
    private List<int> choiceFirstNO = new List<int>();//���������б�ѡ�е�6���Ի����б�
    private List<int> choiceSecondNO = new List<int>();//������ÿ�ֱ�ѡ�е�4���Ի����б�
    private Choice[] Choices = new Choice[12];

    [Header("��־���")]
    private int isChosen = 1; //���̱�־λ��Ĭ�ϵ�һ����ťѡ��
    private int turn = 1; //�Ի��ִα�־λ��Ĭ��Ϊ��һ��
    private float changeSpeed = 1; //���̱任�ٶ�

    [Header("�������")]
    private int score = 0; //��ҵ÷�
    private int npcScore;

    [Header("�������")]
    public Image npc;
    public Image player;
    private Sprite npcNormal;//��������
    private Sprite playerNormal;
    private Sprite npcFail;//ʧ�ܱ���
    private Sprite playerFail;
    private Sprite npcSucceed;//�ɹ�����
    private Sprite playerSucceed;

    [Header("����ʱ���")]
    private int TotalTime = 30;//30s����ʱ

    [Header("�������")]
    public Canvas GameCanvas;
    public UnityEngine.Events.UnityEvent CloseEvent;
    public void Open()
    {
        Debug.Log("Start!");
        var NPCImageList = LoadData();
        var PlayerImageList = LoadPlayerData();

        // ��npcͼƬ
        npcNormal = NPCImageList[0];
        npcSucceed = NPCImageList[1];
        npcFail = NPCImageList[2];

        //��playerͼƬ
        playerNormal = PlayerImageList[0];
        playerSucceed = PlayerImageList[1];
        playerFail = PlayerImageList[2];

        npc.sprite = npcNormal;//����Ĭ�ϱ���
        player.sprite = playerNormal;

        //��ʼ��ѡ������
        for (int i = 0; i < 12; i++)
        {
            Choices[i] = new Choice();
            Choices[i].isChosen = isChosenList[i];//��ѡ��ͼƬ
            Choices[i].notChosen = notChosenList[i];//��δѡ��ͼƬ
            //���öԻ�����
            if (i <= 3)
                Choices[i].score = 1;
            else if (i > 3 && i <= 7)
                Choices[i].score = 0;
            else Choices[i].score = -1;
            //���öԻ���־,0��ʾδ��ѡ���
            Choices[i].flag = 0;
        }

        SetFirstChoices();
        StartCoroutine("ChangeChosenButton");
        StartCoroutine("Time");//����30s����ʱ
    }
    private void OnDisable()
    {

    }
    // Update is called once per frame
    public void ColseInvoke()
    {
        score=0;
        turn=1;
        choiceFirstNO.Clear();
        choiceSecondNO.Clear();
        if (CloseEvent != null)
            CloseEvent.Invoke();
    }

    void Update()
    {
        //��⵹��ʱ�Ƿ����
        if (TotalTime == 0)
        {
            StopCoroutine("ChangeChosenButton");
            StopCoroutine("Time");
            ColseInvoke();
        }
        //����Ƿ��»س����������
        if (Input.GetMouseButtonDown(0))
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
            if (turn > 3)//�����ж����˳���Ϸ
            {
                //�������
                if (score >= npcScore)
                {
                    Debug.Log(score);
                }
                StopCoroutine("ChangeChosenButton");
                StopCoroutine("Time");
                ColseInvoke();
            }
            StopCoroutine("ChangeChosenButton");
            if(gameObject.activeSelf)
                ChangeChoiceImage(turn);//���Ŀǰ�������һ�ֶԻ���������Ի�����
        }
    }

    //����ѡ���Զ�ת������
    IEnumerator ChangeChosenButton()
    {
        //����ʵ�ֹ���
        while (true)
        {
            SetInteractable();//��ʼ�������а�ť
            if (isChosen > 4) isChosen = 1;//�����־λisChosen>4��������Ϊ1
            switch (isChosen)
            {
                case 1:
                    button1.GetComponent<UnityEngine.UI.Button>().interactable = true; //���ð�ť
                    button1.image.sprite = Choices[choiceSecondNO[0]].isChosen;
                    break;
                case 2:
                    button2.GetComponent<UnityEngine.UI.Button>().interactable = true; //���ð�ť
                    button2.image.sprite = Choices[choiceSecondNO[1]].isChosen;
                    break;
                case 3:
                    button3.GetComponent<UnityEngine.UI.Button>().interactable = true; //���ð�ť
                    button3.image.sprite = Choices[choiceSecondNO[2]].isChosen;
                    break;
                case 4:
                    button4.GetComponent<UnityEngine.UI.Button>().interactable = true; //���ð�ť
                    button4.image.sprite = Choices[choiceSecondNO[3]].isChosen;
                    break;
            }
            yield return new WaitForSeconds(changeSpeed);
            isChosen++; //��־λ+1
        }
    }

    //�������а�ť����
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

    //����ʱ����
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
        int tag = MiniGame.Volunteer.CharacterSystem.Instance.Tag;
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

    //���õ�һ��ѡ��
    private void SetFirstChoices()
    {
        //ѡ�����������н�����ֵ�6���Ի�ѡ��
        int i = 0, mark1 = 0;
        while (i < 6)
        {
            if (i <= 1)
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
            else if (i > 1 && i <= 3)
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
        //ѡ����һ�ֶԻ��н�����ֵ�4���Ի�ѡ��
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
        //�󶨵�һ�ֶԻ��е�ѡ��
        button1.image.sprite = Choices[choiceSecondNO[0]].notChosen;
        button2.image.sprite = Choices[choiceSecondNO[1]].notChosen;
        button3.image.sprite = Choices[choiceSecondNO[2]].notChosen;
        button4.image.sprite = Choices[choiceSecondNO[3]].notChosen;
    }

    //ѡ��ѡ����Ӧ�仯
    private void RecordScore(int i)
    {
        score += Choices[choiceSecondNO[i]].score;//�����䶯
        //��Ի�
        if (Choices[choiceSecondNO[i]].score < 0)
        {
            TotalTime -= 5;
            npc.sprite = npcFail;
            player.sprite = playerFail;
        }
        //һ��Ի�
        else if (Choices[choiceSecondNO[i]].score == 0)
        {
            npc.sprite = npcNormal;
            player.sprite = playerNormal;
        }
        //����Ի�
        else
        {
            npc.sprite = npcSucceed;
            player.sprite = playerSucceed;
        }
        Choices[choiceSecondNO[i]].flag = 1;//�ԶԻ��ı�־λ���иı䣬1Ϊ��ѡ���
    }

    //�����Ի��ִ�ѡ���
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
        //�󶨶Ի��е�ѡ��
        button1.image.sprite = Choices[choiceSecondNO[0]].notChosen;
        button2.image.sprite = Choices[choiceSecondNO[1]].notChosen;
        button3.image.sprite = Choices[choiceSecondNO[2]].notChosen;
        button4.image.sprite = Choices[choiceSecondNO[3]].notChosen;
        isChosen = 1;
        if (gameObject.activeSelf)
            StartCoroutine("ChangeChosenButton");
    }
}

