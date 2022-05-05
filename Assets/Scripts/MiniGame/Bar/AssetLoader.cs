using UnityEngine;
using MiniGame.Bar.Datas;

namespace MiniGame.Bar
{
    [DefaultExecutionOrder(-1)]
    public class AssetLoader : MonoBehaviour
    {
        public NPCQUEUE.NpcQueueController npcQueue;
        public OrderTable orderTable;
        public Mix.MixPanelCon mixPanel;
        public NoteComs note;

        private async void Awake()
        {
            DBContorller contorller = new DBContorller();
            var noteAsset = await contorller.LoadAssetBundleAsync<BarNoteAsset>(GobalSetting.BARNOTEASSETABNAME, GobalSetting.BARNOTEASSETRESNAME);
            note.init(noteAsset);
            var shakerAsset = await contorller.LoadAssetBundleAsync<ShakerAsset>(GobalSetting.SHAKERSASSETABNAME,GobalSetting.SHAKERSASSETRESNAME);
            mixPanel.init(shakerAsset.Shakers);
            int[] tags = Common.GetRoandomsInEqa(0, GobalSetting.MAXNPC, GobalSetting.MAXORDERS);
            var orderAsset = await contorller.LoadAssetBundleAsync<QueueNpcAsset>(GobalSetting.QUEUENPCASSETABNAME, GobalSetting.QUEUENPCASSETRESNAME);
            var npcOrders = Common.GetElements<NpcOrders>(tags,orderAsset.NpcOrders);
            Order[] orderlist = new Order[npcOrders.Length];
            for(int i=0;i<npcOrders.Length;i++)
            {
                orderlist[i]= (Common.GetRandom<Order>(npcOrders[i].Orders)).Item2;
            }
            orderTable.init(orderlist);
            var queueAsset = await contorller.LoadAssetBundleAsync<BarQueueAsset>(GobalSetting.BARQUEUEASSETABNAME, GobalSetting.BARQUEUEASSETRESNAME);
            Sprite[] orders = Common.GetElements<Sprite>(tags,queueAsset.Orders);
            Sprite[] waits = Common.GetElements<Sprite>(tags,queueAsset.Wait);
            npcQueue.init(tags,orders,waits);
            // DestroyImmediate(gameObject);
        }
    }
}
