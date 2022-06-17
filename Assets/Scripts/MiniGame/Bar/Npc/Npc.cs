using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGame.Bar.NPC
{
    public class Npc : MonoBehaviour, AnimationControllable, PoolItem
    {
        private event System.Action waitFinishEvent;
        private event System.Action orderEvent;
        private int npcTag;
        public int NpcTag { get => npcTag; set => npcTag = value; }
        private Image image;
        private Sequence moveSequence;
        private bool accessBar;
        private Image NpcImage
        {
            get
            {
                if (image == null)
                    image = gameObject.GetComponent<Image>();
                return image;
            }
        }
        public bool AccessBar { get => accessBar; set => accessBar = value; }
        public bool IsMoving{get=>moveSequence!=null&moveSequence.IsPlaying();}
        private Sprite WaitImage;
        private Sprite OrderImage;
        private void OnDestroy()
        {
            Dispose();
        }
        public void SetImage(Sprite Wait, Sprite Order)
        {
            WaitImage = Wait;
            OrderImage = Order;
        }
        public void SetWait()
        {
            NpcImage.sprite = WaitImage;
        }

        public void SetOrder()
        {
            NpcImage.sprite = OrderImage;
        }
#if DEBUGMODE
        internal void Order(){
            orderEvent();
        }
#endif

        public void SetWaitFinishEvent(System.Action action)=>waitFinishEvent=action;
        public void SetOrderEvent(System.Action action)=>orderEvent=action;
        public void GoIntoQueue(Vector3 target, float duration,int jumpnums)
        {
            moveSequence=(transform as RectTransform).DOJumpAnchorPos(target,GobalSetting.WALKJUMPPOWER,jumpnums,duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (waitFinishEvent != null)
                    waitFinishEvent();
                if(orderEvent!=null&&accessBar)
                {
                    image.sprite=OrderImage;
                    orderEvent();
                }
                Pause();
            }).Pause();
        }
        public void GoOut(Vector3 target, float duration,int jumpnums)
        {
            image.sprite=WaitImage;
            moveSequence=(transform as RectTransform).DOJumpAnchorPos(target,GobalSetting.WALKJUMPPOWER,jumpnums,duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(gameObject);
            }).Pause();
        }

        public void Start()
        {
            if (moveSequence != null)
                moveSequence.Play();
        }

        public void Pause()
        {
            if (moveSequence != null)
                moveSequence.Pause();
        }

        public void Distory()
        {
            if (moveSequence != null)
            {
                Pause();
                moveSequence.Kill();
                moveSequence=null;
            }
        }

        public void Dispose()
        {
            Distory();
        }

        public void Reset()
        {
            Distory();
            accessBar=false;
            gameObject.SetActive(false);
        }
    }
}
