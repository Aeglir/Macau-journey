using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MiniGame.Bar
{
    public class PausePanel : MonoBehaviour
    {
        public Button exitButton;
        public Button continueButton;
        public UnityEvent closeEvent;

        private void Awake() {
            exitButton.onClick.AddListener(ExitHandleClick);
            continueButton.onClick.AddListener(ContinueHandleClick);
        }

        public void ContinueHandleClick()
        {
            gameObject.SetActive(false);
            if(closeEvent!=null)
                closeEvent.Invoke();
        }

        public void ExitHandleClick()
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        }
    }
}
