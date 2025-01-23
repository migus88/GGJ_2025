using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;

namespace Managers
{
    public class TimeService
    {
        public UnityEvent OnSecondPassed;
        public long SecondsPassed;
        
        private bool _isActive;
        private UniTask _timer;
        private bool _timerRunning = true;

        public void StartTimer()
        {
            _isActive = true;
            SecondTicker();
        }

        public void Pause()
        {
            _timerRunning = false;
        }
    
        public void StopTimer()
        {
            _isActive = false;
            _timer = new UniTask();
        }

        public void RestartTimer()
        {
            _timer = new UniTask();
            StartTimer();
        }

        private void SecondTicker()
        {
            _timer = UniTask.Run(async () =>
                {
                    while (_timerRunning)
                    {
                        await UniTask.Delay(TimeSpan.FromSeconds(1));
                        SecondsPassed++;
                        OnSecondPassed?.Invoke();
                    }
                }
            );
        }
    }
}