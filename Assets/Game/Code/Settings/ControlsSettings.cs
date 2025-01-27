using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ControlsMapping", menuName = "GGJ25/ControlsMapping")]
    public class ControlsSettings : ScriptableObject, IControlsSettings
    {
        [field: SerializeField] public KeyCode Left { get; set; }
        [field: SerializeField] public KeyCode Right { get; set; }
        [field: SerializeField] public KeyCode Acceleration { get; set; }
        [field: SerializeField] public KeyCode Inflation { get; set; }
        [field: SerializeField] public KeyCode InfiniteFlight { get; set; }
        [field: SerializeField] public KeyCode Jump { get; set; }
    }
}