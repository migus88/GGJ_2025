using UnityEngine;

namespace Managers
{
    public class DistanceManager : IDistanceManager
    {
        public float DistanceToExit { get; private set; }
        private MazeExitObject _mazeExit;
        private GameObject _player;
        public DistanceManager(MazeExitObject mazeExit, GameObject player)
        {
            _mazeExit = mazeExit;
            _player = player;
        }

        private void CalculateDistance()
        {
            DistanceToExit = Vector2.Distance(_mazeExit.CurrentPosition.position, _player.transform.position);
        }
    }
}
