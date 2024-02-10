using Services;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UIElements;

namespace Game
{
    // /// <summary>
    // /// A button that is visually represented on-screen and triggered by touch or other pointer
    // /// input.
    // /// </summary>
    // [AddComponentMenu("Input/On-Screen Button")]
    // [HelpURL(InputSystem.kDocUrl + "/manual/OnScreen.html#on-screen-buttons")]
    // public class OnScreenButton : OnScreenControl, IPointerDownHandler, IPointerUpHandler
    // {
    //     public void OnPointerUp(PointerEventData eventData)
    //     {
    //         SendValueToControl(0.0f);
    //     }
    //
    //     public void OnPointerDown(PointerEventData eventData)
    //     {
    //         SendValueToControl(1.0f);
    //     }
    //
    //     ////TODO: pressure support
    //     /*
    //     /// <summary>
    //     /// If true, the button's value is driven from the pressure value of touch or pen input.
    //     /// </summary>
    //     /// <remarks>
    //     /// This essentially allows having trigger-like buttons as on-screen controls.
    //     /// </remarks>
    //     [SerializeField] private bool m_UsePressure;
    //     */
    //
    //     [InputControl(layout = "Button")]
    //     [SerializeField]
    //     private string m_ControlPath;
    //
    //     protected override string controlPathInternal
    //     {
    //         get => m_ControlPath;
    //         set => m_ControlPath = value;
    //     }
    // }
    
    public class JumpHUD : HUD
    {
        private readonly JumpHUDPresenter _presenter;
        private VisualElement _button;

        public override HUDAlign Align => HUDAlign.BottomRight;
        
        public JumpHUD(JumpHUDPresenter presenter) 
            : base(AssetKeys.JumpHUDUI)
        {
            _presenter = presenter;
        }

        protected override VisualElement OnInitialize(VisualElement visualElement)
        {
            _button = visualElement.Q<VisualElement>("jump_button");
            return visualElement;
        }
        
        protected override void OnAttached()
        {
            _button.RegisterCallback<PointerDownEvent>(PerformJumpStart);
            _button.RegisterCallback<PointerUpEvent>(PerformJumpEnd);
            _button.RegisterCallback<PointerLeaveEvent>(PerformJumpEnd);
        }

        protected override void OnDetached()
        {
            _button.UnregisterCallback<PointerDownEvent>(PerformJumpStart);
            _button.UnregisterCallback<PointerUpEvent>(PerformJumpEnd);
            _button.UnregisterCallback<PointerLeaveEvent>(PerformJumpEnd);
        }

        private void PerformJumpStart(EventBase evt) => _presenter.PerformJumpStart();
        
        private void PerformJumpEnd(EventBase evt) => _presenter.PerformJumpEnd();
    }
}