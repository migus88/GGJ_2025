using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Settings_", menuName = "GGJ25/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public float DeflationSpeed = 0;
        public float CollisionPenalty = 0;
        public float Speed = 0;
    }
}
