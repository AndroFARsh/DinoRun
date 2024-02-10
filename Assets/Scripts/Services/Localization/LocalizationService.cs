using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Localization.Settings;

namespace Services
{
    public class LocalizationService : ILocalizationService, IDisposable
    {
        private readonly ISettingsService _settingsService;
        private bool _inProgress;
        
        public LocalizationService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }
        
        public UniTask Initialize()
        {
            UpdateLocale(_settingsService.CurrentLocale);
            _settingsService.OnLocaleChanged += UpdateLocale;
            
            return UniTask.CompletedTask;
        }

        public void Dispose()
        {
            _settingsService.OnLocaleChanged -= UpdateLocale;
        }

        private async void UpdateLocale(Locale locale)
        {
            if (_inProgress) return;
            
            _inProgress = true;
            
            await LocalizationSettings.InitializationOperation.Task;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)locale];
            
            _inProgress = false;
        }
    }
}