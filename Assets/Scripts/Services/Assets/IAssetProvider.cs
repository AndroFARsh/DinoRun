using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure;
using UnityEngine.AddressableAssets;

namespace Services
{
    public interface IAssetProvider : IAsyncInitializer, IDisposable
    {
        UniTask<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : class;
        UniTask<TAsset> Load<TAsset>(string key) where TAsset : class;
        UniTask<TAsset[]> LoadAll<TAsset>(IEnumerable<string> keys) where TAsset : class;
        
        UniTask WarmupAssetsByLabel(string label);
        UniTask ReleaseAssetsByLabel(string label);
        
        UniTask<long> BytesToLoadByLabel(string label);
        
        UniTask<long> BytesToLoadByLabels(IEnumerable<string> labels);
        
        UniTask<IReadOnlyList<string>> ListOfLabeledAssets<TAsset>(string label);

        UniTask<IReadOnlyList<string>> ListOfLabeledAssets(string label, Type type = null);
        
        UniTask<IReadOnlyList<object>> ListOfAllAssets();
    }
}