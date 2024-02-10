using Cysharp.Threading.Tasks;
using Infrastructure;
using Services;

namespace Game
{
    public class GameLoopState : ISimpleState
    {
        private readonly ISystem _system;
        private readonly IMusicService _musicService;
        private readonly ITickableService _tickableService;
        
        GameLoopState(ISystem system, IMusicService musicService, ITickableService tickableService)
        {
            _system = system;
            _musicService = musicService;
            _tickableService = tickableService;
        }

        public UniTask Enter()
        {
            _musicService.PlayMusic(Music.GameBackgroundMusic);
            _tickableService.Register(_system.Run);
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            _musicService.StopMusic();
            _tickableService.Unregister(_system.Run);
            return UniTask.CompletedTask;
        }
    }
}