using Cysharp.Threading.Tasks;
using Infrastructure;
using Services;

namespace Game
{
    public class GameEntryState : ISimpleState
    {
        private readonly ICurtain _curtain;
        private readonly IStateMachine _stateMachine;
        private readonly IWindowService _windowService;
        private readonly IHUDService _hudService;
        private readonly ObstaclePool _obstaclePool;
        private readonly CharacterPool _characterPool;
        private readonly AchieveService _achieveService;

        GameEntryState(
            ICurtain curtain,
            IStateMachine stateMachine,
            IWindowService windowService,
            IHUDService hudService,
            ObstaclePool obstaclePool,
            CharacterPool characterPool,
            AchieveService achieveService
        )
        {
            _curtain = curtain;
            _stateMachine = stateMachine;
            _windowService = windowService;
            _hudService = hudService;
            _obstaclePool = obstaclePool;
            _characterPool = characterPool;
            _achieveService = achieveService;
        }

        public async UniTask Enter()
        {
            await _curtain.Show();
            await _windowService.Initialize();
            await _hudService.Initialize();
            await _obstaclePool.Initialize();
            await _characterPool.Initialize();
            await _achieveService.Initialize();
            await _stateMachine.Enter<GameBuildState>();
        }
    }
}