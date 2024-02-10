using Cysharp.Threading.Tasks;

namespace Services
{
    public interface INavigation
    {
        UniTask ToBootstrap();
        
        UniTask ToGame();
        
        UniTask ToGameHub();
        
        UniTask ExitGame();
    }
}