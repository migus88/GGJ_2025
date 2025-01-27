using Game.Code.Interfaces;
using ScriptableObjects;
using UnityEngine;
using VContainer.Unity;

namespace Managers
{
    public class InputService : IInputService, ITickable, IPostFixedTickable
    {

        public bool IsLeftPressed => _isLeftPressed;
        public bool IsRightPressed => _isRightPressed;
        public bool IsAccelerationPressed => _isAccelerationPressed;
        public bool IsInflationPressed => _isInflationPressed;
        public bool IsInfiniteFlightSelected => _isInfiniteFlightSelected;
        public bool IsJumpPressed => _isJumpPressed;
        
        private bool _isLeftPressed;
        private bool _isRightPressed;
        private bool _isJumpPressed;
        private bool _isAccelerationPressed;
        private bool _isInflationPressed;
        private bool _isInfiniteFlightSelected;
        
        private readonly IControlsSettings _settings;
        
        private int _infiniteFlightPressCounter;
        private float _lastTimeInfiniteFlightPressed;

        public InputService(IControlsSettings settings)
        {
            _settings = settings;
        }
        
        public void Tick()
        {
            HandleInfiniteFlightPressReset();
            
            if (!_isLeftPressed) _isLeftPressed = Input.GetKey(_settings.Left);
            if (!_isRightPressed) _isRightPressed = Input.GetKey(_settings.Right);
            if (!_isJumpPressed) _isJumpPressed = Input.GetKeyDown(_settings.Jump);
            if (!_isAccelerationPressed) _isAccelerationPressed = Input.GetKey(_settings.Acceleration);
            if (!_isInflationPressed) _isInflationPressed = Input.GetKeyDown(_settings.Inflation);
            
            if (!_isInfiniteFlightSelected)
            {
                var isPressed = Input.GetKeyDown(_settings.InfiniteFlight);
                _infiniteFlightPressCounter += isPressed ? 1 : 0;
            }
            
            if(_infiniteFlightPressCounter >= 3)
            {
                _isInfiniteFlightSelected = true;
            }
        }
        
        private void HandleInfiniteFlightPressReset()
        {
            if (Time.time - _lastTimeInfiniteFlightPressed > 0.5f)
            {
                _infiniteFlightPressCounter = 0;
            }
            
            _lastTimeInfiniteFlightPressed = Time.time;
        }
        
        public void PostFixedTick()
        {
            ConsumeAccelerationPress();
            ConsumeInfiniteFlightPress();
            ConsumeInflationPress();
            ConsumeJumpPress();
            ConsumeLeftPress();
            ConsumeRightPress();
        }

        private bool ConsumePress(ref bool isPressed)
        {
            if (!isPressed)
            {
                return false;
            }
            
            isPressed = false;
            return true;
        }
        public bool ConsumeLeftPress() => ConsumePress(ref _isLeftPressed);

        public bool ConsumeRightPress() => ConsumePress(ref _isRightPressed);

        public bool ConsumeAccelerationPress() => ConsumePress(ref _isAccelerationPressed);

        public bool ConsumeInflationPress() => ConsumePress(ref _isInflationPressed);

        public bool ConsumeInfiniteFlightPress() => _isInfiniteFlightSelected;

        public bool ConsumeJumpPress() => ConsumePress(ref _isJumpPressed);
    }
}