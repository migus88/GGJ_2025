using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using VContainer.Unity;

namespace Managers
{
    public class TimeService : ITimeService, ITickable
    {
        public event Action SecondPassed;
        public long SecondsPassed { get; private set; }
        public bool IsActive { get; private set; }
        
        private long _previousSecondsPassed;

        public void Start()
        {
            IsActive = true;
            SecondsPassed = 0;
            SecondTicker().Forget();
        }

        public void Pause()
        {
            IsActive = false;
        }

        private async UniTaskVoid SecondTicker()
        {
            var second = TimeSpan.FromSeconds(1);
            while (Application.exitCancellationToken.IsCancellationRequested == false)
            {
                await UniTask.Delay(second, cancellationToken: Application.exitCancellationToken);
                SecondsPassed++;
            }
        }

        public void Tick()
        {
            if (SecondsPassed != _previousSecondsPassed)
            {
                _previousSecondsPassed = SecondsPassed;
                SecondPassed?.Invoke();
            }
        }
    }
}