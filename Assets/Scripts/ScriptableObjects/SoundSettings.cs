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
        
        [field: InspectorName("Sounds")]
        [field:SerializeField] public AudioClip LobbyMusic
        {
            get;
            set;
        }
        [field:SerializeField] public AudioClip GameplayMusic  
        {
            get;
            set;
        }
        [field:SerializeField] public AudioClip[] AccelerationSounds
        {
            get;
            set;
        }
        [field:SerializeField] public AudioClip[] CollisionSounds
        {
            get;
            set;
        }
        [field:SerializeField] public AudioClip[] DeflationSounds
        {
            get;
            set;
        }
        [field: SerializeField] public AudioClip[] InflationSounds
        {
            get;
            set;
        }
    }
}
