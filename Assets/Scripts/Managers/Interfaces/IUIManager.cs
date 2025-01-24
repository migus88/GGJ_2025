using Managers;
using UnityEngine;
using VContainer;

public interface IUIManager
{
    public void Construct(DistanceManager distanceManager, AirManager airManager, TimeService timeService);
}
