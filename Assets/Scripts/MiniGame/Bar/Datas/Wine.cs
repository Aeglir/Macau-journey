namespace  MiniGame.Bar.Datas
{
    [System.Serializable]
    public class Wine
    {
        [UnityEngine.SerializeField]
        [UnityEngine.Header("名字")]
        private string name;
        [UnityEngine.Header("度数")]
        [UnityEngine.SerializeField]
        private int deg;
        public string Name { get => name; }
        public int Deg { get => deg; }
    }
}