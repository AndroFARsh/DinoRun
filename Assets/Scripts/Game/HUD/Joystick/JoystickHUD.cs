using Services;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game
{
    public class JoystickHUD : HUD
    {
        private readonly JoystickHUDPresenter _presenter;
        
        private VisualElement _joystickTouchArea;
        private VisualElement _joystickElement; 
        private VisualElement _joystickKnob; 

        public override HUDAlign Align => HUDAlign.BottomLeft;

        JoystickHUD(JoystickHUDPresenter presenter) : base(AssetKeys.JoystickHUDUI)
        {
            _presenter = presenter;
        }
        
        protected override VisualElement OnInitialize(VisualElement visualElement)
        {
            _joystickTouchArea = visualElement.Q("joystick_touch_area");
            _joystickElement = visualElement.Q("joystick_outer"); 
            _joystickKnob = visualElement.Q("joystick_knob"); 
            
            _joystickElement.style.width = _presenter.Size; // applying width of joystick
            _joystickElement.style.height = _presenter.Size; // applying height of joystick
            
            _joystickKnob.style.transformOrigin = new TransformOrigin(Length.Percent(100), 0, 0);
            
            return visualElement;
        }
        
        protected override void OnAttached()
        {
            _joystickTouchArea.RegisterCallback<PointerDownEvent>(_presenter.ShowJoystick);
            _joystickTouchArea.RegisterCallback<PointerMoveEvent>(_presenter.UpdateJoystick);
            _joystickTouchArea.RegisterCallback<PointerUpEvent>(_presenter.HideJoystick);
            _joystickTouchArea.RegisterCallback<PointerLeaveEvent>(_presenter.HideJoystick);

            _presenter.OnHideJoystick += OnHideJoystick;
            _presenter.OnShowJoystick += OnShowJoystick;
            _presenter.OnUpdateJoystick += OnUpdateJoystick;
        }

        protected override void OnDetached()
        {
            _joystickTouchArea.UnregisterCallback<PointerDownEvent>(_presenter.ShowJoystick);
            _joystickTouchArea.UnregisterCallback<PointerMoveEvent>(_presenter.UpdateJoystick);
            _joystickTouchArea.UnregisterCallback<PointerUpEvent>(_presenter.HideJoystick);
            _joystickTouchArea.UnregisterCallback<PointerLeaveEvent>(_presenter.HideJoystick);
            
            _presenter.OnHideJoystick -= OnHideJoystick;
            _presenter.OnShowJoystick -= OnShowJoystick;
            _presenter.OnUpdateJoystick -= OnUpdateJoystick;
        }

        private void OnShowJoystick(Vector3 position)
        {
            _joystickElement.style.left = position.x - _presenter.Size / 2;
            _joystickElement.style.top = position.y - _presenter.Size / 2;
            _joystickElement.style.display = DisplayStyle.Flex;
        }

        private void OnUpdateJoystick(Vector3 input)
        {
            _joystickKnob.style.translate = new StyleTranslate(new Translate(
                new Length(input.x * _presenter.Size / 2, LengthUnit.Pixel), 
                new Length(-input.y * _presenter.Size / 2, LengthUnit.Pixel))
            );
        }

        private void OnHideJoystick()
        {
            _joystickElement.style.display = DisplayStyle.None;
            _joystickKnob.style.translate = new StyleTranslate(new Translate(new Length(0, LengthUnit.Pixel), new Length(0, LengthUnit.Pixel)));
        }
    }
}