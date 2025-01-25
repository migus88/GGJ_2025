using UnityEngine;

namespace Game.Proto
{
    public class SmoothCameraFollow : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _player;
        
        private void LateUpdate()
        {
            if(_player == null)
            {
                return;
            }
            
            var position = _player.position;
            position.z = _camera.transform.position.z;
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, position, Time.deltaTime * 10f);
        }
    }
}