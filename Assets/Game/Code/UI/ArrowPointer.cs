using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class ArrowPointer : MonoBehaviour
    {
        [SerializeField] public Transform _target;
        [SerializeField] public RectTransform _arrowUI;
        [SerializeField] public Camera _mainCamera;

        private void Start()
        {
            SetArrowDirection();
        }

        private void LateUpdate()
        {
            var targetScreenPosition = _mainCamera.WorldToScreenPoint(_target.position);

            bool isOffScreen = targetScreenPosition.x < 0 || targetScreenPosition.x > Screen.width ||
                               targetScreenPosition.y < 0 || targetScreenPosition.y > Screen.height;

            if (isOffScreen)
            {
                if (!_arrowUI.gameObject.activeSelf)
                {
                    _arrowUI.gameObject.SetActive(true); // Show the arrow if it's not already visible
                }

                var clampedPosition = targetScreenPosition;
                clampedPosition.x = Mathf.Clamp(clampedPosition.x, 50, Screen.width - 50);
                clampedPosition.y = Mathf.Clamp(clampedPosition.y, 50, Screen.height - 50);

                _arrowUI.position = clampedPosition;

                var direction = _target.position - _mainCamera.transform.position;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _arrowUI.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
            else
            {
                if (_arrowUI.gameObject.activeSelf)
                {
                    _arrowUI.gameObject.SetActive(false); // Hide the arrow if it's on-screen
                }
            }
        }

        private void SetArrowDirection()
        {
            var direction = _target.position - _mainCamera.transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _arrowUI.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}
