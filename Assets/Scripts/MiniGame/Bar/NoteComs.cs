using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGame.Bar
{
    public class NoteComs : MonoBehaviour
    {
        public GameObject contain;
        public SceneAudioManager audioManager;
        public GameObject[] imgs;
        [SerializeField]
        private DBContorller contorller;
        [SerializeField]
        private Image PageOne;
        [SerializeField]
        private Image PageTwo;
        [SerializeField]
        private Button pagebutton;
        [SerializeField]
        private GameObject[] pageList;
        private Datas.BarNoteAsset asset;
        private GameObject curImg;
        private int currentPage = 0;
        private int maxPage;
        public void init(Datas.BarNoteAsset asset)
        {
            maxPage = pageList.Length;
            pagebutton.onClick.AddListener(NextPage);
            this.asset = asset;
        }
        public void Enable() => contain.SetActive(true);
        public void Disable()
        {
            Close();
            contain.SetActive(false);
        }
        public void SetImage(int n)
        {
            audioManager.Play(GobalSetting.NEXTPAGENAME);
            if (asset == null || n >= imgs.Length) return;
            if (curImg != null)
                curImg.SetActive(false);
            if (n % 24 < 12)
            {
                PageOne.sprite = asset.Notetips[n];
                PageOne.gameObject.SetActive(true);
            }
            else
            {
                PageTwo.sprite = asset.Notetips[n];
                PageTwo.gameObject.SetActive(true);
            }
            curImg = imgs[n];
            curImg.SetActive(true);
        }
        public void Close()
        {
            audioManager.Play(GobalSetting.NEXTPAGENAME);
            if (curImg != null)
            {
                curImg.SetActive(false);
                curImg = null;
            }
            PageOne.sprite = null;
            PageOne.gameObject.SetActive(false);
            PageTwo.sprite = null;
            PageTwo.gameObject.SetActive(false);
        }
        private void NextPage()
        {
            audioManager.Play(GobalSetting.NEXTPAGENAME);
            pageList[currentPage].SetActive(false);
            PageOne.gameObject.SetActive(false);
            PageTwo.gameObject.SetActive(false);
            currentPage = getNextIndex(currentPage);
            pageList[currentPage].SetActive(true);
        }
        private int getNextIndex(int index)
        {
            return (index + 1) % maxPage;
        }
    }
}
