using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenuButton : MonoBehaviour
    {
        Button btn;

        private void Awake()
        {
            btn = GetComponent<Button>();
        }
        private void Start()
        {
            switch (transform.name)
            {
                case "Start":
                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(StartButtonClicked);
                    break;
                case "Continue":
                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(RestartButtonClicked);
                    break;
                case "Config":
                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(ConfigButtonClicked);
                    break;
                case "Exit":
                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(QuitButtonClicked);
                    break;
                default:
                    break;
            }
        }

        void StartButtonClicked()
        {
            SceneManager.LoadScene("GameScene");
        }
        void QuitButtonClicked()
        {
            Application.Quit();
        }
        void RestartButtonClicked()
        {
            SceneManager.LoadScene("LoadScene");
        }
        void ConfigButtonClicked()
        {
            SceneManager.LoadScene("ConfigScene");
        }
    }
}