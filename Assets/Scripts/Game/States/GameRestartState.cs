using Cysharp.Threading.Tasks;
using Infrastructure;
using Services;

namespace Game
{
    public class GameRestartState : ISimpleState
    {
        private readonly ICurtain _curtain;
        private readonly IStateMachine _stateMachine;
        private readonly ISystem _system;
        private readonly ScoreBoard _scoreBoard;
        private readonly InputService _inputService;
        private readonly IHUDService _hudService;

        GameRestartState(
            ICurtain curtain,
            IStateMachine stateMachine,
            ISystem system,
            ScoreBoard scoreBoard,
            InputService inputService
        )
        {
            _curtain = curtain;
            _stateMachine = stateMachine;
            _system = system;
            _scoreBoard = scoreBoard;
            _inputService = inputService;
        }

        public async UniTask Enter()
        {
            await _curtain.Show();
            _inputService.Disable();
            _system.Destroy();
            _scoreBoard.Reset();
            await _stateMachine.Enter<GameBuildState>();
        }
    }
}