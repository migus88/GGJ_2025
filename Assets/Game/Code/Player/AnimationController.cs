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

        public async UniTask PlaySpawn()
        {
            await UniTask.Yield();
            PlayAnimation(_settings.Spawn, false);
            
            var anim = _skeleton.AnimationState.GetCurrent(0);
            
            if(anim != null)
            {
                // await for animation to finish
                await UniTask.WaitWhile(() => !anim.IsComplete);
            }
            
            Debug.Log("Spawned");
            
            PlayIdle();
        }
        
        public void PlayIdle() => PlayAnimation(_settings.Idle, true);

        public void PlayFall()
        {
            PlayAnimation(_settings.Fall, true);
            _skeleton.AnimationState.ClearTrack(1);
        }

        public async UniTask PlayDeath(bool isMidAir)
        {
            _skeleton.AnimationState.ClearTrack(0);
            _skeleton.AnimationState.ClearTrack(1);
            
            await UniTask.Yield();
            PlayAnimation(isMidAir ? _settings.MidAirDeath : _settings.Death, false);
            
            var anim = _skeleton.AnimationState.GetCurrent(0);
            
            if(anim != null)
            {
                // await for animation to finish
                await UniTask.WaitWhile(() => !anim.IsComplete);
            }
            
            Debug.Log("Died");
        }

        public void PlayFly()
        {
            PlayAnimation(_settings.Fly, true);
            _skeleton.AnimationState.SetAnimation(1, _settings.Inflate, false);
        }

        public void PlayJump()
        {
            PlayAnimation(_settings.Jump, false);
            _skeleton.AnimationState.AddAnimation(0, _settings.Fall, true, _skeleton.AnimationState.GetCurrent(0).Animation.Duration);
        }

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