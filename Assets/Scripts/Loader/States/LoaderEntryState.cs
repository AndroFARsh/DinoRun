using Cysharp.Threading.Tasks;
using Infrastructure;
using Projects;
using Services;
using Services.Ads;

namespace Loader
{
    public class LoaderEntryState : ISimpleState
    {
        private readonly ICurtain _curtain;
        private readonly IWindowService _windowService;
        private readonly IAssetProvider _assetProvider;
        private readonly ILocalizationService _localizationService;
        private readonly IAdvertisementService _advertisementService;
        private readonly IUpdateBundleService _updateBundleService;
        private readonly IStateMachine _stateMachine;

        LoaderEntryState(
            ICurtain curtain,
            IWindowService windowService,
            IAssetProvider assetProvider,
            ILocalizationService localizationService,
            IAdvertisementService advertisementService,
            IUpdateBundleService updateBundleService,
            IStateMachine stateMachine
        )
        {
            _curtain = curtain;
            _windowService = windowService;
            _assetProvider = assetProvider;
            _localizationService = localizationService;
            _advertisementService = advertisementService;
            _updateBundleService = updateBundleService;
            _stateMachine = stateMachine;
        }

        public async UniTask Enter()
        {
            await _curtain.Show();
            await _windowService.Initialize();
            await _windowService.Push<BundleLoaderWindow>();
            await _curtain.Hide();
            
            await _assetProvider.Initialize();
            await _localizationService.Initialize();
            await _advertisementService.Initialize();
            await _updateBundleService.Initialize();
            
            _stateMachine.Enter<HubState>().Forget();
        }

        public async UniTask Exit()
        {
            await _curtain.Show();
        }
    }
}