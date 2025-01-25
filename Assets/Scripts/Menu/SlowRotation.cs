using UnityEngine;

namespace Menu
{
    public class SlowRotation : MonoBehaviour
    {
        [field: SerializeField] public float rotationSpeed = 30f;
        [field: SerializeField] public bool rotateClockwise = true;

        private void FixedUpdate()
        {
            float direction = rotateClockwise ? -1f : 1f;
            transform.Rotate(0f, 0f, direction * rotationSpeed * Time.deltaTime);
        }
    }
}
