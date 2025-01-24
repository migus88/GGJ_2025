using ScriptableObjects;
using UnityEngine;

namespace Managers
{
    public class SoundManager : ISoundManager
    {
        private SoundSettings _settings;
        private AudioSource _mainAudioSource;
        private AudioSource _effectsAudioSource;

        public enum Type
        {
            Lobby,
            Gameplay,
            Accelaration,
            Collision,
            Deflation,
            Inflation
        }
        public SoundManager(SoundSettings settings)
        {
            _settings = settings;
            _mainAudioSource = new AudioSource();
            _mainAudioSource.pitch = _settings.Pitch;
        }
        
        public void PlaySound(Type soundType = Type.Lobby)
        {
            AudioSource source = _mainAudioSource;
            AudioClip sound;
            float pitch;
            
            switch (soundType)
            {
                case Type.Gameplay:
                    pitch = _settings.Pitch;
                    sound = _settings.GameplayMusic;
                    break;
                case Type.Accelaration:
                    source = _effectsAudioSource;
                    pitch = Random.Range(0f,2f);
                    sound = _settings.AccelerationSounds[Random.Range(0, _settings.AccelerationSounds.Length)];
                    break;
                case Type.Collision:
                    source = _effectsAudioSource;
                    pitch = Random.Range(0f,2f);
                    sound = _settings.CollisionSounds[Random.Range(0, _settings.CollisionSounds.Length)];
                    break;
                case Type.Deflation:
                    source = _effectsAudioSource;
                    pitch = Random.Range(0f,2f);
                    sound = _settings.DeflationSounds[Random.Range(0, _settings.DeflationSounds.Length)];
                    break;
                case Type.Inflation:
                    source = _effectsAudioSource;
                    pitch = Random.Range(0f,2f);
                    sound = _settings.InflationSounds[Random.Range(0, _settings.DeflationSounds.Length)];
                    break;
                case Type.Lobby:
                default:
                    pitch = _settings.Pitch;
                    sound = _settings.LobbyMusic;
                    break;
            }
            source.clip = sound;
            source.pitch = pitch;
            source.Play();
        }
    }
}
