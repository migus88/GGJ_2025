using System;
using ScriptableObjects;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Managers
{
    public class SoundManager : MonoBehaviour, ISoundManager
    {
        [SerializeField] private AudioSource _audioSource;
        
        private ISoundSettings _settings;
        private AudioSource _effectsAudioSource;
        
        [Inject]
        public void Construct(ISoundSettings settings)
        {
            _settings = settings;
        }
        
        public void PlaySound(Type soundType = Type.Lobby)
        {
            var isMusic = soundType == Type.Gameplay || soundType == Type.Lobby;
            var pitch = isMusic ? 1f : Random.Range(_settings.MinPitch, _settings.MaxPitch);
            var source = isMusic ? _audioSource : _effectsAudioSource;

            source.clip = soundType switch
            {
                Type.Acceleration => _settings.AccelerationSound,
                Type.Gameplay => _settings.GameplayMusic,
                Type.Collision => _settings.CollisionSound,
                Type.Deflation => _settings.DeflationSound,
                Type.Inflation => _settings.InflationSound,
                Type.Lobby => _settings.LobbyMusic,
                _ => throw new ArgumentOutOfRangeException(nameof(soundType), soundType, null)
            };
            
            source.pitch = pitch;
            source.Play();
        }

        public enum Type
        {
            Lobby = 0,
            Gameplay = 1,
            Acceleration = 2,
            Collision = 3,
            Deflation = 4,
            Inflation = 5
        }

        public void SetVolume(float musicSliderValue, float sfxSliderValue)
        {
            _audioSource.volume = musicSliderValue;
            _effectsAudioSource.volume = sfxSliderValue;
        }
    }
}
