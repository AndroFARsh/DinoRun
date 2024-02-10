using Cysharp.Threading.Tasks;

namespace Infrastructure
{
    public interface IAsyncInitializer
    {
        UniTask Initialize();
    }
}