using Cysharp.Threading.Tasks;
using Infrastructure;

namespace Services.Ads
{
    public enum AdsShowState
    {
        AdsShowClicked,
        AdsShowFailed,
        AdsShowCompleted
    }

    public interface IAdvertisementService : IAsyncInitializer
    {
        bool IsSupported { get; }
        bool IsInitialized { get; }
        bool IsAdsShowing { get; }
        bool IsTestMode { get; }

        UniTask<AdsShowState> ShowAds();
    }
}