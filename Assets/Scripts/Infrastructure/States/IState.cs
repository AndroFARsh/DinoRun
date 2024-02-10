using Cysharp.Threading.Tasks;

namespace Infrastructure
{
    public interface ISimpleState : IState
    {
        UniTask Enter();
    }
    
    public interface IPayloadState<TPayload> : IState
    {
        UniTask Enter(TPayload payload);
    }
    
    public interface IState
    {
        UniTask Exit() => UniTask.CompletedTask;
    }
}