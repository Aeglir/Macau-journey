using System.Collections;
using MiniGame.Bar.NPC;

namespace MiniGame.Bar.NPCQUEUE{
    public class NpcQueue :Dequeueable,Peekable<Npc>,Enqueueable<Npc>,System.IDisposable {
        private Queue queue = new Queue();
        public Npc Peek()=>queue.Peek() as Npc ;
        public void Enqueue(Npc data)=>queue.Enqueue(data);
        public void Dequeue()=>queue.Dequeue();
        public int Count{get=>queue.Count;}
        ~NpcQueue()
        {
            Dispose();
        }

        public void Dispose()
        {
            while(queue.Count>0)
            {
                Npc npc = queue.Dequeue() as Npc;
                UnityEngine.GameObject.DestroyImmediate(npc.gameObject);
            }
        }
    }
}