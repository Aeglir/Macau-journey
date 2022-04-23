using UnityEngine;
namespace MiniGame.Bar.Datas
{
    [CreateAssetMenu(fileName = "LiquorDataAsset", menuName = "MiniGame/Bar/LiquorDataAsset")]
    public class LiquorDataAsset : ScriptableObject
    {
        [SerializeField]
        [Header("相关参数")]
        private Parameter parameters;
        [SerializeField]
        [Header("酒吧饮品")]
        private BarDrink[] bardrinks;
        [Header("材料")]
        [SerializeField]
        private Material materials;
        public BarDrink[] Bardrinks { get => bardrinks; }
        public Material Materials { get => materials; }
        public Parameter Parameters { get => parameters; }
    }
}
