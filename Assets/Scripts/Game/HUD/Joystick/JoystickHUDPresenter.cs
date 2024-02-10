using System;
using Services;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;

namespace Game
{
    public class JoystickHUDPresenter : IDisposable
    {
        private readonly InputActions _inputActions;
        
        private const float size = 100; 
        private const float sensitivity = 50; 
        
        private Vector3 _startPos;
        private Vector3 _input;
        private bool _detectJoystickMovement;
        
        private NativeArray<byte> _buffer;
        private InputEventPtr _eventPtr;
        private InputDevice _device;

        public float Size => size;

        public event Action<Vector3> OnShowJoystick;
        public event Action<Vector3> OnUpdateJoystick;
        public event Action OnHideJoystick;

        JoystickHUDPresenter(InputActions inputActions)
        {
            _inputActions = inputActions;
            
            _device = InputSystem.AddDevice("Keyboard");
            InputSystem.AddDeviceUsage(_device, "OnScreen");
            _buffer = StateEvent.From(_device, out _eventPtr, Allocator.Persistent);
        }
        
        public void Dispose()
        {
            if (_buffer.IsCreated)
                _buffer.Dispose();
            if (_device != null)
                InputSystem.RemoveDevice(_device);

            _device = null;
        }

        public void ShowJoystick(PointerDownEvent ev)
        {
            _detectJoystickMovement = true;
            _startPos = ev.position;
            
            OnShowJoystick?.Invoke(ev.localPosition);
            
            foreach (var c in _inputActions.Game.Jump.controls)
            {
                if (c is not InputControl<float>)
                {
                    c.WriteValueIntoEvent(1f, _eventPtr);
                    InputSystem.QueueEvent(_eventPtr);        
                }
            }
        }

        public void UpdateJoystick(PointerMoveEvent ev)
        {
            if (_detectJoystickMovement)
            {
                var deltaX = ev.position.x - _startPos.x;
                var deltaY = _startPos.y - ev.position.y;
                _input = new Vector3(deltaX, deltaY, 0);
                _input = _input.normalized;

                ApplySensitivity(ref _input, deltaX, deltaY);

                OnUpdateJoystick?.Invoke(_input);
            }
        }

        public void HideJoystick(IPointerEvent ev)
        {
            _input = Vector3.zero;
            _detectJoystickMovement = false;

            OnHideJoystick?.Invoke();
            
            foreach (var c in _inputActions.Game.Jump.controls)
            {
                if (c is not InputControl<float>)
                {
                    c.WriteValueIntoEvent(0f, _eventPtr);
                    InputSystem.QueueEvent(_eventPtr);        
                }
            }
        }

        private static void ApplySensitivity(ref Vector3 input, float deltaX, float deltaY)
        {
            if (Mathf.Abs(deltaX) >= sensitivity || Mathf.Abs(deltaY) >= sensitivity) return;

            input.x = (deltaX > 0)
                ? (deltaX >= sensitivity) ? input.x : Mathf.Lerp(0f, 1f, deltaX / sensitivity)
                : (deltaX <= -sensitivity) ? input.x : Mathf.Lerp(0f, -1f, deltaX / -sensitivity);

            input.y = (deltaY > 0)
                ? (deltaY >= sensitivity) ? input.y : Mathf.Lerp(0f, 1f, deltaY / sensitivity)
                : (deltaY <= -sensitivity) ? input.y : Mathf.Lerp(0f, -1f, deltaY / -sensitivity);
        }
    }
}