using Services;
using UnityEngine.UIElements;

namespace Game
{
    public class SettingsHUD : HUD
    {
        private readonly SettingsHUDPresenter _presenter;
        private Button _button;

        public override HUDAlign Align => HUDAlign.TopRight;
        
        public SettingsHUD(SettingsHUDPresenter presenter) : base(AssetKeys.SettingsHUDUI)
        {
            _presenter = presenter;
        }

        protected override VisualElement OnInitialize(VisualElement visualElement)
        {
            _button = visualElement.Q<Button>("settings_button");
            return visualElement;
        }
        
        protected override void OnAttached()
        {
            _presenter.Attache();
            _button.clickable.clicked += OnSettingsButtonClick;
        }

        protected override void OnDetached()
        {
            _presenter.Detach();
            _button.clickable.clicked -= OnSettingsButtonClick;
        }

        private void OnSettingsButtonClick() => _presenter.ShowPause();
    }
}