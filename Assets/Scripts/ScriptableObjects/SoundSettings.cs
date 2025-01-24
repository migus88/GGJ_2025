using UnityEngine;
using VContainer;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SoundSettings", menuName = "GGJ25/SoundSettings")]
    public class SoundSettings : ScriptableObject
    {
        [Range(0f, 2f)] 
        [field:SerializeField] public float Pitch;

        [Inject] 
        public AudioSource AudioSource;
        
        [InspectorName("Sounds")]
        [field:SerializeField] public AudioClip LobbyMusic;
        [field:SerializeField] public AudioClip GameplayMusic;
        [field:SerializeField] public AudioClip[] AccelerationSounds;
        [field:SerializeField] public AudioClip[] CollisionSounds;
        [field:SerializeField] public AudioClip[] DeflationSounds;
        [field: SerializeField] public AudioClip[] InflationSounds;
    }
}
