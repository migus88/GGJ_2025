using System;
using Game.Proto;
using Spine.Unity;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public bool IsFlying { get; set; } = false;

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _acceleration = 5f;
    [SerializeField] private float _maxSpeed = 10f;
    [SerializeField] private float _drag = 2f;
    [SerializeField] private KeyCode _leftKey = KeyCode.LeftArrow;
    [SerializeField] private KeyCode _rightKey = KeyCode.RightArrow;
    [SerializeField] private FlightController _flightController;
    [SerializeField] private SkeletonAnimation _skeletonAnimation;

    private void Start()
    {
        _rigidbody.linearDamping = _drag;
        _skeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
    }

    private void FixedUpdate()
    {
        if (IsFlying)
        {
            if (Input.GetKey(_leftKey))
            {
                _flightController.Rotate(1);
            }
            else if (Input.GetKey(_rightKey))
            {
                _flightController.Rotate(-1);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                _flightController.Accelerate();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                StopFlying();
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartFlying();
            _rigidbody.AddForce(Vector2.up * _acceleration);
            return;
        }

        if (Input.GetKey(_leftKey))
        {
            _rigidbody.AddForce(Vector2.left * _acceleration);
        }
        else if (Input.GetKey(_rightKey))
        {
            _rigidbody.AddForce(Vector2.right * _acceleration);
        }

        // Clamp the velocity to the maximum speed
        if (_rigidbody.linearVelocity.magnitude > _maxSpeed)
        {
            _rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * _maxSpeed;
        }
    }

    private void StartFlying()
    {
        IsFlying = true;
        _rigidbody.gravityScale = 0;
        _rigidbody.linearDamping = 0;
        _flightController.StartFlying();
    }

    private void StopFlying()
    {
        IsFlying = false;
        _rigidbody.gravityScale = 1; // or any other value for normal gravity
        _rigidbody.linearDamping = _drag;
        _flightController.StopFlying();
    }
}