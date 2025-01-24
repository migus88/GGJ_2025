using System;
using Cysharp.Threading.Tasks;
using Spine.Unity;
using UnityEngine;

namespace Game.Proto
{
    public class FlightController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _rotationSpeed = 200f;
        [SerializeField] private float _acceleration = 5f;
        [SerializeField] private Transform _pivotPoint;
        [SerializeField] private SkeletonAnimation _skeletonAnimation;

        private float _inflation = 0f;

        private void Awake()
        {
            PlayAnimationAsync().Forget();
        }

        public void StartFlying()
        {
            _inflation = 1f;
        }

        public void StopFlying()
        {
            _inflation = 0f;
        }

        public async UniTaskVoid PlayAnimationAsync()
        {
            _skeletonAnimation.AnimationState.SetAnimation(1, "Inflate", false);
            await UniTask.DelayFrame(1);
            var animationTime = _skeletonAnimation.AnimationState.GetCurrent(1).Animation.Duration;
        
            while (!destroyCancellationToken.IsCancellationRequested)
            {
                await UniTask.DelayFrame(1);
                _skeletonAnimation.AnimationState.GetCurrent(1).TrackTime = _inflation * animationTime;
            }
        }

        public void Rotate(float direction)
        {
            var directionVector = _rigidbody.position - (Vector2)_pivotPoint.position;
            var angle = direction * _rotationSpeed * Time.deltaTime;
            directionVector = Quaternion.Euler(0, 0, angle) * directionVector;
            _rigidbody.position = (Vector2)_pivotPoint.position + directionVector;
            _rigidbody.rotation += angle;
        }

        public void Accelerate()
        {
            Vector2 force = transform.up * _acceleration;
            _rigidbody.AddForce(force);
        }
    }
}