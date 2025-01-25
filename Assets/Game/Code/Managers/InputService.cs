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
        public bool IsDeflationPressed => _isDeflationPressed;
        public bool IsJumpPressed => _isJumpPressed;
        
        private bool _isLeftPressed;
        private bool _isRightPressed;
        private bool _isJumpPressed;
        private bool _isAccelerationPressed;
        private bool _isInflationPressed;
        private bool _isDeflationPressed;
        
        private readonly IControlsSettings _settings;

        public InputService(IControlsSettings settings)
        {
            _settings = settings;
        }
        
        public void Tick()
        {
            if (!_isLeftPressed) _isLeftPressed = Input.GetKey(_settings.Left);
            if (!_isRightPressed) _isRightPressed = Input.GetKey(_settings.Right);
            if (!_isJumpPressed) _isJumpPressed = Input.GetKey(_settings.Jump);
            if (!_isAccelerationPressed) _isAccelerationPressed = Input.GetKey(_settings.Acceleration);
            if (!_isInflationPressed) _isInflationPressed = Input.GetKey(_settings.Inflation);
            if (!_isDeflationPressed) _isDeflationPressed = Input.GetKey(_settings.Deflation);
        }
        
        public void PostFixedTick()
        {
            ConsumeAccelerationPress();
            ConsumeDeflationPress();
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

        public bool ConsumeDeflationPress() => ConsumePress(ref _isDeflationPressed);

        public bool ConsumeJumpPress() => ConsumePress(ref _isJumpPressed);
    }
}