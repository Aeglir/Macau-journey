using UnityEngine;
using MiniGame.Bar.NPC;

namespace MiniGame.Bar
{
    public class NpcPool : Settable<Npc>
    {
        private GameObject prefabs;
        private NpcInitializer initializer;
        public NpcPool(GameObject pool,GameObject prefabs, int[] tags, Sprite[] orders, Sprite[] waits) : base(pool)=>initializer = new NpcInitializer(prefabs,tags,orders,waits);
        protected override Npc Create()
        {
            return initializer.GetNewInstance();
        }
        public override Npc pop(){
            if(queue.Count>0&&initializer==null)
                return queue.Dequeue() as Npc;
            if(initializer.NpcHasInitialized>=GobalSetting.MAXNPC)
                initializer=null;
            return Create();
        }
        protected override void DistoryItem(Npc data)
        {
            GameObject obj = data.gameObject;
            GameObject.DestroyImmediate(obj);
        }
        protected override void Reset(Npc data)
        {
            data.Reset();
            data.gameObject.transform.SetParent(pool.transform);
        }
    }
}
