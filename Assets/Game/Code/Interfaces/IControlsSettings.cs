using UnityEngine;

namespace ScriptableObjects
{
    public interface IControlsSettings
    {
        KeyCode Left { get; set; }
        KeyCode Right { get; set; }
        KeyCode Acceleration { get; set; }
        KeyCode Inflation { get; set; }
        KeyCode Deflation { get; set; }
        KeyCode Jump { get; set; }
    }
}