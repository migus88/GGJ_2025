using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Settings_", menuName = "GGJ25/GameSettings")]
    public class GameSettings : ScriptableObject, IGameSettings
    {
        [field: SerializeField] public LayerMask GroundLayer { get; set; }
        [field: SerializeField] public float MaxFallDistance { get; set; }
        [field: SerializeField] public float FlightDuration { get; set; }
        [field: SerializeField] public float FlightAirUsageRate { get; set; }
        [field: SerializeField] public float AirUsageRate { get; set; }
        [field: SerializeField] public float MaxSpeed { get; set; }
        [field: SerializeField] public float MovementSpeed { get; set; }
        [field: SerializeField] public float JumpForce { get; set; }
        [field: SerializeField] public float FlyingSpeed { get; set; }
        [field: SerializeField] public float RotationSpeed { get; set; }
        [field: SerializeField] public float RegularGravity { get; set; }
        [field: SerializeField] public float FlyingGravity { get; set; }
        [field: SerializeField] public float FallingGravity { get; set; }
        [field: SerializeField] public float MovementDrag { get; set; }
        [field: SerializeField] public float FlyingDrag { get; set; }
        [field: SerializeField] public PhysicsMaterial2D MovementMaterial { get; set; }
        [field: SerializeField] public PhysicsMaterial2D FlyingMaterial { get; set; }
    }
}