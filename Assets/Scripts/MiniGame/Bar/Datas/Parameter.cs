namespace MiniGame.Bar.Datas
{
    [System.Serializable]
    public class Parameter
    {
        [UnityEngine.SerializeField]
        [UnityEngine.Header("口味")]
        private string[] tastes;
        [UnityEngine.SerializeField]
        [UnityEngine.Header("容量")]
        private string[] caps;
        [UnityEngine.SerializeField]
        [UnityEngine.Header("度数量度")]
        private string[] degs;
        [UnityEngine.SerializeField]
        [UnityEngine.Header("饮品性质")]
        private string[] liqudkind;
        public string[] Ltastes { get => tastes; }
        public string[] Caps { get => caps; }
        public string[] Degs { get => degs; }
        public string[] Liqudkind { get => liqudkind; }
    }
}