using Cysharp.Threading.Tasks;

namespace Infrastructure
{
    public interface IStateMachine
    {
        bool HasState { get; }

        UniTask Enter<TState>() where TState : class, ISimpleState;
        
        UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
     
        bool IsStateSupported<TState>() where TState : IState;
    }
}