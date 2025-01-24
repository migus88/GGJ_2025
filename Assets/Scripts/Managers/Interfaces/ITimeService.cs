using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;

namespace Managers
{
    public interface ITimeService
    {
        UnityEvent OnSecondPassed { get; }
        long SecondsPassed { get; }

        void StartTimer(); // Starts the timer
        void Pause(); // Pauses the timer
        void StopTimer(); // Stops the timer
        void RestartTimer(); // Restarts the timer
    }
}