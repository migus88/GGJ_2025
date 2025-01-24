using UnityEngine;

namespace Managers
{
    public class DistanceManager : IDistanceManager
    {
        public float DistanceToExit;
        private MazeExitObject _mazeExit;
        private GameObject _player;
        public DistanceManager(MazeExitObject mazeExit, GameObject player)
        {
            _mazeExit = mazeExit;
            _player = player;
        }
        
        public void CalculateDistance()
        {
            DistanceToExit = Vector2.Distance(_mazeExit.CurrentPosition.position, _player.transform.position);
        }
    }
}
