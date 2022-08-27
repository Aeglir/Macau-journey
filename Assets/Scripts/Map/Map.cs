using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MAP
{
    public class Map : MonoBehaviour
    {
        private Place curPlace;
        private Area curArea;
        private Work curWork;
        private bool isWork;
        public Place CurPlace { get => curPlace; set => curPlace = value; }
        public Area CurArea { get => curArea; set => curArea = value; }
        public Work CurWork { get => curWork; set => curWork = value; }
        public Image listImg;
        public Sprite defaultListImg;
        public GameObject ExplorePanel;
        public GameObject WorkPanel;
        public Button WorkButton;
        public Button ExploreButton;
        public Button ExitButton;
        public Button GoButton;
        private void Awake()
        {
            WorkButton.onClick.AddListener(WorkHandleClick);
            ExploreButton.onClick.AddListener(ExploreHandleClick);
            ExitButton.onClick.AddListener(ExitHandleClick);
            GoButton.onClick.AddListener(GoHandleClick);
        }
        public void SetListImg(Sprite sp){
            listImg.sprite=sp;
            listImg.gameObject.SetActive(true);
        }
        public void ListReset()
        {
            listImg.sprite = defaultListImg;
            listImg.gameObject.SetActive(false);
        }
        public void ExploreHandleClick()
        {
            isWork = false;
            ExplorePanel.SetActive(true);
            WorkPanel.SetActive(false);
            listImg.sprite = defaultListImg;
            ExploreButton.gameObject.SetActive(false);
            WorkButton.gameObject.SetActive(true);
        }
        public void WorkHandleClick()
        {
            isWork = true;
            ExplorePanel.SetActive(false);
            WorkPanel.SetActive(true);
            listImg.sprite = defaultListImg;
            ExploreButton.gameObject.SetActive(true);
            WorkButton.gameObject.SetActive(false);
        }
        public void ExitHandleClick()
        {
            SceneManager.LoadSceneAsync(0);
        }

        public void GoHandleClick()
        {
            if(isWork)
                curWork.ToWork();
        }
    }
}
