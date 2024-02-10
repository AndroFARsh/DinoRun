using Cysharp.Threading.Tasks;
using Infrastructure;
using Projects;
using Services;
using SettingsMenu;

namespace Game
{
    public class PauseModalPresenter
    {
        private readonly IWindowService _windowService;
        private readonly IStateMachine _stateMachine;

        public PauseModalPresenter(
            IWindowService windowService,
            IStateMachine stateMachine)
        {
            _windowService = windowService;
            _stateMachine = stateMachine;
        }

        public void Resume() => _stateMachine.Enter<GameLoopState>().Forget();
        
        public void Restart() => _stateMachine.Enter<GameRestartState>();
        
        public void Options() => _windowService.Push<SettingsMenuWindow>();
        
        public void MainMenu() => _stateMachine.Enter<GameTeardownState>().Forget();
    }
}