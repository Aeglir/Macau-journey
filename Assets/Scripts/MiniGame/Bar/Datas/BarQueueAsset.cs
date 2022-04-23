using UnityEngine;
namespace MiniGame.Bar.Datas{
    [CreateAssetMenu(fileName = "BarQueueAsset", menuName = "MiniGame/Bar/BarQueueAsset")]
    public class BarQueueAsset : ScriptableObject
    {
        [SerializeField]
        [Header("点单")]
        private Sprite[] orders;
        [SerializeField]
        [Header("排队")]
        private Sprite[] waits;
        public Sprite[] Orders { get => orders;  }
        public Sprite[] Wait { get => waits;  }
    }
}
