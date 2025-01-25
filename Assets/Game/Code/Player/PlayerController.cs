using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Code.Interfaces;
using Managers;
using ScriptableObjects;
using UnityEngine;
using VContainer;

namespace Game.Code.Player
{
    public class PlayerController : MonoBehaviour, IPositionProvider
    {
        public Vector3 Position => transform.position;
        
        [SerializeField] private Collider2D _groundChecker;
        [SerializeField] private GameObject _accelerationEffect;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _rotationPivot;
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _view;
        

        private PlayerState _state;
        private float _previousHeight;
        private float _fallingDistance;
        private CancellationTokenSource _flightCancellationTokenSource;

        // Initialization
        private IGameSettings _gameSettings;
        private IInputService _inputService;
        private IAirManager _airManager;

        [Inject]
        public void Construct(IGameSettings gameSettings, IInputService inputService, IAirManager airManager)
        {
            _gameSettings = gameSettings;
            _inputService = inputService;
            _airManager = airManager;
            
            _rigidbody.linearDamping = _gameSettings.MovementDrag;
        }

        private void Update()
        {
            if (_state == PlayerState.Dead)
            {
                return;
            }
            
            UpdateGroundedState();
            HandleGravity();
            HandleFall();
            HandleLanding();
            HandleAcceleration();

            HandleDeath();
        }

        private void FixedUpdate()
        {
            if (_state == PlayerState.Dead)
            {
                return;
            }
            
            ControlFlight();
            Move();
            
            
            // Clamp the velocity to the maximum speed
            if (_rigidbody.linearVelocity.magnitude > _gameSettings.MaxSpeed)
            {
                _rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * _gameSettings.MaxSpeed;
            }
        }

        private void HandleDeath()
        {
            if (_airManager.CurrentAir <= 0)
            {
                Die(false).Forget();
            }
        }

        private async UniTaskVoid Die(bool isFallDeath)
        {
            _state = PlayerState.Dead;
            _accelerationEffect.SetActive(false);
            _rigidbody.linearVelocity = Vector2.zero;
            _airManager.PauseDeflation();
            await _animationController.PlayDeath(!isFallDeath);
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            
            _rigidbody.position = _spawnPoint.position;

            Spawn().Forget();
        }

        private async UniTaskVoid Spawn()
        {
            _view.SetActive(true);

            await UniTask.Yield();
            
            transform.position = _spawnPoint.position;
            _airManager.AddAir(1f);
            _fallingDistance = 0;
            _rigidbody.rotation = 0;
            _previousHeight = _rigidbody.position.y;
            
            await _animationController.PlaySpawn();
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            
            _airManager.StartDeflation();
            _state = PlayerState.Spawning;
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
                return;
            }
            
            if(_inputService.ConsumeJumpPress() && _state == PlayerState.Grounded)
            {
                _animationController.PlayJump();
                _rigidbody.AddForce(Vector2.up * _gameSettings.JumpForce, ForceMode2D.Impulse);
            }

            if (_inputService.ConsumeLeftPress())
            {
                if(_state == PlayerState.Grounded)
                {
                    _animationController.PlayWalk(Vector2.left);
                }
                _rigidbody.AddForce(Vector2.left * _gameSettings.MovementSpeed);
            }
            else if (_inputService.ConsumeRightPress())
            {
                if(_state == PlayerState.Grounded)
                {
                    _animationController.PlayWalk(Vector2.right);
                }
                _rigidbody.AddForce(Vector2.right * _gameSettings.MovementSpeed);
            }
            else if(_state == PlayerState.Grounded)
            {
                _animationController.PlayIdle();
            }
        }

        private void StartFlying()
        {
            _state = PlayerState.Flying;
            _animationController.PlayFly();
            _animationController.Inflation = 1f;

            _rigidbody.sharedMaterial = _gameSettings.FlyingMaterial;
            _rigidbody.linearVelocity *= 0.5f; // Slowing down the inertia
            _rigidbody.gravityScale = _gameSettings.FlyingGravity;
            _rigidbody.linearDamping = _gameSettings.FlyingDrag;
            _groundChecker.enabled = false;
            _flightCancellationTokenSource = new();
            _airManager.AddAir(-_gameSettings.FlightAirUsageRate);
            
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
            if (_state == PlayerState.Flying)
            {
                _fallingDistance = 0;
                _previousHeight = _rigidbody.position.y;
            }
            
            if (_state != PlayerState.Grounded)
            {
                return;
            }
            
            if (_fallingDistance >= _gameSettings.MaxFallDistance)
            {
                Die(true).Forget();
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
                    _animationController.PlayFall();
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

            _state = _groundChecker.IsTouchingLayers(_gameSettings.GroundLayer)
                ? PlayerState.Grounded
                : PlayerState.Falling;
        }
    }
}