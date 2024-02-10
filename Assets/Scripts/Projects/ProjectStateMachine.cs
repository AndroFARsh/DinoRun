using Cysharp.Threading.Tasks;
using Projects;
using VContainer.Unity;

namespace Infrastructure
{
    public class ProjectStateMachine : StateMachine, IInitializable
    {
        public ProjectStateMachine(IFactory factory) : base(null, factory)
        {
        }

        public void Initialize()
        {
            RegisterState<BootstrapState>();
            RegisterState<LoaderState>();
            RegisterState<HubState>();
            RegisterState<GameState>();
            RegisterState<TeardownState>();

            Enter<BootstrapState>().Forget();
        }
    }
}