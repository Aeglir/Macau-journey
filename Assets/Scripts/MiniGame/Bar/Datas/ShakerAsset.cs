using UnityEngine;
namespace MiniGame.Bar.Datas
{
    [CreateAssetMenu(fileName = "ShakerAsset", menuName = "MiniGame/Bar/ShakerAsset")]
    public class ShakerAsset : ScriptableObject
    {
        [SerializeField]
        [Header("阶段图片")]
        private Sprite[] shakers;
        public Sprite[] Shakers { get => shakers; }
    }
}
