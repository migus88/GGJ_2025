using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Code.Views
{
    public class WinScreen : MonoBehaviour
    {
        private void Start()
        {
            RestartGame().Forget();
        }

        private async UniTaskVoid RestartGame()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(10));
            SceneManager.LoadScene("GameScene");
        }
    }
}