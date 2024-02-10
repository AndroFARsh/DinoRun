using Cysharp.Threading.Tasks;
using Infrastructure;

namespace Services
{
    public interface ICurtain : IAsyncInitializer
    {
        UniTask Show();

        UniTask Hide();
    }
}