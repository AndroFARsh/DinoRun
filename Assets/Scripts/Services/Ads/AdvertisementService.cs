using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Infrastructure;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Services.Ads
{
    class AdvertisementService : IAdvertisementService
    {
        private const string iOSAppId = "5532496";
        private const string AndroidAppId = "5532497";
        
        private const string iOSPlacementId = "Interstitial_iOS";
        private const string AndroidPlacementId = "Interstitial_Android";
        
        private string gameId => ProjectData.Platform == RuntimePlatform.IPhonePlayer ? iOSAppId : AndroidAppId;

        private string placementId => ProjectData.Platform == RuntimePlatform.IPhonePlayer ? iOSPlacementId : AndroidPlacementId;

        public bool IsSupported => Advertisement.isSupported;
        
        public bool IsInitialized => Advertisement.isInitialized;

        public bool IsAdsShowing => Advertisement.isShowing;
        
        public bool IsTestMode => true;

        public async UniTask Initialize()
        {
            if (!IsSupported) return;
            
            await Init();
            await Load();
        }


        public async UniTask<AdsShowState> ShowAds()
        {
            if (!IsSupported || !IsInitialized || IsAdsShowing) return AdsShowState.AdsShowFailed;
            
            var taskCompletionSource = new UniTaskCompletionSource<AdsShowState>();
            Advertisement.Show(placementId, new ShowListener(taskCompletionSource));
            
            return await taskCompletionSource.Task;
        }

        private async Task Load()
        {
            var taskCompletionSource = new UniTaskCompletionSource();
            
            Advertisement.Load(placementId, new LoadListener(taskCompletionSource));
            
            await taskCompletionSource.Task;
        }

        private async Task Init()
        {
            var taskCompletionSource = new UniTaskCompletionSource();
            
            Advertisement.Initialize(gameId, IsTestMode, new InitializationListener(taskCompletionSource));

            await taskCompletionSource.Task;
        }
        
        private readonly struct ShowListener : IUnityAdsShowListener
        {
            private readonly UniTaskCompletionSource<AdsShowState> _taskCompletionSource;

            public ShowListener(UniTaskCompletionSource<AdsShowState> taskCompletionSource)
            {
                _taskCompletionSource = taskCompletionSource;
            }

            public void OnUnityAdsShowStart(string placementId) { }

            public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) =>
                _taskCompletionSource.TrySetResult(AdsShowState.AdsShowFailed);

            public void OnUnityAdsShowClick(string placementId) =>
                _taskCompletionSource.TrySetResult(AdsShowState.AdsShowClicked);
            
            public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) =>
                _taskCompletionSource.TrySetResult(AdsShowState.AdsShowCompleted);
        }

        private readonly struct LoadListener : IUnityAdsLoadListener
        {
            private readonly UniTaskCompletionSource _taskCompletionSource;

            public LoadListener(UniTaskCompletionSource taskCompletionSource)
            {
                _taskCompletionSource = taskCompletionSource;
            }
            
            public void OnUnityAdsAdLoaded(string placementId) => _taskCompletionSource.TrySetResult();

            public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
                => _taskCompletionSource.TrySetException(new Exception(message));
        }

        private readonly struct InitializationListener : IUnityAdsInitializationListener
        {
            private readonly UniTaskCompletionSource _taskCompletionSource;
            
            public InitializationListener(UniTaskCompletionSource taskCompletionSource)
            {
                _taskCompletionSource = taskCompletionSource;
            }

            public void OnInitializationComplete() => _taskCompletionSource.TrySetResult();

            public void OnInitializationFailed(UnityAdsInitializationError error, string message)
            {
                _taskCompletionSource.TrySetException(new Exception(message));
            }
        }
    }
}