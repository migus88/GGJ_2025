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

    private ITimeService _timeService;
    private IDistanceManager _distanceManager;
    private IAirManager _airManager;

    [Inject]
    public void Construct(IDistanceManager distanceManager, IAirManager airManager, ITimeService timeService)
    {
        _distanceManager = distanceManager;
        _airManager = airManager;
        _timeService = timeService;
        
        _timeService.SecondPassed += HandleSecondPassed;
    }

    private void HandleSecondPassed()
    {
        SetDistance();
        SetAir();
        SetTime();
    }

    private void SetDistance()
    {
        if (_distanceTxt)
            _distanceTxt.text = $"{_distanceManager.DistanceToExit:F1}m";
    }

    private void SetAir()
    {
        if (_airAmountTxt)
            _airAmountTxt.text = $"Air: {(_airManager.CurrentAir):P}";
    }

    private void SetTime()
    {
        if (_timeCounterTxt)
        {
            var timeSpan = TimeSpan.FromSeconds(_timeService.SecondsPassed);
            _timeCounterTxt.text = $"{timeSpan.Minutes}:{timeSpan.Seconds}{timeSpan.Milliseconds}";
        }
    }

    private void OnDestroy()
    {
        _timeService.SecondPassed -= HandleSecondPassed;
    }
}