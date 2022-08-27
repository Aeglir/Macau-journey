using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MiniGame.Bar
{
    public class TipsPanel : MonoBehaviour
    {
        public UnityEvent closeEvent;
        public Button nextButton;
        public Button preButton;
        public Button closeButton;
        public List<Sprite> sprites;
        public Image image;
        private int size;
        private int cur;
        private void Awake()
        {
            size = sprites.Count;
            nextButton.onClick.AddListener(NextHandelClick);
            preButton.onClick.AddListener(PreHandleClick);
            closeButton.onClick.AddListener(CloseHandleClick);
        }
        private void OnEnable()
        {
            cur = 0;
            image.sprite = sprites[cur];

        }
        public void PreHandleClick()
        {
            if (cur <= 0)
                return;
            cur--;
            image.sprite = sprites[cur];
        }
        public void NextHandelClick()
        {
            if (cur >= size - 1)
                return;
            cur++;
            image.sprite = sprites[cur];
        }
        public void CloseHandleClick()
        {
            gameObject.SetActive(false);
            if (closeEvent != null)
                closeEvent.Invoke();
        }
    }
}
