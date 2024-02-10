using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Services
{
    public class UpdateBundleService : IUpdateBundleService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ILoggerService _loggerService;

        public event Action<DownloadStatus> OnProgress;
        
        UpdateBundleService(IAssetProvider assetProvider, ILoggerService loggerService)
        {
            _assetProvider = assetProvider;
            _loggerService = loggerService;
        }

        public async UniTask Initialize()
        {
            var assets = await _assetProvider.ListOfAllAssets();
            var handle = Addressables.DownloadDependenciesAsync(assets, Addressables.MergeMode.Union, true);
            
            while (!handle.IsDone)
            {
                var status = handle.GetDownloadStatus();
                _loggerService.Info("Total: {0}b; Downloaded: {1}b", status.TotalBytes, status.DownloadedBytes);
                
                OnProgress?.Invoke(status);
                await UniTask.NextFrame();
            }
            
            _loggerService.Info("Download Handle: {0}", handle.IsDone);
        }
    }
}