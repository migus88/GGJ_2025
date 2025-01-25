using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;

namespace Managers
{
    public interface ITimeService
    {
        event Action SecondPassed;
        long SecondsPassed { get; }

        void Start();
        void Pause();
    }
}