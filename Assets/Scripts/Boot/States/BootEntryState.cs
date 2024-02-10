using Cysharp.Threading.Tasks;
using Infrastructure;
using Projects;
using Services;

namespace Boot
{
    public class BootEntryState : ISimpleState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IEffectService _effectService;
        private readonly IMusicService _musicService;
        private readonly ICurtain _curtain;

        public BootEntryState(
            IStateMachine stateMachine, 
            IEffectService effectService,
            IMusicService musicService,
            ICurtain curtain
        ) {
            _stateMachine = stateMachine;
            _effectService = effectService;
            _musicService = musicService;
            _curtain = curtain;
        }

        public async UniTask Enter()
        {
            await _curtain.Initialize();
            await _effectService.Initialize();
            await _musicService.Initialize();
            
            _stateMachine.Enter<LoaderState>().Forget();
        }
    }
}