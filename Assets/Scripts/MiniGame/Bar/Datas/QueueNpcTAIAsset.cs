using UnityEngine;
namespace MiniGame.Bar.Datas
{
    [System.Serializable]
    public class Evaluation{
        [UnityEngine.SerializeField]
        [UnityEngine.Header("文案")]
        private string text;
        [UnityEngine.SerializeField]
        [UnityEngine.Header("图片")]
        private Sprite[] img;
        public string Text { get => text; }
        public Sprite[] Img { get => img; }
    }
    [System.Serializable]
    public class NpcEvaluations{
        [UnityEngine.SerializeField]
        private Evaluation bad;
        [UnityEngine.SerializeField]
        private Evaluation normal;
        [UnityEngine.SerializeField]
        private Evaluation good;
        public Evaluation Bad { get => bad; }
        public Evaluation Normal { get => normal; }
        public Evaluation Good { get => good; }
    }
    [System.Serializable]
    public struct FinishFirstPImg{
        [UnityEngine.SerializeField]
        private string name;
        [UnityEngine.SerializeField]
        private Sprite bad;
        [UnityEngine.SerializeField]
        private Sprite normal;
        [UnityEngine.SerializeField]
        private Sprite good;
        public Sprite Bad { get => bad; }
        public Sprite Normal { get => normal; }
        public Sprite Good { get => good; }
        public string Name { get => name; }
    }
    [CreateAssetMenu(fileName = "QueueNpcTextAndImgAsset", menuName = "MiniGame/Bar/QueueNpcTextAndImgAsset")]
    public class QueueNpcTAIAsset : ScriptableObject
    {
        [UnityEngine.SerializeField]
        private FinishFirstPImg[] first;
        [UnityEngine.SerializeField]
        private Sprite[] second;
        [UnityEngine.SerializeField]
        [UnityEngine.Header("NPC评价相关")]
        NpcEvaluations[] npcEvas;
        public NpcEvaluations[] NpcEvas { get => npcEvas; }
        public FinishFirstPImg[] First { get => first; }
        public Sprite[] Second { get => second; }
    }
}
