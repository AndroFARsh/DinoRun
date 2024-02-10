using Cysharp.Threading.Tasks;
using Infrastructure;

namespace Services
{
    public interface IWindowService : IAsyncInitializer
    {
        UniTask Push<T>() where T : View;

        UniTask Replace<T>() where T : View;
        
        void Pop();
    }
}