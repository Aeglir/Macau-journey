namespace  MiniGame.Bar.Datas
{
    [System.Serializable]
    public class Cup
    {
        [UnityEngine.SerializeField]
        [UnityEngine.Header("名字")]
        private string name;
        [UnityEngine.Header("容量")]
        [UnityEngine.SerializeField]
        private int cap;
        public string Name { get => name; }
        public int Cap { get => cap; }
    }
}