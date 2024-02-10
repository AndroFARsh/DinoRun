using Services;
using SettingsMenu;

namespace MainMenu
{
    public class MainMenuPresenter
    {
        private readonly INavigation _navigation;
        private readonly IEffectService _effectService;
        private readonly IWindowService _windowService;

        public MainMenuPresenter(INavigation navigation, IWindowService windowService, IEffectService effectService)
        {
            _navigation = navigation;
            _windowService = windowService;
            _effectService = effectService;
        }

        public void PlayButtonClicked()
        {
            _effectService.PlayEffect(Effect.Click);
            _navigation.ToGame();  
        }

        public void SettingsButtonClicked()
        {
            _effectService.PlayEffect(Effect.Click);
            _windowService.Push<SettingsMenuWindow>();
        }

        public void ExitButtonClicked()
        {
            _effectService.PlayEffect(Effect.Click);
            _navigation.ExitGame();
        }
    }
}