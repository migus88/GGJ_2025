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
        [field: SerializeField] private Transform _optionsPlaceholder;

        private float _originalOptionsYPos;

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
            _musicSlider.onValueChanged.AddListener(HandleVolumeChanged);
        }

        private void HandleVolumeChanged(float arg0)
        {
            GetComponent<AudioSource>().volume = arg0;
        }

        private void HandleCloseClick()
        {
            _optionsPanel.transform.DOMoveY(_originalOptionsYPos, 2f)
                .OnComplete(() => _optionsPanel.SetActive(false));
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
            if (_optionsPanel.activeSelf)
            {
                HandleCloseClick();
            }
            _creditsPanel.SetActive(true);
        }

        private void HandleOptionsClick()
        {
            if (_optionsPanel.activeSelf)
            {
                HandleCloseClick();
                return;
            }
            _optionsPanel.SetActive(true);
            _originalOptionsYPos = _optionsPanel.transform.position.y;
            _optionsPanel.transform.DOMoveY(_optionsPlaceholder.position.y,1f);
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
            _saveSettingsBtn.onClick.RemoveListener(HandleSaveSettingsClick);
            _closeButton.onClick.RemoveListener(HandleCloseClick);
            _musicSlider.onValueChanged.RemoveListener(HandleVolumeChanged);
        }
    }
}