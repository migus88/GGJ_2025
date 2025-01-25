using System;
using Cysharp.Threading.Tasks;
using Game.Code.Interfaces;
using Managers;
using Spine.Unity;
using UnityEngine;
using VContainer;

namespace Game.Code.Player
{
    public class AnimationController : MonoBehaviour
    {
        public float Inflation { get; set; }
        
        [SerializeField] private SkeletonAnimation _skeleton;

        private float _inflationAnimationDuration;
        
        private IAnimationSettings _settings;
        private IAirManager _airManager;

        [Inject]
        public void Construct(IAnimationSettings animationSettings, IAirManager airManager)
        {
            _settings = animationSettings;
            _airManager = airManager;
        }
        
        private void Awake()
        {
            PlayIdle();
            _skeleton.AnimationState.SetAnimation(1, _settings.Inflate, false);
            _inflationAnimationDuration = _skeleton.AnimationState.GetCurrent(1).Animation.Duration;
        }

        private void Update()
        {
            _skeleton.AnimationState.GetCurrent(1).TrackTime = Inflation * _inflationAnimationDuration;
            _skeleton.AnimationState.SetAnimation(2, GetStatusAnimation(), true);
        }
        
        public void PlayIdle() => PlayAnimation(_settings.Idle, true);
        public void PlayFall() => PlayAnimation(_settings.Fall, true);
        public void PlayDeath() => PlayAnimation(_settings.Death, false);
        public void PlayFly() => PlayAnimation(_settings.Fly, true);
        public void PlayJump() => PlayAnimation(_settings.Jump, false);

        public void PlayWalk(Vector2 direction)
        {
            PlayAnimation(_settings.Walk, true);

            _skeleton.Skeleton.ScaleX = direction.x switch
            {
                > 0 => 1,
                < 0 => -1,
                _ => _skeleton.Skeleton.ScaleX
            };
        }

        private void PlayAnimation(string animationName, bool isLooped)
        {
            if (_skeleton?.AnimationState == null)
            {
                return;
            }
            
            _skeleton.AnimationState.SetAnimation(0, animationName, isLooped);
        }

        private string GetStatusAnimation()
        {
            if (_airManager.CurrentAir > 0.7f)
            {
                return _settings.Healthy;
            }

            if (_airManager.CurrentAir > 0.3f)
            {
                return _settings.Hurt;
            }

            return _settings.AlmostDead;
        }
    }
}