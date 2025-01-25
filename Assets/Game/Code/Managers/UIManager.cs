using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

public class UIManager : MonoBehaviour, IUIManager
{
    [SerializeField] private TextMeshProUGUI _distanceTxt;
    [SerializeField] private TextMeshProUGUI _airAmountTxt;
    [SerializeField] private TextMeshProUGUI _timeCounterTxt;

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
        _distanceTxt.text = $"{_distanceManager.DistanceToExit:F1}m";
    }

    private void SetAir()
    {
        _airAmountTxt.text = $"{_airManager.CurrentAir}%";
    }

    private void SetTime()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(_timeService.SecondsPassed);
        _timeCounterTxt.text = $"{timeSpan.Minutes}:{timeSpan.Seconds}{timeSpan.Milliseconds}";
    }
}
