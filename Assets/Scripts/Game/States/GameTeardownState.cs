using Cysharp.Threading.Tasks;
using Infrastructure;
using Projects;
using Services;

namespace Game
{
    public class GameTeardownState : ISimpleState
    {
        private readonly ICurtain _curtain;
        private readonly IStateMachine _stateMachine;
        private readonly IHUDService _hudService;
        private readonly ISystem _system;
        private readonly InputService _inputService;

        GameTeardownState(
            ICurtain curtain,
            IStateMachine stateMachine,
            ISystem system,
            InputService inputService
        )
        {
            _curtain = curtain;
            _stateMachine = stateMachine;
            _system = system;
            _inputService = inputService;
        }

        public async UniTask Enter()
        {
            await _curtain.Show();
            _inputService.Disable();
            _system.Destroy();
            await _stateMachine.Enter<HubState>();
        }
    }
}