using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SoundSettings", menuName = "GGJ25/SoundSettings")]
    public class SoundSettings : ScriptableObject, ISoundSettings
    {
        [Range(0f, 2f)]
        [field: SerializeField]
        public float MinPitch { get; set; }

        [Range(0f, 2f)]
        [field: SerializeField]
        public float MaxPitch { get; set; }

        [field: SerializeField]
        public float MusicVol { get; set; }
        
        [field: SerializeField]
        public float SFXVol { get; set; }
        
        [field: SerializeField] public AudioClip LobbyMusic { get; set; }
        [field: SerializeField] public AudioClip GameplayMusic { get; set; }

        [field: SerializeField] public AudioClip[] AccelerationSounds { get; set; }
        [field: SerializeField] public AudioClip[] CollisionSounds { get; set; }
        [field: SerializeField] public AudioClip[] DeflationSounds { get; set; }
        [field: SerializeField] public AudioClip[] InflationSounds { get; set; }

        public AudioClip AccelerationSound => GetRandomClip(AccelerationSounds);
        public AudioClip CollisionSound => GetRandomClip(CollisionSounds);
        public AudioClip DeflationSound => GetRandomClip(DeflationSounds);
        public AudioClip InflationSound => GetRandomClip(InflationSounds);

        public AudioClip GetRandomClip(AudioClip[] clips) =>
            clips.Length > 0 ? clips[Random.Range(0, clips.Length)] : null;
    }
}