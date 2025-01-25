using Game.Code.Interfaces;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "AnimationSettings", menuName = "GGJ25/AnimationSettings")]
    public class AnimationSettings : ScriptableObject, IAnimationSettings
    {
        [field: SerializeField] public string Walk { get; set; }
        [field: SerializeField] public string Idle { get; set; }
        [field: SerializeField] public string Fall { get; set; }
        [field: SerializeField] public string Inflate { get; set; }
        [field: SerializeField] public string Fly { get; set; }
        [field: SerializeField] public string Jump { get; set; }
        [field: SerializeField] public string Healthy { get; set; }
        [field: SerializeField] public string Hurt { get; set; }
        [field: SerializeField] public string AlmostDead { get; set; }
        [field: SerializeField] public string Death { get; set; }
        [field: SerializeField] public string MidAirDeath { get; set; }
        [field: SerializeField] public string Spawn { get; set; }
    }
}