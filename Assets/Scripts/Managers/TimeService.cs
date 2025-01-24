using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;

namespace Managers
{
    public class TimeService : ITimeService
    {
        public event Action SecondPassed;
        public long SecondsPassed { get; private set; }
        private bool _isActive;
        private UniTask _timer;
        private bool _timerRunning = true;

        public void StartTimer()
        {
            _isActive = true;
            _timerRunning = true;
            SecondsPassed = 0;
            SecondTicker();
        }

        public void Pause()
        {
            _timerRunning = false;
        }
    
        public void StopTimer()
        {
            _isActive = false;
            _timerRunning = false;
        }

        public void RestartTimer()
        {
            StartTimer();
        }

        public async UniTaskVoid SecondTicker()
        {
            while (_timerRunning)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1));
                SecondsPassed++;
                SecondPassed?.Invoke();
            }
        }
    }
}