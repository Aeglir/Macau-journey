using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MiniGame.Bar.Mix
{
    public class MixPanelCon : HotMono
    {
        public OrderTable orderTable;
        public GameObject[] panels;
        public Image indicator;
        public MixPanel panel;
        public UnityEvent<int> Event;
        public DBContorller dBContorller;
        public Button nextButton;
        public Button preButton;
        public int cap;
        public void init(Sprite[] asset){
            panel = new MixPanel(panels,cap,indicator,asset);
            nextButton.onClick.AddListener(() =>
            {
                if (!panel.NextPage())
                {
                    gameObject.SetActive(false);
                    int Bardrinkid = orderTable.Order.Bardrinkid;
                    int t = panel.Finish(orderTable.Order);
                    panel.Reset();
                    if (t != -1)
                    {
                        Debug.Log(Bardrinkid+" : "+t);
                        // Event.Invoke(t);
                    }
                }
            });
            preButton.onClick.AddListener(() =>
            {
                if (!panel.PrePage())
                {
                    gameObject.SetActive(false);
                    panel.Reset();
                }
            });
        }
        private void OnEnable()
        {
            panel.init();
        }

        private void OnDisable()
        {
            if (panel != null)
                panel.Disable();
        }
        public void AddWine(int wine)
        {
            panel.addMaterial(MixPanel.MATERIALTYPE.WINE,wine);
        }
        public void AddDrink(int drink)
        {
            panel.addMaterial(MixPanel.MATERIALTYPE.DRINK,drink);
        }
        public void AddFlavoring(int flavoring)
        {
            panel.addMaterial(MixPanel.MATERIALTYPE.FLAVORING,flavoring);
        }
    }
}
