using Managers;
using UnityEngine;

public class ManagerPlaceholder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private MazeExitObject _mazeExitObject;
    [SerializeField]
    private GameObject _player;

    private DistanceManager _distanceManager;
    private AirManager _airManager;
    private TimeService _timeService;
    [SerializeField]
    private UIManager _uiManager;

    void Start()
    {
        
        _timeService = new TimeService();
        _timeService.StartTimer();
        _distanceManager = new DistanceManager(_mazeExitObject, _player,_timeService);
        _airManager = new AirManager(_timeService);
        _uiManager = gameObject.GetComponent<UIManager>();
        _uiManager.Construct(_distanceManager,_airManager,_timeService);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
