using System;
using Cysharp.Threading.Tasks;
using Spine.Unity;
using UnityEngine;

public class SpineTest : MonoBehaviour
{
    [SerializeField, Range(0,1f)] private float _inflation;
    
    [SerializeField] private SkeletonAnimation _skeletonAnimation;

    private void Start()
    {
        PlayAnimationAsync().Forget();
    }

    public async UniTaskVoid PlayAnimationAsync()
    {
        _skeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
        _skeletonAnimation.AnimationState.SetAnimation(1, "Inflate", false);
        await UniTask.DelayFrame(1);
        var animationTime = _skeletonAnimation.AnimationState.GetCurrent(1).Animation.Duration;
        
        while (true)
        {
            await UniTask.DelayFrame(1);
            _skeletonAnimation.AnimationState.GetCurrent(1).TrackTime = _inflation * animationTime;
        }
    }
    
}
