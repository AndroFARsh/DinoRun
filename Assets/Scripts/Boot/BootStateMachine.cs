using Cysharp.Threading.Tasks;
using Infrastructure;
using VContainer.Unity;

namespace Boot
{
    public class BootStateMachine : StateMachine, IInitializable
    {
        public BootStateMachine(ProjectStateMachine project, IFactory factory) : base(project, factory)
        {
        }
        
        public void Initialize()
        {
            RegisterState<BootEntryState>();
            
            Enter<BootEntryState>().Forget();
        }
    }
}