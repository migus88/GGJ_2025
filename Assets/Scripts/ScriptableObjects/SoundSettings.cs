using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SoundSettings", menuName = "GGJ25/SoundSettings")]
    public class SoundSettings : ScriptableObject
    {
        [Range(0f, 2f)] 
        public float Pitch;
        
        [InspectorName("Sounds")]
        public AudioClip LobbyMusic;
        public AudioClip GameplayMusic;
        public AudioClip[] AccelerationSounds;
        public AudioClip[] CollisionSounds;
        public AudioClip[] DeflationSounds;
    }
}
