using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Services
{
    public class InputService
    {
        private readonly InputActions _inputActions;
        
        private float _crouchTouchTimestamp = -1;
        
        public bool StartGame => _inputActions.Game.StartGame.IsPressed();

        public bool IsJump => _inputActions.Game.Jump.IsPressed() &&  _inputActions.Game.Jump.WasPressedThisFrame();

        public bool IsJumpInProgress => _inputActions.Game.Jump.IsInProgress();
        
        public bool IsCrouch => _crouchTouchTimestamp > 0 
            ? Time.time - _crouchTouchTimestamp >= 0 
            : _inputActions.Game.Crouch.IsInProgress();
        
        public event Action<InputAction.CallbackContext> Menu
        {
            add => _inputActions.Game.Menu.performed += value;
            remove => _inputActions.Game.Menu.performed -= value;
        }
        
        public event Action<InputAction.CallbackContext> Accept
        {
            add => _inputActions.Hub.Accept.performed += value;
            remove => _inputActions.Hub.Accept.performed -= value;
        }
        
        public event Action<InputAction.CallbackContext> Decline
        {
            add => _inputActions.Hub.Decline.performed += value;
            remove => _inputActions.Hub.Decline.performed -= value;
        }


        public InputService(InputActions inputActions)
        {
            _inputActions = inputActions;
        }

        public void CrouchTouchStart()
        {
            if (_crouchTouchTimestamp < 0)
            {
                _crouchTouchTimestamp = Time.time;
            }
        } 
        
        public async void CrouchTouchEnd()
        {
            await UniTask.NextFrame();
            _crouchTouchTimestamp = -1f;
        }

        public void Enable() => _inputActions.Enable();
        
        public void Disable() => _inputActions.Disable();
    }
}