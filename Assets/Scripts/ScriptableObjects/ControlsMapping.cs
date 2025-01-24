using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ControlsMapping", menuName = "GGJ25/ControlsMapping")]
    public class ControlsMapping : ScriptableObject
    {
        [field: SerializeField]public KeyCode LeftRotation { get; set; }
        [field: SerializeField]public KeyCode RightRotation { get; set; }
        [field: SerializeField]public KeyCode Throttle  { get; set; }
    }
}
