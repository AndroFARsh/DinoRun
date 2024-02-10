using System;
using UnityEngine;

namespace Services
{
    public class SettingsService: ISettingsService
    {
        private readonly IImmutableSettingsService _defaultSettingsService;
        private const string LocaleKey = "locale";
        private const string TextSizeKey = "text_size";
        private const string MusicVolumeKey = "music_volume";
        private const string EffectVolumeKey = "effect_volume";
        
        public event Action<float> OnTextSizeFactorChanged;
        public event Action<Locale> OnLocaleChanged;
        public event Action<float> OnMusicVolumeChanged;
        public event Action<float> OnEffectVolumeChanged;

        SettingsService(DefaultSettingsService defaultSettingsService)
        {
            _defaultSettingsService = defaultSettingsService;
        }

        public float TextSizeFactor
        {
            get => PlayerPrefs.GetFloat(TextSizeKey, _defaultSettingsService.TextSizeFactor);
            set
            {
                if (Math.Abs(value - TextSizeFactor) < 0.0001f) return;
                
                PlayerPrefs.SetFloat(TextSizeKey, value);
                OnTextSizeFactorChanged?.Invoke(value);
            } 
        }

        public Locale CurrentLocale
        {
            get => GetEnum(LocaleKey, _defaultSettingsService.CurrentLocale);
            set
            {
                if (value == CurrentLocale) return;
                
                SetEnum(LocaleKey, value);
                OnLocaleChanged?.Invoke(value);
            }
        }
        public float MusicVolume 
        {
            get => PlayerPrefs.GetFloat(MusicVolumeKey, _defaultSettingsService.MusicVolume);
            set
            {
                if (Math.Abs(value - MusicVolume) < 0.0001f) return;
                
                PlayerPrefs.SetFloat(MusicVolumeKey, value);
                OnMusicVolumeChanged?.Invoke(value);
            } 
        }

        public float EffectVolume 
        {
            get => PlayerPrefs.GetFloat(EffectVolumeKey, _defaultSettingsService.EffectVolume);
            set
            {
                if (Math.Abs(value - EffectVolume) < 0.0001f) return;
                
                PlayerPrefs.SetFloat(EffectVolumeKey, value);
                OnEffectVolumeChanged?.Invoke(value);
            }
        }

        public void FactoryReset()
        {
            TextSizeFactor = _defaultSettingsService.TextSizeFactor;
            CurrentLocale = _defaultSettingsService.CurrentLocale;
            EffectVolume = _defaultSettingsService.EffectVolume;
            MusicVolume = _defaultSettingsService.MusicVolume;
        }

        private static void SetEnum<T>(string key, T value) where T : struct =>
            PlayerPrefs.SetString(key, value.ToString());
       
        private static T GetEnum<T>(string key, T defaultValue) where T : struct => 
            Enum.TryParse<T>(PlayerPrefs.GetString(key, defaultValue.ToString()), out var result) ? result : defaultValue;
    }
}