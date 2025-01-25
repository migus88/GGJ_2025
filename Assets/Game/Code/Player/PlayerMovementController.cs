using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Code.Interfaces;
using ScriptableObjects;
using Spine.Unity;
using UnityEngine;
using VContainer;

namespace Game.Code.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Collider2D _groundChecker;
        [SerializeField] private GameObject _accelerationEffect;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _rotationPivot;
        [SerializeField] private AnimationController _animationController;

        private PlayerState _state;
        private float _previousHeight;
        private float _fallingDistance;
        private CancellationTokenSource _flightCancellationTokenSource;

        // Initialization
        private IGameSettings _gameSettings;
        private IControlsSettings _controlsSettings;
        private IInputService _inputService;

        [Inject]
        public void Construct(IGameSettings gameSettings, IControlsSettings controlsSettings,
            IInputService inputService)
        {
            _gameSettings = gameSettings;
            _controlsSettings = controlsSettings;
            _inputService = inputService;
        }

        private void Update()
        {
            UpdateGroundedState();
            HandleGravity();
            HandleFall();
            HandleLanding();
            HandleAcceleration();
        }

        private void FixedUpdate()
        {
            ControlFlight();
            Move();
        }

        private void Move()
        {
            if (_state == PlayerState.Flying)
            {
                return;    
            }
            
            if (_inputService.ConsumeInflationPress())
            {
                StartFlying();
                _rigidbody.AddForce(Vector2.up * _gameSettings.FlyingSpeed);
                return;
            }
            
            if(_inputService.ConsumeJumpPress())
            {
                _rigidbody.AddForce(Vector2.up * _gameSettings.JumpForce, ForceMode2D.Impulse);
            }

            if (_inputService.ConsumeLeftPress())
            {
                _rigidbody.AddForce(Vector2.left * _gameSettings.MovementSpeed);
            }
            else if (_inputService.ConsumeRightPress())
            {
                _rigidbody.AddForce(Vector2.right * _gameSettings.MovementSpeed);
            }
        }

        private void StartFlying()
        {
            _state = PlayerState.Flying;

            _rigidbody.sharedMaterial = _gameSettings.FlyingMaterial;
            _rigidbody.linearVelocity *= 0.5f; // Slowing down the inertia
            _rigidbody.gravityScale = _gameSettings.FlyingGravity;
            _rigidbody.linearDamping = _gameSettings.FlyingDrag;
            _groundChecker.enabled = false;
            _flightCancellationTokenSource = new();
            
            Fly(_flightCancellationTokenSource.Token).Forget();
        }
        
        private void StopFlying()
        {
            _state = PlayerState.Falling;
            
            _rigidbody.sharedMaterial = _gameSettings.MovementMaterial;
            _rigidbody.rotation = 0;
            _rigidbody.gravityScale = _gameSettings.FallingGravity;
            _rigidbody.linearDamping = _gameSettings.MovementDrag;
            _groundChecker.enabled = true;
            _animationController.Inflation = 0f;
        }
        
        private async UniTaskVoid Fly(CancellationToken cancellationToken)
        {
            await UniTask.DelayFrame(1, cancellationToken: destroyCancellationToken);

            while (!destroyCancellationToken.IsCancellationRequested && !cancellationToken.IsCancellationRequested)
            {
                await UniTask.DelayFrame(1, cancellationToken: destroyCancellationToken);

                if (_animationController.Inflation > 0)
                {
                    _animationController.Inflation -= Time.deltaTime / _gameSettings.FlightDuration;
                    _animationController.Inflation = Math.Max(0, _animationController.Inflation);
                }

                if (_animationController.Inflation > 0)
                {
                    continue;
                }
                
                StopFlying();
                break;
            }
        }

        private void ControlFlight()
        {
            if (_state != PlayerState.Flying)
            {
                return;
            }
            
            if (_inputService.ConsumeLeftPress())
            {
                Rotate(1);
            }
            else if (_inputService.ConsumeRightPress())
            {
                Rotate(-1);
            }

            if (_inputService.ConsumeAccelerationPress())
            {
                Accelerate();
            }

            if (_inputService.ConsumeDeflationPress())
            {
                _flightCancellationTokenSource.Cancel();
            }
        }

        private void Accelerate()
        {
            var force = transform.up * _gameSettings.FlyingSpeed;
            _rigidbody.AddForce(force);
        }

        private void Rotate(float direction)
        {
            var directionVector = _rigidbody.position - (Vector2)_rotationPivot.position;
            var angle = direction * _gameSettings.RotationSpeed * Time.deltaTime;
            directionVector = Quaternion.Euler(0, 0, angle) * directionVector;
            _rigidbody.position = (Vector2)_rotationPivot.position + directionVector;
            _rigidbody.rotation += angle;
        }

        private void HandleAcceleration()
        {
            _accelerationEffect.SetActive(_state == PlayerState.Flying && _inputService.IsAccelerationPressed);
        }

        private void HandleLanding()
        {
            if (_state != PlayerState.Grounded)
            {
                return;
            }
            
            if (_fallingDistance >= _gameSettings.MaxFallDistance)
            {
                Debug.Log("Death");
                Destroy(gameObject); // TODO: Respawn instad
                return;
            }
            
            _fallingDistance = 0;
            _previousHeight = _rigidbody.position.y;
        }
        
        private void HandleFall()
        {
            if(_state != PlayerState.Falling)
            {
                return;
            }
            
            var fallDistance = Math.Abs(_rigidbody.position.y - _previousHeight);
            _fallingDistance += fallDistance;
            _previousHeight = _rigidbody.position.y;
        }

        private void HandleGravity()
        {
            switch (_state)
            {
                case PlayerState.Falling:
                    _rigidbody.gravityScale = _gameSettings.FallingGravity;
                    break;
                case PlayerState.Flying:
                    _rigidbody.gravityScale = _gameSettings.FlyingGravity;
                    break;
                case PlayerState.Grounded:
                default:
                    _rigidbody.gravityScale = _gameSettings.RegularGravity;
                    break;
            }
        }

        private void UpdateGroundedState()
        {
            if (_state == PlayerState.Flying)
            {
                return;
            }

            var isGrounded = _groundChecker.IsTouchingLayers(_gameSettings.GroundLayer);

            if (isGrounded)
            {
                _state = PlayerState.Grounded;
            }
        }
    }
}