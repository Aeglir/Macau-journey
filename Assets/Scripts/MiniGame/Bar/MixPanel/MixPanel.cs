using UnityEngine;
using MiniGame.Bar.Datas;
using static MiniGame.Bar.Mix.MixPanel;

namespace MiniGame.Bar.Mix
{
    public class MixPanel : MaterialAdder<MATERIALTYPE,int>
    {
        private MixChart chart;
        private GameObject[] panels;
        private Order order;
        private Indicator indicator;
        public Order Order { get => order; set => order = value; }
        public enum MATERIALTYPE{
            WINE,DRINK,FLAVORING
        }
        private int currentPage;
        public MixPanel(GameObject[] panels,int cap, UnityEngine.UI.Image image,Sprite[] asset)
        {
            chart = new MixChart();
            indicator = new Indicator(cap,image,asset);
            this.panels = panels;
            foreach (GameObject obj in panels)
                obj.SetActive(false);
        }
        internal void init()
        {
            currentPage = -1;
            NextPage();
        }
        internal bool NextPage()
        {
            if (currentPage >= 0)
                panels[currentPage].SetActive(false);
            currentPage++;
            if (currentPage >= panels.Length)
                return false;
            panels[currentPage].SetActive(true);
            return true;
        }
        internal bool PrePage()
        {
            if (currentPage < panels.Length)
                panels[currentPage].SetActive(false);
            currentPage--;
            if (currentPage < 0)
                return false;
            panels[currentPage].SetActive(true);
            return true;
        }
        internal void Reset()
        {
            chart.Clear();
            indicator.Reset();
        }
        internal int Finish(Datas.Order order)
        {
            if (order == null) return 0;
            int tag = 0;
            MaterialChart c = chart.result;
            int wl = c.wines.Length;
            int dl = c.drinks.Length;
            int fl = c.flavorings.Length;
            if (wl == order.Wines.Length)
            {
                tag++;
                for (int i = 0; i < wl; i++)
                {
                    if (c.wines[i] != order.Wines[i])
                    {
                        tag--;
                        break;
                    }
                }
            }
            if (dl == order.Drinks.Length)
            {
                tag++;
                for (int i = 0; i < dl; i++)
                {
                    if (c.wines[i] != order.Drinks[i])
                    {
                        tag--;
                        break;
                    }
                }
            }
            if (fl == order.Flavorings.Length)
            {
                tag++;
                for (int i = 0; i < fl; i++)
                {
                    if (c.wines[i] != order.Flavorings[i])
                    {
                        tag--;
                        break;
                    }
                }
            }
            return tag;
        }
        internal void Disable()
        {
            foreach (GameObject obj in panels)
                obj.SetActive(false);
        }

        public void addMaterial(MATERIALTYPE type, int material)
        {
            chart.addMaterial(type,material);
            indicator.addMaterial(type,material);
        }
    }

}