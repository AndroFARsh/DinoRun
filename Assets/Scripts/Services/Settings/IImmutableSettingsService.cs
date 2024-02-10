namespace Services
{
    public interface IImmutableSettingsService
    {
        float TextSizeFactor { get; }

        Locale CurrentLocale { get; }
        float MusicVolume { get; }
        float EffectVolume { get; }
    }
}