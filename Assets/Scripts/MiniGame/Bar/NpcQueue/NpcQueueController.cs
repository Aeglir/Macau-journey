using UnityEngine;
using UnityEngine.Events;

namespace MiniGame.Bar.NPCQUEUE
{
    [System.Serializable]
    public class OrderEvent : UnityEvent<int> { }
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
        public OrderEvent OrderEvent;
        public void init(int[] tags, Sprite[] orders, Sprite[] waits){
            var fac = new NpcQueueFactory();
            fac.BuildNAppearCon();
            fac.BuildNMotionCon(layer);
            fac.BuildNPCPool(pool, prefabs,tags,orders,waits);
            core = fac.GetResult();
            core.init();
            core.SetOrderEvent((d) =>
            {
                OrderEvent.Invoke(d);
                Debug.Log(d);
            });
#if DEBUGMODE
            if(!debug)
                core.AppearCon.Start();
            else{
                core.AddNpc();
            }
#else
            core.AppearCon.Start();
#endif
        }

        private void OnDestroy()
        {
            if(core!=null)
                core.Dispose();
        }
    }
}
