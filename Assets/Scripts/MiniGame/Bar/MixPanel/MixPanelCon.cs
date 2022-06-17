using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

namespace MiniGame.Bar.Mix
{
    public class MixPanelCon : HotMono
    {
        public OrderTable orderTable;
        public SceneAudioManager audioManager;
        public RectTransform[] panels;
        public Image indicator;
        public MixPanel panel;
        public UnityEvent<int> Event;
        public DBContorller dBContorller;
        public Button nextButton;
        public Button preButton;
        public int cap;
        public void init(Sprite[] asset)
        {
            panel = new MixPanel(gameObject.transform as RectTransform, panels, cap, indicator, asset);
            nextButton.onClick.AddListener(() =>
            {
                if (!panel.NextPage())
                {
                    int t = panel.Finish(orderTable.Drink);
                    if (t != -1)
                    {
                        nextButton.image.DOFade(0,GobalSetting.MIXBUTTONFADEDUR);
                        preButton.image.DOFade(0,GobalSetting.MIXBUTTONFADEDUR);
                        UnityEngine.EventSystems.EventSystem.current.enabled = false;
                        panel.indicator.Shaking(() =>
                        {
                            Event.Invoke(t);
                        },()=>{
                            audioManager.Play(GobalSetting.SHAKENAME);
                        },()=>{
                            Disable();
                        });
                    }
                }
            });
            preButton.onClick.AddListener(() =>
            {
                panel.PrePage();
            });
        }
        public void Enable()
        {
            nextButton.image.color=Color.white;
            preButton.image.color=Color.white;
            panel.init();
        }
        public void Disable()
        {
            panel.Disable();
        }
        public void AddWine(int wine)
        {
            audioManager.Play(GobalSetting.POURNAME);
            panel.addMaterial(MixPanel.MATERIALTYPE.WINE, wine);
        }
        public void AddDrink(int drink)
        {
            panel.addMaterial(MixPanel.MATERIALTYPE.DRINK, drink);
        }
        public void AddFlavoring(int flavoring)
        {
            panel.addMaterial(MixPanel.MATERIALTYPE.FLAVORING, flavoring);
        }
    }
}
