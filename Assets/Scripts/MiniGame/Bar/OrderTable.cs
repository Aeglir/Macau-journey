using TMPro;
using UnityEngine;
using MiniGame.Bar.Datas;

namespace MiniGame.Bar
{
    public class OrderTable : MonoBehaviour
    {
        public DBContorller dBContorller;
        public TextMeshProUGUI text;
        private Order order;
        private int npcTag;
        private Order[] orders;
        private int orderindex;
        int current;
        public Order Order { get => order;}
        public int NpcTag { get => npcTag;}

        public void init(Order[] orders){this.orders=orders;}
        public void SetTable(int npcTag)
        {
            this.npcTag=npcTag;
            if(current>=GobalSetting.MAXORDERS) return;
            order=orders[current];
            text.text=order.Text;
        }
    }
}
