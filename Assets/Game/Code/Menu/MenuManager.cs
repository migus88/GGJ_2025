using DG.Tweening;
using Managers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

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

        [field: InspectorName("Sound Settings")]
        [field: SerializeField] private Slider _musicSlider;
        [field: SerializeField] private Slider _sfxSlider;
        [field: SerializeField] private Button _saveSettingsBtn;
        [field: SerializeField] private Button _closeButton;
        [field: SerializeField] private SoundSettings _soundSettings;
        
        private void OnEnable()
        {
            Vector3 cameraPosition = _bgGameObject.transform.position;
            cameraPosition.z = _cameraTransform.position.z;
            _cameraTransform.position = cameraPosition;

            _newGameButton.onClick.AddListener(HandleNewGameClick);
            _optionButton.onClick.AddListener(HandleOptionsClick);
            _creditsButton.onClick.AddListener(HandleCreditsClick);
            _exitButton.onClick.AddListener(HandleExitClick);
            _saveSettingsBtn.onClick.AddListener(HandleSaveSettingsClick);
            _closeButton.onClick.AddListener(HandleCloseClick);
        }

        private void HandleCloseClick()
        {
            _optionsPanel.transform.DOMoveY(_optionsPanel.transform.position.y*4,1f);
        }

        private void HandleSaveSettingsClick()
        {
            _soundSettings.MusicVol = _musicSlider.value;
            _soundSettings.SFXVol =_sfxSlider.value;
            HandleCloseClick();
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
            _optionsPanel.transform.DOMoveY(_optionsPanel.transform.position.y/4,1f);
        }

        private void HandleNewGameClick()
        {
            SceneManager.LoadScene("GameScene");
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