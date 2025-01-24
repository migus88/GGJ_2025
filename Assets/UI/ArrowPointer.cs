using UnityEngine;

namespace UI
{
    public class ArrowPointer : MonoBehaviour
    {
        public Transform target;
        public RectTransform arrowUI;
        public Camera mainCamera;

        void Update()
        {
            Vector3 targetScreenPosition = mainCamera.WorldToScreenPoint(target.position);

            bool isOffScreen = targetScreenPosition.x < 0 || targetScreenPosition.x > Screen.width ||
                               targetScreenPosition.y < 0 || targetScreenPosition.y > Screen.height;

            if (isOffScreen)
            {
                Vector3 clampedPosition = targetScreenPosition;
                clampedPosition.x = Mathf.Clamp(clampedPosition.x, 50, Screen.width - 50);
                clampedPosition.y = Mathf.Clamp(clampedPosition.y, 50, Screen.height - 50);

                arrowUI.position = clampedPosition;

                Vector3 direction = target.position - mainCamera.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                arrowUI.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
            else
            {
                arrowUI.gameObject.SetActive(false);
            }
        }
    }
}