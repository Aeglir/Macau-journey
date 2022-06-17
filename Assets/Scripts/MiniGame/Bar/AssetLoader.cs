using UnityEngine;
using MiniGame.Bar.Datas;
using System.Threading.Tasks;

namespace MiniGame.Bar
{
    [DefaultExecutionOrder(-1)]
    public class AssetLoader : MonoBehaviour
    {
        public NPCQUEUE.NpcQueueController npcQueue;
        public OrderTable orderTable;
        public Mix.MixPanelCon mixPanel;
        public NoteComs note;
        public ShowPanel showPanel;
        public EvaluationPanel evaPanel;
        public SceneAudioManager audioManager;
        public ClickChecker clickChecker;
        private DBContorller contorller = new DBContorller();
        private int[] tags;
        private int[] l;
        private int[] drinkIDs;
        private async Task InitAudioManager()
        {
            var asset = await contorller.LoadAssetBundleAsync<AudioAsset>(GobalSetting.AUDIOABNAME,GobalSetting.AUDIORESNAME);
            System.Collections.Generic.Dictionary<string,AudioClip> assets=new System.Collections.Generic.Dictionary<string, AudioClip>();
            foreach(var ass in asset.clips)
            {
                assets.Add(ass.name,ass.clip);
            }
            audioManager.asset=assets;
        }
        private void InitTags()
        {
            tags = Common.GetRoandomsInEqa(0, GobalSetting.MAXNPC, GobalSetting.MAXORDERS);
            // tags = new int[5];
            // tags[0]=11;
            // tags[1]=4;
            // tags[2]=8;
            // tags[3]=12;
            // tags[4]=14;
        }
        private async Task InitNotePanel()
        {
            var noteAsset = await contorller.LoadAssetBundleAsync<BarNoteAsset>(GobalSetting.BARNOTEASSETABNAME, GobalSetting.BARNOTEASSETRESNAME);
            note.init(noteAsset);
        }
        private async Task InitMixPanel(){
            var shakerAsset = await contorller.LoadAssetBundleAsync<ShakerAsset>(GobalSetting.SHAKERSASSETABNAME, GobalSetting.SHAKERSASSETRESNAME);
            mixPanel.init(shakerAsset.Shakers);
        }
        private async Task InitOrderTable(){
            var orderAsset = await contorller.LoadAssetBundleAsync<QueueNpcAsset>(GobalSetting.QUEUENPCASSETABNAME, GobalSetting.QUEUENPCASSETRESNAME);
            var npcOrders = Common.GetElements<NpcOrders>(tags, orderAsset.NpcOrders);
            string[] drinkTexts = new string[GobalSetting.MAXORDERS];
            drinkIDs = new int[GobalSetting.MAXORDERS];
            l = new int[GobalSetting.MAXORDERS];
            for (int i = 0; i < GobalSetting.MAXORDERS; i++)
            {
                var t = Common.GetRandom<Order>(npcOrders[i].Orders);
                l[i] = t.Item1;
                drinkTexts[i]=t.Item2.Text;
                drinkIDs[i]=t.Item2.Bardrinkid;
            }
            var liquorAsset = await contorller.LoadAssetBundleAsync<LiquorDataAsset>(GobalSetting.LINQUORDATAASSETABNAME,GobalSetting.LINQUORDATAASSETRESNAME);
            orderTable.init(drinkTexts,Common.GetElements<Datas.BarDrink>(drinkIDs,liquorAsset.Bardrinks));
        }
        private async Task InitNpcQueue()
        {
            var queueAsset = await contorller.LoadAssetBundleAsync<BarQueueAsset>(GobalSetting.BARQUEUEASSETABNAME, GobalSetting.BARQUEUEASSETRESNAME);
            Sprite[] orders = Common.GetElements<Sprite>(tags, queueAsset.Orders);
            Sprite[] waits = Common.GetElements<Sprite>(tags, queueAsset.Wait);
            npcQueue.init(tags, orders, waits);
        }
        private async Task InitShowPanel(){
            LiquorIMGAsset iMGAsset = await contorller.LoadAssetBundleAsync<LiquorIMGAsset>(GobalSetting.LINQUORDATAASSETABNAME, GobalSetting.LINQUORIMGASSETRESNAME);
            Sprite[][] sprites = new Sprite[GobalSetting.EVALUATIONCOUNT][];
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = new Sprite[GobalSetting.MAXORDERS];
            }

            for (int j = 0; j < GobalSetting.MAXORDERS; j++)
            {
                int index = drinkIDs[j];
                sprites[0][j] = iMGAsset.Bad[index];
                sprites[1][j] = iMGAsset.Normal[index];
                sprites[2][j] = iMGAsset.Good[index];
            }
            showPanel.init(sprites);
        }
        private async Task InitEvaluationPanel(){
            var NIMGAsset = await contorller.LoadAssetBundleAsync<QueueNpcTAIAsset>(GobalSetting.QUEUENPCASSETABNAME, GobalSetting.QUEUETAIASSETRESNAME);
            Sprite[][] First = new Sprite[GobalSetting.MAXORDERS][];
            for (int i = 0; i < First.Length; i++)
            {
                int index = drinkIDs[i];
                First[i] = new Sprite[GobalSetting.EVALUATIONCOUNT];
                First[i][0] = NIMGAsset.First[index].Bad;
                First[i][1] = NIMGAsset.First[index].Normal;
                First[i][2] = NIMGAsset.First[index].Good;
            }
            Sprite[] Second = new Sprite[GobalSetting.MAXORDERS];
            for (int i = 0; i < Second.Length; i++)
            {
                Second[i] = NIMGAsset.Second[tags[i]];
            }
            ThirdInfo[][] thirds = new ThirdInfo[GobalSetting.MAXORDERS][];

            for (int i = 0; i < thirds.Length; i++)
            {
                thirds[i] = new ThirdInfo[GobalSetting.EVALUATIONCOUNT];
                int t = tags[i];
                thirds[i][0] = new ThirdInfo(NIMGAsset.Third[t].Bad);
                thirds[i][1] = new ThirdInfo(NIMGAsset.Third[t].Normal);
                thirds[i][2] = new ThirdInfo(NIMGAsset.Third[t].Good, l[i]);
            }
            evaPanel.init(First, Second, thirds);

        }
        private async void Awake()
        {
            InitTags();
            await InitAudioManager();
            await InitNotePanel();
            await InitMixPanel();
            await InitOrderTable();
            await InitNpcQueue();
            await InitShowPanel();
            await InitEvaluationPanel();
            contorller.ClearAb();
            audioManager.SetBack(GobalSetting.BGMNAME);
            clickChecker.enable();
            DestroyImmediate(gameObject);
        }
    }
}
