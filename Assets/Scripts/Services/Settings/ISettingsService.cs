using System;

namespace Services
{
    public interface ISettingsService : IImmutableSettingsService
    {
        event Action<float> OnTextSizeFactorChanged;
        event Action<Locale> OnLocaleChanged;
        event Action<float> OnMusicVolumeChanged;
        event Action<float> OnEffectVolumeChanged;

        new float TextSizeFactor { get; set; }
        new Locale CurrentLocale { get; set; }
        new float MusicVolume { get; set; }
        new float EffectVolume { get; set; }

        void FactoryReset();
    }
}