using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGame.Bar
{
    public class NoteComs : MonoBehaviour
    {
        public GameObject contain;
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
        private int currentPage=0;
        private int maxPage;
        public void init(Datas.BarNoteAsset asset)
        {
            maxPage=pageList.Length;
            pagebutton.onClick.AddListener(NextPage);
            this.asset = asset;
        }

        public void Enable()=>contain.SetActive(true);
        public void Disable()=>contain.SetActive(false);
        public void SetImage(int n)
        {
            if(asset==null) return;
            if(n%24<12)
            {
                PageOne.sprite=asset.Notetips[n];
                PageOne.gameObject.SetActive(true);
            }
            else 
            {
                PageTwo.sprite=asset.Notetips[n];
                PageTwo.gameObject.SetActive(true);
            }
        }
        private void NextPage()
        {
            pageList[currentPage].SetActive(false);
            PageOne.gameObject.SetActive(false);
            PageTwo.gameObject.SetActive(false);
            currentPage=getNextIndex(currentPage);
            pageList[currentPage].SetActive(true);
        }
        private int getNextIndex(int index)
        {
            return (index+1)%maxPage;
        }
    }
}
