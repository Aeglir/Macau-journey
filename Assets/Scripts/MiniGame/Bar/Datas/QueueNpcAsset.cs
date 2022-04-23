using UnityEngine;
namespace MiniGame.Bar.Datas
{
    [System.Serializable]
    public class NpcOrders{
        [SerializeField]
        [Header("可用订单")]
        private Order[] orders;
        public Order[] Orders { get => orders; }
    }
    [CreateAssetMenu(fileName = "QueueNpcAsset", menuName = "MiniGame/Bar/QueueNpcAsset")]
    public class QueueNpcAsset : ScriptableObject
    {
        [SerializeField]
        [Header("npc订单列表")]
        private NpcOrders[] npcOrders;

        public NpcOrders[] NpcOrders { get => npcOrders; }
    }
}
