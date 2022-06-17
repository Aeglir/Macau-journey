using MiniGame.Bar.NPC;

namespace MiniGame.Bar.NPCQUEUE
{
    public class NpcQueueCore : System.IDisposable,AnimationControllable
    {
        NAppearCon appearCon;
        NMotionCon motionCon;
        NpcPool pool;
        NpcQueue queue;
        private event System.Action orderEvent;
        internal NpcQueueCore(NAppearCon appearCon, NMotionCon motionCon, NpcPool pool)
        {
            queue = new NpcQueue();
            this.appearCon = appearCon;
            this.motionCon = motionCon;
            this.pool = pool;
        }
        ~NpcQueueCore() => Dispose();
        public NAppearCon AppearCon { get => appearCon; }
        public NMotionCon MotionCon { get => motionCon; }
        public NpcQueue Queue { get => queue; }

        public void Dispose()
        {
            appearCon.Dispose();
            queue.Dispose();
            appearCon = null;
            motionCon = null;
            pool = null;
            queue = null;
        }

        internal void init()
        {
            appearCon.Action = (t) =>
            {
                Npc npc = pool.pop();
                npc.SetOrderEvent(OrderHandle);
                motionCon.SetTargetPos(npc, t);
                queue.Enqueue(npc);
            };
        }
#if DEBUGMODE
        internal void AddNpc()
        {
            Npc npc = pool.pop();
            motionCon.SetOnGoal(npc);
            npc.SetOrderEvent(OrderHandle);
            queue.Enqueue(npc);
            npc.Order();
        }
#endif
        internal void SetOrderEvent(System.Action action) => orderEvent = action;
        public void OrderHandle()
        {
            if (orderEvent != null)
                orderEvent();
        }
        public bool OrderComplete()
        {
            if (queue.Count == 0)
                return false;
            appearCon.Pause();
            queue.Dequeue();
            motionCon.MoveStep(() =>
            {
                appearCon.TopHasOut();
                appearCon.Start();
            });
            appearCon.Start();
            return true;
        }

        public void Start()
        {
            appearCon.Start();
            motionCon.Start();
        }

        public void Pause()
        {
            appearCon.Pause();
            motionCon.Pause();
        }

        public void Distory()
        {
            throw new System.NotImplementedException();
        }
    }
}