using Cysharp.Threading.Tasks;
using Infrastructure;
using Services;

namespace Game
{
    public class GameBuildState : ISimpleState
    {
        private readonly ICurtain _curtain;
        private readonly IStateMachine _stateMachine;
        private readonly InputService _inputService;
        private readonly ISystem _system;
        
        GameBuildState(
            ICurtain curtain,
            IStateMachine stateMachine,
            InputService inputService,
            ISystem system
        )
        {
            _curtain = curtain;
            _stateMachine = stateMachine;
            _inputService = inputService;
            _system = system;
        }

        public async UniTask Enter()
        {
            _system.Init();
            await _stateMachine.Enter<GameLoopState>();
        }

        public async UniTask Exit()
        {
            await _curtain.Hide();
            _inputService.Enable();
        }
    }
}