using Cysharp.Threading.Tasks;
using Infrastructure;
using Projects;

namespace Services
{
    class Navigation : INavigation
    {
        private readonly ProjectStateMachine _stateMachine;
        
        public Navigation(ProjectStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public UniTask ToBootstrap() => _stateMachine.Enter<BootstrapState>();
        
        public UniTask ToGame() => _stateMachine.Enter<GameState>();

        public UniTask ToGameHub() => _stateMachine.Enter<HubState>();
        
        public UniTask ExitGame() => _stateMachine.Enter<TeardownState>();
    }

 
}