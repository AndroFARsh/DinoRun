using Infrastructure;
using Services;
using UnityEngine.InputSystem;

namespace Game
{
    public class SettingsHUDPresenter
    {
        private readonly IStateMachine _stateMachine;
        private readonly IEffectService _effectService;
        private readonly InputService _inputService;

        public SettingsHUDPresenter(
            IStateMachine stateMachine, 
            IEffectService effectService,
            InputService inputService
        ) 
        {
            _stateMachine = stateMachine;
            _effectService = effectService;
            _inputService = inputService;
        }

        public void ShowPause()
        {
            _effectService.PlayEffect(Effect.Click);
            _stateMachine.Enter<GamePauseState>();
        }

        public void Attache() => _inputService.Menu += ShowPause;

        public void Detach() => _inputService.Menu -= ShowPause;

        private void ShowPause(InputAction.CallbackContext context) => ShowPause();
    }
}