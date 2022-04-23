namespace MiniGame.Bar.Datas
{
    [System.Serializable]
    public class Drink
    {
        [UnityEngine.SerializeField]
        [UnityEngine.Header("名字")]
        private string name;
        [UnityEngine.Header("口味")]
        [UnityEngine.SerializeField]
        private int[] tastes;
        public string Name { get => name;}
        public int[] Tastes { get => tastes; }
    }
}
