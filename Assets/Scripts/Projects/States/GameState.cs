using Cysharp.Threading.Tasks;
using Infrastructure;
using Services;

namespace Projects
{
    public class GameState : ISimpleState
    {
        private readonly ICurtain _curtain;
        private readonly IAssetProvider _assetProvider;
        private readonly ISceneManager _sceneManager;

        public GameState(
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
            await _assetProvider.WarmupAssetsByLabel(AssetLabels.Game);
            await _sceneManager.LoadScene(Scene.GameScene);
        } 
        
        public async UniTask Exit() => await _assetProvider.WarmupAssetsByLabel(AssetLabels.Game);
    }
}