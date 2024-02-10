using System;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Projects;
using Services;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Loader
{
    public class BundleLoaderPresenter : IDisposable
    {
        public event Action<float> OnProgress;

        private readonly IUpdateBundleService _updateService;
        private readonly IStateMachine _stateMachine;

        public BundleLoaderPresenter(IUpdateBundleService updateService)
        {
            _updateService = updateService;
            _updateService.OnProgress += OnUpdate;
        }
 
        public void Dispose() => _updateService.OnProgress -= OnUpdate;
        
        private void OnUpdate(DownloadStatus data) => OnProgress?.Invoke(data.Percent);
    }
}