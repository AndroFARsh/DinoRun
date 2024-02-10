using Cysharp.Threading.Tasks;
using Infrastructure;
using Services;
using Services.Ads;

namespace Game
{
    public class GameOverState : ISimpleState
    {
        private readonly IAdvertisementService _adsService;
        private readonly IWindowService _windowService;

        GameOverState(IAdvertisementService adsService, IWindowService windowService)
        {
            _adsService = adsService;
            _windowService = windowService;
        }

        public async UniTask Enter()
        {
            await _adsService.ShowAds();
            await _windowService.Push<GameOverModal>();
        }
        
        public UniTask Exit()
        {
            _windowService.Pop();
            return UniTask.CompletedTask;
        }
    }
}