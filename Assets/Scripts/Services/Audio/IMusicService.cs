using Infrastructure;

namespace Services
{
    public interface IMusicService : IAsyncInitializer
    {
        void PlayMusic(Music music);
        
        void StopMusic();
    }
}