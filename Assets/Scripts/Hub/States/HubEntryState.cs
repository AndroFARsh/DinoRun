using Cysharp.Threading.Tasks;
using Infrastructure;
using Services;

namespace Hub
{
    public class HubEntryState : ISimpleState
    {
        private readonly ICurtain _curtain;
        private readonly IStateMachine _stateMachine;
        private readonly IWindowService _windowService;

        HubEntryState(
            ICurtain curtain,
            IStateMachine stateMachine,
            IWindowService windowService
        )
        {
            _curtain = curtain;
            _stateMachine = stateMachine;
            _windowService = windowService;
        }

        public async UniTask Enter()
        {
            await _curtain.Show();
            await _windowService.Initialize();
            await _stateMachine.Enter<MainMenuState>();
        }
    }
}