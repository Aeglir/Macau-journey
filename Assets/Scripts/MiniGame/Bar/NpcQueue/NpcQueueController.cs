using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MiniGame.Bar.NPCQUEUE
{
    public class NpcQueueController : MonoBehaviour
    {
        NpcQueueCore core;
#if DEBUGMODE
        public bool debug;
#endif
        public DBContorller contorller;
        public GameObject prefabs;
        public GameObject layer;
        public GameObject pool;
        public Button shakeButton;
        public UnityEvent OrderEvent;
        public UnityEvent NextEvent;
        public void init(int[] tags, Sprite[] orders, Sprite[] waits)
        {
            var fac = new NpcQueueFactory();
            fac.BuildNAppearCon();
            fac.BuildNMotionCon(layer);
            fac.BuildNPCPool(pool, prefabs, tags, orders, waits);
            core = fac.GetResult();
            core.init();
            core.SetOrderEvent(() =>
            {
                OrderEvent.Invoke();
                shakeButton.interactable=true;
            });
#if DEBUGMODE
            if (!debug)
                core.AppearCon.Start();
            else
            {
                core.AddNpc();
            }
#else
            core.AppearCon.Start();
#endif
        }
        public void FinishOrder()
        {
            shakeButton.interactable=false;
            
            if(core.OrderComplete())
                NextEvent.Invoke();
        }
        private void OnDestroy()
        {
            if (core != null)
                core.Dispose();
        }
        public void Start(){
            if (core!=null)
                core.Start();
        }

        public void Pasue(){
            if (core!=null)
                core.Pause();
        }
    }
}
