using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace MainMenu
{
    public class MainMenuButton : MonoBehaviour
    {
        List<Button> ButtonList;

        private void Awake()
        {
            ButtonList = new List<Button>(this.transform.GetComponentsInChildren<Button>());
        }
        private void Start()
        {
            ButtonList[0].onClick.AddListener(StartButtonClicked);
            ButtonList[1].onClick.AddListener(ContinueButtonClicked);
            ButtonList[2].onClick.AddListener(ConfigButtonClicked);
            ButtonList[3].onClick.AddListener(QuitButtonClicked);
        }

        void StartButtonClicked()
        {
            SceneManager.LoadScene("GameScene");
        }
        void QuitButtonClicked()
        {
            Application.Quit();
        }
        void ContinueButtonClicked()
        {
            SceneManager.LoadScene("ContinueScene");
        }
        void ConfigButtonClicked()
        {
            SceneManager.LoadScene("ConfigScene");
        }
    }
}