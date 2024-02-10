using Cysharp.Threading.Tasks;
using Infrastructure;
using VContainer.Unity;

namespace Hub
{
    public class HubStateMachine : StateMachine, IInitializable
    {
        public HubStateMachine(ProjectStateMachine project, IFactory factory) : base(project, factory)
        {
        }

        public void Initialize()
        {
            RegisterState<HubEntryState>();
            RegisterState<MainMenuState>();
            RegisterState<SettingsMenuState>();
            
            Enter<HubEntryState>().Forget();
        }
    }
}