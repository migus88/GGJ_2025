using Managers;
using UnityEngine;
using VContainer;

public interface IUIManager
{
    [Inject]
    public void Construct(DistanceManager distanceManager, AirManager airManager, TimeService timeService);
}
