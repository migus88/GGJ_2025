using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using Managers;
using VContainer;

namespace Game.Code.Views
{
    public class OxygenTank : MonoBehaviour
    {
        // move the item up and down in a loop using DOTween
        [SerializeField] private float _respawnRate = 20f;
        [SerializeField] private float _upDownDistance = 0.5f;
        [SerializeField] private float _upDownDuration = 1f;

        private IAirManager _airManager;

        [Inject]
        public void Construct(IAirManager airManager)
        {
            _airManager = airManager;
        }
        
        private void Start()
        {
            transform.DOMoveY(transform.position.y + _upDownDistance, _upDownDuration).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _airManager.AddAir(1f);
            Respawn().Forget();
            gameObject.SetActive(false);
        }

        private async UniTaskVoid Respawn()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_respawnRate), cancellationToken: destroyCancellationToken);
            gameObject.SetActive(true);
        }
    }
}