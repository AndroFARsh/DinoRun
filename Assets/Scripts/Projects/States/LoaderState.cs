using Cysharp.Threading.Tasks;
using Infrastructure;
using Services;

namespace Projects
{
    public class LoaderState : ISimpleState
    {
        private readonly ICurtain _curtain;
        private readonly IAssetProvider _assetProvider;
        private readonly ISceneManager _sceneManager;

        public LoaderState(
            ICurtain curtain,
            IAssetProvider assetProvider,
            ISceneManager sceneManager
        )
        {
            _curtain = curtain;
            _assetProvider = assetProvider;
            _sceneManager = sceneManager;
        }

        public async UniTask Enter()
        {
            await _curtain.Show();
            await _assetProvider.WarmupAssetsByLabel(AssetLabels.Loader);
            await _sceneManager.LoadScene(Scene.LoaderScene);
        }
        
        public async UniTask Exit() => await _assetProvider.WarmupAssetsByLabel(AssetLabels.Loader);
    }
}