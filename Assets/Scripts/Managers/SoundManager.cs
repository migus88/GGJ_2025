using ScriptableObjects;
using UnityEngine;

namespace Managers
{
    public class SoundManager
    {
        private SoundSettings _settings;
        private AudioSource _audioSource;

        public SoundManager(SoundSettings settings)
        {
            _settings = settings;
            _audioSource = new AudioSource();
            _audioSource.pitch = _settings.Pitch;
        }

        private void PlaySound( AudioClip sound)
        {
            _audioSource.clip = sound;
            _audioSource.Play();
        }
    }
}
