using System;
using Managers;
using TMPro;
using UnityEngine;
using VContainer;

public class UIManager : MonoBehaviour, IUIManager
{
    [SerializeField] private TextMeshProUGUI _distanceTXT;
    [SerializeField] private TextMeshProUGUI _airAmountTXT;
    [SerializeField] private TextMeshProUGUI _timeCounterTXT;

    private TimeService _timeService;
    private DistanceManager _distanceManager;
    private AirManager _airManager;
    
    [Inject]
    public void Construct(DistanceManager distanceManager, AirManager airManager, TimeService timeService)
    {
        _distanceManager = distanceManager;
        _airManager = airManager;
        _timeService = timeService;

        
        _timeService.SecondPassed += HandleSecondPassed;
    }
    public void OnEnable()
    {
        
    }

    private void HandleSecondPassed()
    {
        SetDistance();
    }

    private void SetDistance()
    {
        _distanceTXT.text = $"{_distanceManager.DistanceToExit:F1}m";
    }

    private void SetAir()
    {
        _airAmountTXT.text = $"{_airManager.CurrentAir}%";
    }

    private void SetTime()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(_timeService.SecondsPassed);
        _timeCounterTXT.text = $"{timeSpan.Minutes}:{timeSpan.Seconds}{timeSpan.Milliseconds}";
    }
}
