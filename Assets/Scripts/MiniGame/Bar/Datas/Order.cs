namespace MiniGame.Bar.Datas
{
    [System.Serializable]
    public class Order
    {
        [UnityEngine.SerializeField]
        [UnityEngine.Header("酒吧饮品")]
        private int bardrinkid;
        [UnityEngine.SerializeField]
        [UnityEngine.Header("文案")]
        private string text;
        public int Bardrinkid { get => bardrinkid; }
        public string Text { get => text; }
    }
}