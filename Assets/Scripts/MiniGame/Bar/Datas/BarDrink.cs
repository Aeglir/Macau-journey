namespace  MiniGame.Bar.Datas
{
    [System.Serializable]
    public class BarDrink
    {
        [UnityEngine.SerializeField]
        private int id;
        [UnityEngine.SerializeField]
        [UnityEngine.Header("名字")]
        private string name;
        [UnityEngine.SerializeField]
        [UnityEngine.Header("酒")]
        private int[] wines;
        [UnityEngine.SerializeField]
        [UnityEngine.Header("饮料")]
        private int[] drinks;
        [UnityEngine.SerializeField]
        [UnityEngine.Header("调料")]
        private int[] flavourings;
        [UnityEngine.SerializeField]
        [UnityEngine.Header("杯子")]
        private int cup;
        [UnityEngine.SerializeField]
        [UnityEngine.Header("口味")]
        private int[] tastes;
        [UnityEngine.SerializeField]
        [UnityEngine.Header("度数")]
        private int deg;
        [UnityEngine.SerializeField]
        [UnityEngine.Header("性质")]
        private int kind;
        public string Name { get => name; }
        public int[] Wines { get => wines; }
        public int[] Drinks { get => drinks; }
        public int[] Flavourings { get => flavourings; }
        public int Cup { get => cup; }
        public int[] Tastes { get => tastes; }
        public int Deg { get => deg; }
        public int Kind { get => kind; }
        public int Id { get => id;}
    }
}

