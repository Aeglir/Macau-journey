using UnityEngine;

namespace MiniGame.Bar.NPCQUEUE{
    public class NpcQueueFactory{
        private NMotionCon motionCon;
        private NAppearCon appearCon;
        private NpcQueueCore result;
        private NpcInitializer initializer;
        private NpcPool pool;
        internal void BuildNMotionCon(GameObject layer){
            motionCon=new NMotionCon(layer);
        }
        internal void BuildNPCPool(GameObject pool,GameObject prefabs, int[] tags, Sprite[] orders, Sprite[] waits)
        {
            this.pool = new NpcPool(pool,prefabs,tags,orders,waits);
        }
        internal void BuildNAppearCon()
        {
            appearCon=new NAppearCon();
        }
        internal NpcQueueCore GetResult()
        {
            result = new NpcQueueCore(appearCon,motionCon,pool);
            return result;
        }
    }
}