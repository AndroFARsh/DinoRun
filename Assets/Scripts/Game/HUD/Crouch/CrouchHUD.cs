using Services;
using UnityEngine.UIElements;

namespace Game
{
    public class CrouchHUD : HUD
    {
        private readonly CrouchHUDPresenter _presenter;
        private VisualElement _button;

        public override HUDAlign Align => HUDAlign.BottomLeft;
        
        public CrouchHUD(CrouchHUDPresenter presenter) 
            : base(AssetKeys.CrouchHUDUI)
        {
            _presenter = presenter;
        }

        protected override VisualElement OnInitialize(VisualElement visualElement)
        {
            _button = visualElement.Q<VisualElement>("crouch_button");
            return visualElement;
        }
        
        protected override void OnAttached()
        {
            _button.RegisterCallback<PointerDownEvent>(PerformCrouchStart);
            _button.RegisterCallback<PointerUpEvent>(PerformCrouchEnd);
            _button.RegisterCallback<PointerLeaveEvent>(PerformCrouchEnd);
        }

        protected override void OnDetached()
        {
            _button.UnregisterCallback<PointerDownEvent>(PerformCrouchStart);
            _button.UnregisterCallback<PointerUpEvent>(PerformCrouchEnd);
            _button.UnregisterCallback<PointerLeaveEvent>(PerformCrouchEnd);
        }

        private void PerformCrouchStart(EventBase evt) => _presenter.PerformCrouchStart();
        
        private void PerformCrouchEnd(EventBase evt) => _presenter.PerformCrouchEnd();
    }
}