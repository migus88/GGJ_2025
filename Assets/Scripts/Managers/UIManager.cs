using System;
using Managers;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, IUIManager
{
    [SerializeField] private TextMeshProUGUI _distanceTXT;
    [SerializeField] private TextMeshProUGUI _airAmountTXT;
    [SerializeField] private TextMeshProUGUI _timeCounterTXT;

    private TimeService _timeService;
    private DistanceManager _distanceManager;
    private AirManager _airManager;

    public UIManager(DistanceManager distanceManager, AirManager airManager, TimeService timeService)
    {
        _timeService = timeService;
        _distanceManager = distanceManager;
        _airManager = airManager;
    }
    public void OnEnable()
    {
        //Sign up to events;
    }

    private void SetDistance()
    {
        _distanceTXT.text = $"{_distanceManager.DistanceToExit}m";
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
