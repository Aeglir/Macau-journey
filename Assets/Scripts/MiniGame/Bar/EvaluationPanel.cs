using DG.Tweening;
using MiniGame.Bar.Datas;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGame.Bar
{
    public class ThirdInfo
    {
        public string text;
        public Sprite sprite;
        public ThirdInfo(Evaluation eva, int imgIndex = 0)
        {
            this.text = eva.Text;
            this.sprite = eva.Img[imgIndex];
        }
    }
    public class EvaluationPanel : MonoBehaviour
    {
#if DEBUGMODE
        public bool debug;
        public int debugEva;
#endif
        public NPCQUEUE.NpcQueueController controller;
        public GameObject content;
        public GameObject[] PageList;
        public Image[] textContainer;
        public Text[] textCom;
        private Image First;
        private Image Second;
        private Image Third;
        private Image curTextContainer;
        private Sprite[][] first;
        private Sprite[] second;
        private ThirdInfo[][] thirds;
        private Sequence[] sequences;
        private Tweener tweener;
        private int cur;
        private int clickCount;
        private void Awake()
        {
            cur = 0;
            content.SetActive(false);
            First = PageList[0].GetComponent<Image>();
            Second = PageList[1].GetComponent<Image>();
            Third = PageList[2].GetComponent<Image>();
            textCom = new Text[3];
            textCom[0] = textContainer[0].GetComponentInChildren<Text>();
            var tc = textContainer[1].GetComponentsInChildren<Text>();
            textCom[1] = tc[0];
            textCom[2] = tc[1];
            sequences = new Sequence[3];
        }
        public void init(Sprite[][] first, Sprite[] second, ThirdInfo[][] thirds)
        {
            this.first = first;
            this.second = second;
            this.thirds = thirds;
#if DEBUGMODE
            if (debug)
            {
                StartEva();
            }
#endif
        }
#if DEBUGMODE
        private void StartEva()
        {
            Show(debugEva);
        }
#endif
        public void Show(int eva)
        {
            if (cur >= GobalSetting.MAXORDERS)
                return;
            clickCount = 0;
            First.sprite = first[cur][eva];
            Second.sprite = second[cur];
            Third.sprite = thirds[cur][eva].sprite;
            string text = thirds[cur][eva].text;
            string[] texts = text.Split('$');
            if (texts.Length > 1)
            {
                curTextContainer = textContainer[1];
                textCom[1].text = texts[0];
                textCom[2].text = texts[1];
            }
            else
            {
                curTextContainer = textContainer[0];
                textCom[0].text = texts[0];
            }
            // curTextContainer.SetActive(true);
            content.SetActive(true);
            WaitForNextClick();
        }
        private void WaitForNextClick()
        {
            switch (clickCount)
            {
                case 0:
                    SetFirstSequence();
                    First.gameObject.SetActive(true);
                    break;
                case 1:
                    SetSecondSequence();
                    Second.gameObject.SetActive(true);
                    break;
                case 2:
                    SetThirdSequence();
                    Third.gameObject.SetActive(true);
                    break;
                case 3:
                    SetTextSequence();
                    curTextContainer.gameObject.SetActive(true);
                    break;
                case 4:
                    SetFinishSequence();
                    Finish();
                    break;
            }
        }
        private void SetFirstSequence()
        {
            sequences[0] = DOTween.Sequence(First.DOFade(1, GobalSetting.EVAIMAGEDUR));
            sequences[0].Join(First.rectTransform.DOAnchorPosX(GobalSetting.FIRSTGOAL, GobalSetting.EVAIMAGEDUR));
        }
        private void SetSecondSequence()
        {
            sequences[1] = DOTween.Sequence(Second.DOFade(1, GobalSetting.EVAIMAGEDUR));
            sequences[1].Join(Second.rectTransform.DOAnchorPosX(GobalSetting.SECONDGOAL, GobalSetting.EVAIMAGEDUR));
        }
        private void SetThirdSequence()
        {
            sequences[2] = DOTween.Sequence(Third.DOFade(1, GobalSetting.EVAIMAGEDUR));
            sequences[2].Join(Third.rectTransform.DOAnchorPosX(GobalSetting.THIRDGOAL, GobalSetting.EVAIMAGEDUR));
        }
        private void SetTextSequence()
        {
            tweener = curTextContainer.rectTransform.DOAnchorPosX(GobalSetting.EVATEXTGOAL, GobalSetting.EVATEXTDUR);
        }
        private void SetFinishSequence()
        {
            foreach (var s in sequences)
            {
                s.Pause();
                s.Kill();
            }
            tweener.Pause();
            tweener.Kill();
        }
        public void MouseClick()
        {
            if (clickCount >= 4)
                return;
            clickCount++;
            WaitForNextClick();
        }
        public void Reset()
        {
            clickCount = 0;

            ResetFirst();
            ResetSecond();
            ResetThird();
            ResetTextCur();
            content.SetActive(false);
        }
        private void ResetFirst()
        {
            First.gameObject.SetActive(false);
            First.color = new Color(1, 1, 1, 0);
            Vector3 pos = First.rectTransform.anchoredPosition3D;
            pos.x = GobalSetting.FIRSTSTART;
            First.rectTransform.anchoredPosition3D = pos;
        }
        private void ResetSecond()
        {
            Second.gameObject.SetActive(false);
            Second.color = new Color(1, 1, 1, 0);
            Vector3 pos = Second.rectTransform.anchoredPosition3D;
            pos.x = GobalSetting.SECONDSTART;
            Second.rectTransform.anchoredPosition3D = pos;
        }
        private void ResetThird()
        {
            Third.gameObject.SetActive(false);
            Third.color = new Color(1, 1, 1, 0);
            Vector3 pos = Third.rectTransform.anchoredPosition3D;
            pos.x = GobalSetting.THIRDSTART;
            Third.rectTransform.anchoredPosition3D = pos;
        }
        private void ResetTextCur()
        {
            if (curTextContainer != null)
            {
                curTextContainer.gameObject.SetActive(false);
                Vector2 pos = curTextContainer.rectTransform.anchoredPosition;
                pos.x = GobalSetting.EVATEXTSTART;
                curTextContainer.rectTransform.anchoredPosition = pos;
                curTextContainer = null;
            }
        }
        public void Finish()
        {
            clickCount = 0;
            Reset();
            controller.FinishOrder();
        }
        public void Next()
        {
            cur++;
        }
    }
}
