using System;
using Infrastructure;
using Services;
using SettingsMenu;

namespace Hub
{
    class SettingsMenuPresenter : ISettingsMenuPresenter
    {
        private readonly IStateMachine _stateMachine;
        private readonly ISettingsService _settingsService;
        private readonly ILoggerService _logger;
        private readonly IEffectService _effectService;
       
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
        public SettingsMenuPresenter(
            IStateMachine stateMachine, 
            ISettingsService settingsService,
            ILoggerService logger, 
            IEffectService effectService)
        {
            _stateMachine = stateMachine;
            _settingsService = settingsService;
            _logger = logger;
            _effectService = effectService;
        }

        public void Back()
        {
            _effectService.PlayEffect(Effect.Click);
            _stateMachine.Enter<SettingsMenuState>();
            _logger.Info("Back");
        } 

        public void Reset()
        {
            _effectService.PlayEffect(Effect.Click);
            _settingsService.FactoryReset();
            _logger.Info("Reset");
        }
    }
}