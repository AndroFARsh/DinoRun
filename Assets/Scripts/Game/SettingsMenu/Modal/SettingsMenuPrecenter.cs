using System;
using Services;
using SettingsMenu;

namespace Game
{
    class SettingsMenuPresenter : ISettingsMenuPresenter
    {
        private readonly ILoggerService _logger;
        private readonly IEffectService _effectService;
        private readonly IWindowService _windowService;
        private readonly ISettingsService _settingsService;

        public event Action<float> OnTextSizeFactorChanged
        {
            add
            {
                value(_settingsService.TextSizeFactor);
                _settingsService.OnTextSizeFactorChanged += value;   
            }
            remove => _settingsService.OnTextSizeFactorChanged -= value;
        }

        // Start is called before the first frame update
        public SettingsMenuPresenter(IWindowService windowService, ISettingsService settingsService, ILoggerService logger, IEffectService effectService)
        {
            _windowService = windowService;
            _settingsService = settingsService;
            _logger = logger;
            _effectService = effectService;
        }

        public void Back()
        {
            _effectService.PlayEffect(Effect.Click);
            _windowService.Pop();
        } 

        public void Reset()
        {
            _effectService.PlayEffect(Effect.Click);
            _settingsService.FactoryReset();
            _logger.Info("Reset");
        }
    }
}