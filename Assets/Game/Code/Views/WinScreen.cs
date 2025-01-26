using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Code.Views
{
    public class WinScreen : MonoBehaviour
    {
        [SerializeField] private Button _replayButton;
        
        private void Awake()
        {
            _replayButton.onClick.AddListener(RestartGame);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Menu");
            }
        }

        private void RestartGame()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}