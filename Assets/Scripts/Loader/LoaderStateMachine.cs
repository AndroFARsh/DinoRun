using Cysharp.Threading.Tasks;
using Infrastructure;
using VContainer.Unity;

namespace Loader
{
    public class LoaderStateMachine : StateMachine, IInitializable
    {
        public LoaderStateMachine(ProjectStateMachine project, IFactory factory) : base(project, factory)
        {
        }

        public void Initialize()
        {
            RegisterState<LoaderEntryState>();
            
            Enter<LoaderEntryState>().Forget();
        }
    }
}