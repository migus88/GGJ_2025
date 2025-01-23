using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ControlsMapping", menuName = "GGJ25/ControlsMapping")]
    public class ControlsMapping : ScriptableObject
    {
        public KeyCode LeftRotation = KeyCode.LeftArrow;
        public KeyCode RightRotation = KeyCode.RightArrow;
        public KeyCode Throttle = KeyCode.Space;
    }
}
