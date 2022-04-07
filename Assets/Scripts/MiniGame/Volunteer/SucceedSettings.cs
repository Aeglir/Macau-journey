using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SucceedSettings : MonoBehaviour
{

    [Header("PicturePart")]
    public Image money;
    public Image turns;

    [Header("MarkPart")]
    private int price;
    private int turn;

    [Header("FirstList")]
    public Sprite vol1, vol2, vol3, vol4, vol5, vol6, vol7;

    [Header("SecondList")]
    public Sprite vo0, vo1, vo2, vo3, vo4, vo5;

    [Header("CanvasPart")]
    public GameObject succeedCanvas;
    public UnityEngine.Events.UnityEvent closeEvent;

    private System.Action closeAction;

    public void SetCloseListener(System.Action action) => closeAction = action;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            succeedCanvas.SetActive(false);
            closeAction.Invoke();
        }
    }
    //loadData
    public void LoadData(int price)
    {
        turn = MiniGame.Volunteer.CharacterSystem.Instance.round;
        this.price = price;
        SetImage();
    }
    //setImage
    private void SetImage()
    {
        //setMoneyImage
        switch(price)
        {
            case 0:
                money.sprite = vol1;
                break;
            case 1:
                money.sprite = vol2;
                break;
            case 2:
                money.sprite = vol3;
                break;
            case 5:
                money.sprite = vol4;
                break;
            case 25:
                money.sprite = vol5;
                break;
            case 50:
                money.sprite = vol6;
                break;
            case 100:
                money.sprite = vol7;
                break;
        }

        //setTurnImage
        switch(turn)
        {
            case 1:
                turns.sprite = vo4;
                break;
            case 2:
                turns.sprite = vo3;
                break;
            case 3:
                turns.sprite = vo2;
                break;
            case 4:
                turns.sprite = vo1;
                break;
            case 5:
                turns.sprite = vo0;
                break;
        }
    }
}
