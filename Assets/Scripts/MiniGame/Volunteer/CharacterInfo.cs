using UnityEngine;

namespace MiniGame.Volunteer
{
    [System.Serializable]
    public class CharacterInfo
    {
        [Header("唯一标识")]
        public int tag;
        [Header("名字")]
        public string Name;
        [Header("精灵动画")]
        public System.Collections.Generic.List<Sprite> frameTex;
        [Header("黑白精灵动画")]
        public System.Collections.Generic.List<Sprite> alphaFrameTex;
        [Header("动画偏移量")]
        public Vector2 TexOffset;
        [Header("源方向")]
        public bool isRight;
        [Header("立绘")]
        public Sprite painting;
        [Header("移动速度")]
        public float moveSpeed;
        [Header("分数需求")]
        public int _sorceNeed;
        [Header("报酬")]
        public int _price;
        protected RectTransform transform;
        public Vector2 anchoredPosition { get => transform.anchoredPosition; }
    }
}
