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
        private string _currentAnimation;

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
            var inflationAnimation = _skeleton.AnimationState.GetCurrent(1);
            
            if(inflationAnimation != null)
            {
                inflationAnimation.TrackTime = Inflation * _inflationAnimationDuration;
            }
            
            _skeleton.AnimationState.SetAnimation(2, GetStatusAnimation(), true);
        }
        
        public void PlayIdle() => PlayAnimation(_settings.Idle, true);

        public void PlayFall()
        {
            PlayAnimation(_settings.Fall, true);
            _skeleton.AnimationState.ClearTrack(1);
        }
        public void PlayDeath() => PlayAnimation(_settings.Death, false);

        public void PlayFly()
        {
            PlayAnimation(_settings.Fly, true);
            _skeleton.AnimationState.SetAnimation(1, _settings.Inflate, false);
        }
        public void PlayJump() => PlayAnimation(_settings.Jump, false);

        public void PlayWalk(Vector2 direction)
        {
            PlayAnimation(_settings.Walk, true);

            _skeleton.Skeleton.ScaleX = direction.x switch
            {
                > 0 => -1,
                < 0 => 1,
                _ => _skeleton.Skeleton.ScaleX
            };
        }

        private void PlayAnimation(string animationName, bool isLooped)
        {
            if (_skeleton?.AnimationState == null || string.IsNullOrWhiteSpace(animationName))
            {
                return;
            }
            
            if (_currentAnimation == animationName)
            {
                return;
            }
            
            _currentAnimation = animationName;
            
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