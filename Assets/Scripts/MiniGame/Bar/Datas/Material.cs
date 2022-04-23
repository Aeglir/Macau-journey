namespace MiniGame.Bar.Datas
{
    [System.Serializable]
    public class Material
    {
        [UnityEngine.SerializeField]
        [UnityEngine.Header("杯子")]
        private Cup[] cups;
        [UnityEngine.SerializeField]
        [UnityEngine.Header("调味酒")]
        private Wine[] wines;
        [UnityEngine.SerializeField]
        [UnityEngine.Header("饮料")]
        private Drink[] drinks;
        [UnityEngine.SerializeField]
        [UnityEngine.Header("调料")]
        private Flavouring[] flavourings;
        public Cup[] Cups { get => cups; }
        public Wine[] Wines { get => wines; }
        public Drink[] Drinks { get => drinks; }
        public Flavouring[] Flavourings { get => flavourings; }
    }
}
