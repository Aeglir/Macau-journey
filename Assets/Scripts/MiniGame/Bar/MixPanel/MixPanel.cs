using UnityEngine;
using MiniGame.Bar.Datas;
using System.Linq;
using DG.Tweening;
using static MiniGame.Bar.Mix.MixPanel;
using UnityEngine.EventSystems;

namespace MiniGame.Bar.Mix
{
    public class MixPanel : MaterialAdder<MATERIALTYPE, int>
    {
        private MixChart chart;
        private RectTransform content;
        private RectTransform[] panels;
        private Order order;
        public Indicator indicator;
        private Vector2 start;
        public Order Order { get => order; set => order = value; }
        public EventSystem eventSystem;
        public enum MATERIALTYPE
        {
            WINE, DRINK, FLAVORING
        }
        private int currentPage;
        public MixPanel(RectTransform content, RectTransform[] panels, int cap, UnityEngine.UI.Image image, Sprite[] asset)
        {
            this.content = content;
            chart = new MixChart();
            indicator = new Indicator(cap, image, asset);
            this.panels = panels;
            start = new Vector2(GobalSetting.CANVASSTARTPOS, 0);
            eventSystem = EventSystem.current;
        }
        private void Move(RectTransform rect, float tar, System.Action action = null)
        {
            var tweener = rect.DOAnchorPosX(tar, GobalSetting.CANVASDUR);
            if (action != null)
                tweener.OnComplete(() => { action(); });
        }
        internal void init()
        {
            content.gameObject.SetActive(true);
            currentPage = 0;
            Reset();
            panels[currentPage].gameObject.SetActive(true);
            panels[currentPage].anchoredPosition = Vector2.zero;
            eventSystem.enabled = false;
            Move(content, GobalSetting.CANVASSHOWPOS, () =>
            {
                eventSystem.enabled = true;
            });
        }
        internal bool NextPage()
        {
            eventSystem.enabled = false;
            int pre = currentPage;
            currentPage++;
            if (currentPage < panels.Length)
            {
                Move(panels[pre], GobalSetting.CANVASENDPOS, () =>
                {
                    panels[pre].gameObject.SetActive(false);
                });
                panels[currentPage].gameObject.SetActive(true);
                Move(panels[currentPage], GobalSetting.CANVASSHOWPOS, () =>
                {
                    eventSystem.enabled = true;
                });
                return true;
            }
            eventSystem.enabled = true;
            if (chart.isEmpty())
            {
                currentPage--;
                return true;
            }
            panels[pre].gameObject.SetActive(false);
            return false;
        }
        internal bool PrePage()
        {
            eventSystem.enabled = false;
            int pre = currentPage;
            currentPage--;
            if (currentPage >= 0)
            {
                Move(panels[pre], GobalSetting.CANVASSTARTPOS, () =>
                {
                    panels[pre].gameObject.SetActive(false);
                });
                panels[currentPage].gameObject.SetActive(true);
                Move(panels[currentPage], GobalSetting.CANVASSHOWPOS, () =>
                {
                    eventSystem.enabled = true;
                });
                return true;
            }
            Move(content, GobalSetting.CANVASSTARTPOS, () =>
            {
                content.gameObject.SetActive(false);
                eventSystem.enabled = true;
            });
            return false;
        }
        internal void Reset()
        {
            chart.Clear();
            indicator.Reset();
            foreach (RectTransform rect in panels)
                rect.anchoredPosition = start;
        }
        internal int Finish(Datas.BarDrink order)
        {
            if (order == null) return -1;
            int tag = 0;
            MaterialChart c = chart.result;
            int wl = c.wines.Length;
            int dl = c.drinks.Length;
            int fl = c.flavorings.Length;
            if (wl == order.Wines.Length)
            {
                c.wines.OrderBy((s) => s);
                tag++;
                for (int i = 0; i < wl; i++)
                {
                    Debug.Log(c.wines[i]);
                    if (c.wines[i] != order.Wines[i])
                    {
                        tag--;
                        break;
                    }
                }
            }
            if (dl == order.Drinks.Length)
            {
                c.drinks.OrderBy((s) => s);
                tag++;
                for (int i = 0; i < dl; i++)
                {
                    // Debug.Log(c.drinks[i]);
                    if (c.drinks[i] != order.Drinks[i])
                    {
                        tag--;
                        break;
                    }
                }
            }
            if (fl == order.Flavourings.Length)
            {
                c.flavorings.OrderBy((s) => s);
                tag++;
                for (int i = 0; i < fl; i++)
                {
                    // Debug.Log(c.flavorings[i]);
                    if (c.flavorings[i] != order.Flavourings[i])
                    {
                        tag--;
                        break;
                    }
                }
            }
            return tag > 0 ? tag - 1 : tag;
        }
        internal void Disable()
        {
            Reset();
            content.gameObject.SetActive(false);
        }

        public void addMaterial(MATERIALTYPE type, int material)
        {
            chart.addMaterial(type, material);
            indicator.addMaterial(type, material);
        }
    }

}