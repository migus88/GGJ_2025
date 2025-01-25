using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Settings_", menuName = "GGJ25/GameSettings")]
    public class GameSettings : ScriptableObject, IGameSettings
    {
        [field: SerializeField] public float DeflationSpeed { get; set; }
        [field:SerializeField] public float CollisionPenalty{ get; set; }
        [field:SerializeField] public float Speed{ get; set; }
    }
}
