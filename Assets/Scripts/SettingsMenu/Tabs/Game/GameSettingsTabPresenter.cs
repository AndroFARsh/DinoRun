using System;
using Infrastructure;
using Services;

namespace SettingsMenu
{
    public class GameSettingsTabPresenter
    {
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
        
        public event Action<Locale> OnLocaleChanged
        {
            add
            {
                value(_settingsService.CurrentLocale);
                _settingsService.OnLocaleChanged += value;  
            } 
            remove => _settingsService.OnLocaleChanged -= value;
        }
        
        GameSettingsTabPresenter(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public void PrevLocale()
        {
            var newLocale = _settingsService.CurrentLocale.Prev();
            _settingsService.CurrentLocale = newLocale;  
        }

        public void NextLocale()
        {
            var newLocale = _settingsService.CurrentLocale.Next();
            _settingsService.CurrentLocale = newLocale;  
        }
        
        public void RequestTextSizeValueChange(float oldValue, float newValue) => _settingsService.TextSizeFactor = newValue;
    }
}