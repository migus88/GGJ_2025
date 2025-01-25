using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class ArrowPointer : MonoBehaviour
    {
        [field: SerializeField] public Transform Target;
        [field: SerializeField] public RectTransform ArrowUI;
        [field: SerializeField] public Camera MainCamera;

        void Start()
        {
            SetArrowDirection();
        }

        void FixedUpdate()
        {
            var targetScreenPosition = MainCamera.WorldToScreenPoint(Target.position);

            bool isOffScreen = targetScreenPosition.x < 0 || targetScreenPosition.x > Screen.width ||
                               targetScreenPosition.y < 0 || targetScreenPosition.y > Screen.height;

            if (isOffScreen)
            {
                if (!ArrowUI.gameObject.activeSelf)
                {
                    ArrowUI.gameObject.SetActive(true); // Show the arrow if it's not already visible
                }

                var clampedPosition = targetScreenPosition;
                clampedPosition.x = Mathf.Clamp(clampedPosition.x, 50, Screen.width - 50);
                clampedPosition.y = Mathf.Clamp(clampedPosition.y, 50, Screen.height - 50);

                ArrowUI.position = clampedPosition;

                var direction = Target.position - MainCamera.transform.position;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                ArrowUI.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
            else
            {
                if (ArrowUI.gameObject.activeSelf)
                {
                    ArrowUI.gameObject.SetActive(false); // Hide the arrow if it's on-screen
                }
            }
        }

        private void SetArrowDirection()
        {
            var direction = Target.position - MainCamera.transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            ArrowUI.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}
