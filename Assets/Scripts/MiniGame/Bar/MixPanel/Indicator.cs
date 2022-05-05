using UnityEngine;
using UnityEngine.UI;

namespace MiniGame.Bar.Mix
{
    public class Indicator : MaterialAdder<MixPanel.MATERIALTYPE,int>
    {
        private Sprite[] sprites;
        private Image image;
        int current;
        int cap;

        internal Indicator(int cap, Image image, Sprite[] sprites)
        {
            this.cap = cap;
            this.image = image;
            this.sprites = sprites;
        }

        public void addMaterial(MixPanel.MATERIALTYPE type, int material)
        {
            if(current>=cap)
                return;
            current++;
            image.sprite=sprites[current];
        }

        public void Reset()
        {
            current=0;
            image.sprite=sprites[0];
        }
    }
}