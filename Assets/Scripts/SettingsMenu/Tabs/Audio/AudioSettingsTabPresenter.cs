using System;
using Services;

namespace SettingsMenu
{
    public class AudioSettingsTabPresenter
    {
        private readonly ISettingsService _settingsService;
        
        public event Action<float> OnEffectVolumeChanged
        {
            add
            {
                value(_settingsService.EffectVolume);
                _settingsService.OnEffectVolumeChanged += value;  
            } 
            remove => _settingsService.OnEffectVolumeChanged -= value;
        }
        
        public event Action<float> OnMusicVolumeChanged
        {
            add
            {
                value(_settingsService.MusicVolume);
                _settingsService.OnMusicVolumeChanged += value;  
            } 
            remove => _settingsService.OnMusicVolumeChanged -= value;
        }
        
        AudioSettingsTabPresenter(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }
        
        public void RequestMusicValueChange(float oldValue, float newValue) => _settingsService.MusicVolume = newValue;
        
        public void RequestFXValueChange(float oldValue, float newValue) => _settingsService.EffectVolume = newValue;
    }
}