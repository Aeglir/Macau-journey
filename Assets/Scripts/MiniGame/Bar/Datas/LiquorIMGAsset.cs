using UnityEngine;
namespace MiniGame.Bar.Datas{ 
    [CreateAssetMenu(fileName = "LiquorIMGAsset", menuName = "MiniGame/Bar/LiquorIMGAsset")]
    public class LiquorIMGAsset : ScriptableObject
    {
        [SerializeField]
        [Header("空杯")]
        private Sprite[] cups;
        [SerializeField]
        [Header("差")]
        private Sprite[] bad;
        [SerializeField]
        [Header("普通")]
        private Sprite[] normal;
        [SerializeField]
        [Header("优")]
        private Sprite[] good;
        public Sprite[] Bad { get => bad;  }
        public Sprite[] Normal { get => normal;  }
        public Sprite[] Good { get => good; }
        public Sprite[] Cups { get => cups; }
    }
}
