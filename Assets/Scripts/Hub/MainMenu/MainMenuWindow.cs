using Infrastructure;
using Services;
using UnityEngine.UIElements;

namespace MainMenu
{
    public class MainMenuWindow : Window
    {
        private readonly MainMenuPresenter _presenter;

        private Button _play;
        private Button _settings;
        private Button _exit;
        private Label _version;

        public MainMenuWindow(MainMenuPresenter presenter)
            : base(AssetKeys.MainMenuUI)
        {
            _presenter = presenter;
        }

        protected override VisualElement OnInitialize(VisualElement visualElement)
        {
            _play = visualElement.Q<Button>("play_button");
            _settings = visualElement.Q<Button>("settings_button");
            _exit = visualElement.Q<Button>("exit_button");
            
            _version = visualElement.Q<Label>("version");
            
            return visualElement; 
        }

        protected override void OnAttached()
        {
            _play.Focus();
            
            _play.clickable.clicked += _presenter.PlayButtonClicked;
            _settings.clickable.clicked += _presenter.SettingsButtonClicked;
            _exit.clickable.clicked += _presenter.ExitButtonClicked;

            _version.text = ProjectData.Version;
        }

        protected override void OnDetached()
        {
            _play.clickable.clicked -= _presenter.PlayButtonClicked;
            _settings.clickable.clicked -= _presenter.SettingsButtonClicked;
            _exit.clickable.clicked -= _presenter.ExitButtonClicked;
        }
    }
}