namespace Services
{
    public class DefaultSettingsService: IImmutableSettingsService
    {
        public Locale CurrentLocale => Locale.English;
        
        public float TextSizeFactor => 1;
        
        public float MusicVolume => 1.0f;
            
        public float EffectVolume => 1.0f;
    }
}