using System;
using Cysharp.Threading.Tasks;
using Game.Proto;
using Spine.Unity;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public bool IsFlying { get; set; } = false;

    [SerializeField] private float _deflationDuration = 5f;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _acceleration = 5f;
    [SerializeField] private float _maxSpeed = 10f;
    [SerializeField] private float _drag = 2f;
    [SerializeField] private KeyCode _leftKey = KeyCode.LeftArrow;
    [SerializeField] private KeyCode _rightKey = KeyCode.RightArrow;
    [SerializeField] private FlightController _flightController;
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Collider2D _groundCheckCollider;
    [SerializeField] private float _maxFall = 3f;
    [SerializeField] private GameObject _fartEffect;

    private bool _leftPressed;
    private bool _rightPressed;
    private bool _spacePressed;
    private bool _isJumpPressed;
    private float _fallingDistance;
    private bool _isGrounded;
    private float _previousHeight;

    private void Start()
    {
        _rigidbody.linearDamping = _drag;
        _skeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
    }

    private void Update()
    {
        _isGrounded = IsGrounded();

        if (_isGrounded)
        {
            _rigidbody.gravityScale = 1;
        }
        else if(!IsFlying)
        {
            _rigidbody.gravityScale = 5;
        }
        
        if(!_leftPressed) _leftPressed = Input.GetKey(_leftKey);
        if(!_rightPressed) _rightPressed = Input.GetKey(_rightKey);
        if(!_spacePressed) _spacePressed = Input.GetKey(KeyCode.Space);
        if(!_isJumpPressed) _isJumpPressed = Input.GetKeyDown(KeyCode.UpArrow);
        
        if(!_isGrounded && !IsFlying)
        {
            var fallDistance = Math.Abs(_rigidbody.position.y - _previousHeight);
            _fallingDistance += fallDistance;
            _previousHeight = _rigidbody.position.y;
            Debug.Log($"Falling distance: {_fallingDistance}");
        }
        else if(_isGrounded)
        {
            if (_fallingDistance >= _maxFall)
            {
                Debug.Log("Death");
                Destroy(gameObject);
                return;
            }
            
            _fallingDistance = 0;
            _previousHeight = _rigidbody.position.y;
        }
        else
        {
            _fallingDistance = 0;
            _previousHeight = _rigidbody.position.y;
        }
        
        _fartEffect.SetActive(IsFlying && _spacePressed);
    }

    private void FixedUpdate()
    {
        if (IsFlying)
        {
            if (_leftPressed)
            {
                _leftPressed = false;
                _flightController.Rotate(1);
            }
            else if (_rightPressed)
            {
                _rightPressed = false;
                _flightController.Rotate(-1);
            }

            if (_spacePressed)
            {
                _spacePressed = false;
                _flightController.Accelerate();
            }
        }
        else
        {
            if (_spacePressed)
            {
                _spacePressed = false;
                StartFlying();
                _rigidbody.AddForce(Vector2.up * _acceleration);
                return;
            }
            
            if(_isJumpPressed)
            {
                _isJumpPressed = false;
                _rigidbody.AddForce(Vector2.up * _acceleration, ForceMode2D.Impulse);
            }

            if (_leftPressed)
            {
                _leftPressed = false;
                _rigidbody.AddForce(Vector2.left * _acceleration);
            }
            else if (_rightPressed)
            {
                _rightPressed = false;
                _rigidbody.AddForce(Vector2.right * _acceleration);
            }
        }

        // Clamp the velocity to the maximum speed
        if (_rigidbody.linearVelocity.magnitude > _maxSpeed)
        {
            _rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * _maxSpeed;
        }
    }

    private bool IsGrounded()
    {
        return _groundCheckCollider.IsTouchingLayers(_groundLayer);
    }

    private void StartFlying()
    {
        IsFlying = true;
        _rigidbody.linearVelocity *= 0.5f;
        _rigidbody.gravityScale = 0;
        _rigidbody.linearDamping = 0;
        _flightController.StartFlying();
        _groundCheckCollider.enabled = false;
        FlyAsync().Forget();
    }

    private void StopFlying()
    {
        IsFlying = false;
        _rigidbody.rotation = 0;
        _rigidbody.gravityScale = 5;
        _rigidbody.linearDamping = _drag;
        _groundCheckCollider.enabled = true;
        _flightController.StopFlying();
    }

    private async UniTaskVoid FlyAsync()
    {
        await UniTask.DelayFrame(1, cancellationToken: destroyCancellationToken);

        while (!destroyCancellationToken.IsCancellationRequested)
        {
            await UniTask.DelayFrame(1, cancellationToken: destroyCancellationToken);

            if (_flightController.Inflation > 0)
            {
                _flightController.Inflation -= Time.deltaTime / _deflationDuration;
                _flightController.Inflation = Math.Max(0, _flightController.Inflation);
            }

            if (_flightController.Inflation <= 0)
            {
                StopFlying();
                break;
            }
        }
    }
}