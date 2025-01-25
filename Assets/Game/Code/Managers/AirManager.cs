using System;
using ScriptableObjects;
using UnityEngine;

namespace Managers
{
    public class AirManager : IAirManager
    {
        public float CurrentAir
        {
            get => _currentAir;
            private set => _currentAir = Mathf.Clamp(value, 0f, 1f);
        }

        private float _currentAir;

        private readonly ITimeService _timeService;
        private readonly IGameSettings _gameSettings;

        public AirManager(ITimeService timeService, IGameSettings gameSettings)
        {
            _timeService = timeService;
            _gameSettings = gameSettings;
        }
        
        public void AddAir(float amountOfAir)
        {
            CurrentAir += amountOfAir;
        }

        public void EmptyAir()
        {
            CurrentAir = 0;
        }

        public void StartDeflation()
        {
            _timeService.SecondPassed += HandleDeflation;
        }

        public void PauseDeflation()
        {
            _timeService.SecondPassed -= HandleDeflation;
        }

        private void HandleDeflation()
        {
            if (CurrentAir > 0f)
            {
                CurrentAir -= _gameSettings.AirUsageRate;
            }
        }
    }
}
