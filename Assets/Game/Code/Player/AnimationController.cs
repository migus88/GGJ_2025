using Cysharp.Threading.Tasks;
using Spine.Unity;
using UnityEngine;

namespace Game.Code.Player
{
    public class AnimationController : MonoBehaviour
    {
        public float Inflation { get; set; }
        
        [SerializeField] private SkeletonAnimation _skeleton;
        
        private void Awake()
        {
            _skeleton.AnimationState.SetAnimation(0, "Idle", true);
            _skeleton.AnimationState.SetAnimation(1, "Inflate", false);
            
            PlayAnimationAsync().Forget();
        }
        
        private async UniTaskVoid PlayAnimationAsync()
        {
            await UniTask.DelayFrame(1, cancellationToken: destroyCancellationToken);
            var animationTime = _skeleton.AnimationState.GetCurrent(1).Animation.Duration;
        
            while (!destroyCancellationToken.IsCancellationRequested)
            {
                await UniTask.DelayFrame(1, cancellationToken: destroyCancellationToken);
                _skeleton.AnimationState.GetCurrent(1).TrackTime = Inflation * animationTime;
            }
        }
    }
}