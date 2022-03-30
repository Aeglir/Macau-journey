using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.Volunteer
{
    [System.Serializable]
    public class Character
    {
        [Header("唯一标识")]
        public int tag;
        [Header("精灵动画")]
        public Sprite Tex;
        [HideInInspector]
        public bool isForwardToRight;
        [Header("移动速度")]
        public int moveSpeed;
        protected RectTransform transform;
        public Vector2 anchoredPosition { get => transform.anchoredPosition; }
        public virtual void enable(RectTransform transform) => this.transform = transform;
        public virtual void disable() => transform = null;
        public virtual void Move()
        {
            if (isForwardToRight)
            {
                transform.Translate(Vector2.right * moveSpeed, Space.Self);
            }
            else
            {
                transform.Translate(Vector2.left * moveSpeed, Space.Self);
            }
        }
    }
    [System.Serializable]
    public class Npc : Character
    {
        [Header("分数需求")]
        public int _sorceNeed;
        public bool FinishRaiders(int score) => score >= _sorceNeed ? true : false;
    }
    [System.Serializable]
    public class Player : Character
    {
    }
}
