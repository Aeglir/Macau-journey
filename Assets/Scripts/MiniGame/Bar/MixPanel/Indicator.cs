using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MiniGame.Bar.Mix
{
    public class Indicator : MaterialAdder<MixPanel.MATERIALTYPE, int>
    {
        private Sprite[] sprites;
        private Image image;
        int current;
        int cap;
        Vector2 start;
        internal Indicator(int cap, Image image, Sprite[] sprites)
        {
            this.cap = cap;
            this.image = image;
            this.sprites = sprites;
            start = image.rectTransform.anchoredPosition;
        }

        public void addMaterial(MixPanel.MATERIALTYPE type, int material)
        {
            if (current >= cap)
                return;
            current++;
            image.sprite = sprites[current];
        }

        public void Reset()
        {
            current = 0;
            image.sprite = sprites[0];
            image.rectTransform.anchoredPosition = start;
        }
        public void Shaking(System.Action action=null,System.Action audioAction=null,System.Action disableAction=null)
        {
            var seq = DOTween.Sequence();
            seq.Append(image.rectTransform.DOAnchorPos(Vector2.zero, GobalSetting.SHAKEMOVEDUR));
            if(audioAction!=null)
                seq.AppendCallback(()=>{
                    audioAction();
                });
            seq.Append(image.rectTransform.DOShakeAnchorPos(GobalSetting.SHAKINGDUR));
            if (action != null)
                seq.AppendCallback(() =>
                {
                    action();
                });
            seq.Append(image.rectTransform.DOAnchorPosX(GobalSetting.CANVASSTARTPOS,GobalSetting.SHAKEMOVEDUR));
            if(disableAction!=null)
                seq.AppendCallback(() =>
                {
                    disableAction();
                });
        }
    }
}