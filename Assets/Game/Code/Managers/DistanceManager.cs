using System;
using Game.Code.Player;
using UnityEngine;
using VContainer.Unity;

namespace Managers
{
    public class DistanceManager : IDistanceManager, IStartable, IDisposable
    {
        public float DistanceToExit { get; private set; }
        
        private readonly IExit _exit;
        private readonly PlayerController _player;
        private readonly ITimeService _timeService;

        public DistanceManager(IExit exit, PlayerController player, ITimeService timeService)
        {
            _exit = exit;
            _player = player;
            
            _timeService = timeService;
        }

        public void Start()
        {
            _timeService.SecondPassed += HandleSecondPassed;
        }

        private void HandleSecondPassed()
        {
            CalculateDistance();
        }

        private void CalculateDistance()
        {
            DistanceToExit = Vector2.Distance(_exit.Position, _player.transform.position);
        }

        public void Dispose()
        {
            _timeService.SecondPassed -= HandleSecondPassed;
        }
    }
}
