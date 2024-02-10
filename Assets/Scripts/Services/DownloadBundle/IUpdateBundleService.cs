using System;
using Infrastructure;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Services
{
    public interface IUpdateBundleService : IAsyncInitializer
    {
        public event Action<DownloadStatus> OnProgress;
    }
}