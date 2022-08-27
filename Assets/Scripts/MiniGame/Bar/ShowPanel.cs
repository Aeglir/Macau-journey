using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;

namespace MiniGame.Bar
{
    public class ShowPanel : MonoBehaviour
    {
        public UnityEvent<int> EvaluationEvent;
        public RectTransform contain;
        public UnityEngine.UI.Image image;
        public Button orderButton;
        public Button ridButton;
        private Sprite[][] sprites;
        private int cur;
        private int eva;
        private Vector2 cupStartPos;
        private UnityEngine.EventSystems.EventSystem eventSystem;
        private bool isActive;
        private void Awake()
        {
            contain.gameObject.SetActive(false);
            cur = 0;
            cupStartPos = image.rectTransform.anchoredPosition;
            cupStartPos.x = GobalSetting.CUPSTARTPOS;
            orderButton.onClick.AddListener(() =>
            {
                OrderHandle();
            });
            ridButton.onClick.AddListener(() =>
            {
                RidHandle();
            });
            eventSystem = UnityEngine.EventSystems.EventSystem.current;
            Reset();
        }
        public void init(Sprite[][] sprites) => this.sprites = sprites;
        public void Show(int eva)
        {
            this.eva = eva;
            image.sprite = sprites[eva][cur];
            image.SetNativeSize();
            contain.gameObject.SetActive(true);
            var seq = DOTween.Sequence();
            seq.Append(contain.DOAnchorPosX(GobalSetting.CANVASSHOWPOS, GobalSetting.CANVASDUR));
            seq.Append(image.rectTransform.DOAnchorPosX(GobalSetting.CUPSHOWPOS, GobalSetting.CUPSHOWDUR));
            seq.OnComplete(() =>
            {
                var s = DOTween.Sequence();
                s.Append(orderButton.image.DOFade(1, GobalSetting.SHOWBUTTONDUR));
                s.Join(ridButton.image.DOFade(1, GobalSetting.SHOWBUTTONDUR));
                s.OnComplete(() =>
                {
                    eventSystem.enabled = true;
                    orderButton.interactable = true;
                    ridButton.interactable = true;
                    isActive=true;
                });
            });
        }
        public void Reset()
        {
            image.rectTransform.anchoredPosition = cupStartPos;
            contain.anchoredPosition = new Vector2(GobalSetting.CANVASSTARTPOS, 0);
            orderButton.image.color = new Color(1, 1, 1, 0);
            ridButton.image.color = new Color(1, 1, 1, 0);
            orderButton.interactable = false;
            ridButton.interactable = false;
            isActive=false;
        }
        public void Close() => contain.gameObject.SetActive(false);
        public void RidHandle()
        {
            if(!isActive)
                return;
            contain.gameObject.SetActive(false);
            Reset();
        }
        public void Next()
        {
            cur++;
        }
        public void OrderHandle()
        {
            if(!isActive)
                return;
            EvaluationEvent.Invoke(eva);
            contain.gameObject.SetActive(false);
            Reset();
        }
    }
}
