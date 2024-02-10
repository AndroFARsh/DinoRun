using Cysharp.Threading.Tasks;
using Infrastructure;
using VContainer.Unity;

namespace Game
{
    public class GameStateMachine : StateMachine, IInitializable
    {
        public GameStateMachine(ProjectStateMachine project, IFactory factory) : base(project, factory)
        {
        }

        public void Initialize()
        {
            RegisterState<GameEntryState>();
            RegisterState<GameBuildState>();
            RegisterState<GameLoopState>();
            RegisterState<GamePauseState>();
            RegisterState<GameOverState>();
            RegisterState<GameRestartState>();
            RegisterState<GameTeardownState>();
            
            Enter<GameEntryState>().Forget();
        }
    }
}