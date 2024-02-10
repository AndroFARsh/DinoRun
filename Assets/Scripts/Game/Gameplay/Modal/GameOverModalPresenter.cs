using Cysharp.Threading.Tasks;
using Infrastructure;
using Projects;

namespace Game
{
    public class GameOverPresenter
    {
        private readonly IStateMachine _stateMachine;

        public GameOverPresenter(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Restart() => _stateMachine.Enter<GameRestartState>().Forget();

        public void GameHub() => _stateMachine.Enter<HubState>().Forget();
    }
}