using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class MenuManager : MonoBehaviour
    {
        [field: SerializeField] private Transform _cameraTransform;
        [field: SerializeField] private GameObject _bgGameObject;
        [field: SerializeField] private GameObject _optionsPanel;
        [field: SerializeField] private GameObject _creditsPanel;

        [field: InspectorName("Buttons")] 
        [field: SerializeField] private Button _newGameButton;
        [field: SerializeField] private Button _optionButton;
        [field: SerializeField] private Button _creditsButton;
        [field: SerializeField] private Button _exitButton;

        private void OnEnable()
        {
            Vector3 cameraPosition = _bgGameObject.transform.position;
            cameraPosition.z = _cameraTransform.position.z;
            _cameraTransform.position = cameraPosition;

            _newGameButton.onClick.AddListener(HandleNewGameClick);
            _optionButton.onClick.AddListener(HandleOptionsClick);
            _creditsButton.onClick.AddListener(HandleCreditsClick);
            _exitButton.onClick.AddListener(HandleExitClick);
        }

        private void HandleExitClick()
        {
            Application.Quit();
        }

        private void HandleCreditsClick()
        {
            _creditsPanel.SetActive(true);
        }

        private void HandleOptionsClick()
        {
            _optionsPanel.SetActive(true);
        }

        private void HandleNewGameClick()
        {
            SceneManager.LoadScene("MainScene");
        }

        private void OnDestroy()
        {
            _newGameButton.onClick.RemoveListener(HandleNewGameClick);
            _optionButton.onClick.RemoveListener(HandleOptionsClick);
            _creditsButton.onClick.RemoveListener(HandleCreditsClick);
            _exitButton.onClick.RemoveListener(HandleExitClick);
        }
    }
}