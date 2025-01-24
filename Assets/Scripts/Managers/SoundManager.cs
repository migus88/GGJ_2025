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
            AudioClip sound;
            var isMusic = soundType == Type.Gameplay || soundType == Type.Lobby;
            var pitch = isMusic ? 1f : Random.Range(0f,2f);
            var source = isMusic ? _mainAudioSource : _effectsAudioSource;
            
            switch (soundType)
            {
                case Type.Gameplay:
                    sound = _settings.GameplayMusic;
                    break;
                case Type.Accelaration:
                    sound = _settings.AccelerationSounds[Random.Range(0, _settings.AccelerationSounds.Length)];
                    break;
                case Type.Collision:
                    sound = _settings.CollisionSounds[Random.Range(0, _settings.CollisionSounds.Length)];
                    break;
                case Type.Deflation:
                    sound = _settings.DeflationSounds[Random.Range(0, _settings.DeflationSounds.Length)];
                    break;
                case Type.Inflation:
                    sound = _settings.InflationSounds[Random.Range(0, _settings.DeflationSounds.Length)];
                    break;
                case Type.Lobby:
                default:
                    sound = _settings.LobbyMusic;
                    break;
            }
            source.clip = sound;
            source.pitch = pitch;
            source.Play();
        }
    }
}
