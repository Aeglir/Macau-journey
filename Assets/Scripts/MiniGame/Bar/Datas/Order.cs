namespace MiniGame.Bar.Datas
{
    [System.Serializable]
    public class Order
    {
        [UnityEngine.SerializeField]
        [UnityEngine.Header("酒吧饮品")]
        private int bardrinkid;
        [UnityEngine.SerializeField]
        private int[] wines;
        [UnityEngine.SerializeField]
        private int[] drinks;
        [UnityEngine.SerializeField]
        private int[] flavorings;
        [UnityEngine.SerializeField]
        [UnityEngine.Header("文案")]
        private string text;
        public int Bardrinkid { get => bardrinkid; }
        public string Text { get => text; }
        public int[] Wines { get => wines; }
        public int[] Drinks { get => drinks; }
        public int[] Flavorings { get => flavorings;}
    }
}