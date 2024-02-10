using Cysharp.Threading.Tasks;
using Infrastructure;
using Services;

namespace Game
{
    public class GamePauseState : ISimpleState
    {
        private readonly IWindowService _windowService;

        GamePauseState(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public UniTask Enter()
        {
            _windowService.Push<PauseModal>();
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            _windowService.Pop();
            return UniTask.CompletedTask;
        }
    }
}