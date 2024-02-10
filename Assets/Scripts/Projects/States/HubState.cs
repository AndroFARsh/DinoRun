using Cysharp.Threading.Tasks;
using Infrastructure;
using Services;

namespace Projects
{
    public class HubState : ISimpleState
    {
        private readonly ICurtain _curtain;
        private readonly IAssetProvider _assetProvider;
        private readonly ISceneManager _sceneManager;
        private readonly IMusicService _musicService;

        public HubState(
            ICurtain curtain,
            IAssetProvider assetProvider,
            ISceneManager sceneManager,
            IMusicService musicService
        )
        {
            _curtain = curtain;
            _assetProvider = assetProvider;
            _sceneManager = sceneManager;
            _musicService = musicService;
        }

        public async UniTask Enter()
        {
            await _curtain.Show();
            await _assetProvider.WarmupAssetsByLabel(AssetLabels.Hub);
            await _sceneManager.LoadScene(Scene.HubScene);
            
            _musicService.PlayMusic(Music.HubBackgroundMusic);
        }

        public async UniTask Exit()
        {
            _musicService.StopMusic();
            await _assetProvider.WarmupAssetsByLabel(AssetLabels.Hub);  
        } 
    }
}