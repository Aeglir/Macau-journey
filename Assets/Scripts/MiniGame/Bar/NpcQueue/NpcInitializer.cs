using UnityEngine;
using UnityEngine.UI;

namespace MiniGame.Bar
{
    public class NpcInitializer
    {
        private GameObject prefabs;
        private int _NpcHasInitialized;
        private int[] tags;
        private Sprite[] orders;
        private Sprite[] waits;
        internal NpcInitializer(GameObject prefabs, int[] tags, Sprite[] orders, Sprite[] waits)
        {
            this.prefabs = prefabs;
            this.tags = tags;
            this.orders = orders;
            this.waits = waits;
        }

        public int NpcHasInitialized { get => _NpcHasInitialized;}

        internal NPC.Npc GetNewInstance(){
            if(_NpcHasInitialized>=GobalSetting.MAXORDERS)
                return null;
            GameObject obj = GameObject.Instantiate<GameObject>(prefabs);
            int index = _NpcHasInitialized;
            var npc = obj.GetComponent<NPC.Npc>();
            npc.NpcTag=tags[_NpcHasInitialized];
            npc.SetImage(waits[index],orders[index]);
            npc.SetWait();
            _NpcHasInitialized++;
            return npc;
        }
    }
}