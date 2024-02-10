using Infrastructure;

namespace Services
{
    public enum Channel
    {
        Primary,
        Secondary
        
    }
    public interface IEffectService : IAsyncInitializer
    {
        void PlayEffect(Effect effect, Channel channel = Channel.Primary);
        
        void StopEffect(Channel channel = Channel.Primary);
    }
}