using UnityEngine;

namespace ScriptableObjects
{
    public interface ISoundSettings
    {
        float MinPitch { get; }
        float MaxPitch { get; }
        AudioClip LobbyMusic { get; }
        AudioClip GameplayMusic { get; }
        AudioClip AccelerationSound { get;}
        AudioClip CollisionSound { get;}
        AudioClip DeflationSound { get; }
        AudioClip InflationSound { get; }
    }
}