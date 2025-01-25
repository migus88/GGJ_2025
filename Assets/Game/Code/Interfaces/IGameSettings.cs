using UnityEngine;

namespace ScriptableObjects
{
    public interface IGameSettings
    {
        LayerMask GroundLayer { get; }
        float MaxFallDistance { get; }
        float FlightDuration { get; }
        float FlightAirUsageRate { get; }
        float AirUsageRate { get; }
        float MaxSpeed { get; }
        float MovementSpeed { get; }
        float JumpForce { get; }
        float FlyingSpeed { get; }
        float RotationSpeed { get; }
        float RegularGravity { get; }
        float FlyingGravity { get; }
        float FallingGravity { get; }
        float MovementDrag { get; }
        float FlyingDrag { get; }
        PhysicsMaterial2D MovementMaterial { get; }
        PhysicsMaterial2D FlyingMaterial { get; }
    }
}