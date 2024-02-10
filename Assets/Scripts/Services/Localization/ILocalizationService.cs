using Infrastructure;

namespace Services
{
    public enum Locale
    {
        English,
        Ukraine
    }
    
    public interface ILocalizationService : IAsyncInitializer
    {
    }
}