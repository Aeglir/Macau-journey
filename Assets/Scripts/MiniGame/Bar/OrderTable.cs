using UnityEngine;
using MiniGame.Bar.Datas;
using UnityEngine.UI;

namespace MiniGame.Bar
{
    public class OrderTable : MonoBehaviour
    {
        public DBContorller dBContorller;
        public Text text;
        private string[] orderTexts;
        private MiniGame.Bar.Datas.BarDrink[] drinks;
        private MiniGame.Bar.Datas.BarDrink curDrink;
        int cur;
        public MiniGame.Bar.Datas.BarDrink Drink{get=>curDrink;}

        public void init(string[] orderTexts,MiniGame.Bar.Datas.BarDrink[] drinks)
        {
            this.orderTexts = orderTexts;
            this.drinks = drinks;
            cur = 0;
        }
        public void SetTable()
        {
            if (cur >= GobalSetting.MAXORDERS) return;
            curDrink = drinks[cur];
            text.text = orderTexts[cur].Replace("\\n", "\n");
        }
        public void Next()
        {
            cur++;
            curDrink=null;
            text.text="";
        }
    }
}
