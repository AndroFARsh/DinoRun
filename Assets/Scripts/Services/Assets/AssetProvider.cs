using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Services
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _assetRequests = new ();
        private readonly List<object> _allAssetKeys = new ();
        private IResourceLocator _resourceLocator;

        public async UniTask Initialize()
        {
            _resourceLocator = await Addressables.InitializeAsync().ToUniTask();
            
            foreach (var key in _resourceLocator.Keys)
                _allAssetKeys.Add(key);
        }

        public void Dispose()
        {
            foreach (var handle in _assetRequests.Values)
            {
                Addressables.Release(handle);
            }
            _assetRequests.Clear();
            Addressables.RemoveResourceLocator(_resourceLocator);
        }
        
        public UniTask<IReadOnlyList<object>> ListOfAllAssets()
            => UniTask.FromResult<IReadOnlyList<object>>(_allAssetKeys);

        public UniTask<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : class
            => Load<TAsset>(assetReference.AssetGUID);
        public async UniTask<TAsset> Load<TAsset>(string key) where TAsset : class
        {
            if (!_assetRequests.TryGetValue(key, out var handle))
            {
                handle = Addressables.LoadAssetAsync<TAsset>(key);
                _assetRequests.Add(key, handle);
            }

            await handle.ToUniTask();
            
            return handle.Result as TAsset;
        }

        public async UniTask<TAsset[]> LoadAll<TAsset>(IEnumerable<string> keys) where TAsset : class
            => await UniTask.WhenAll(keys.Select(Load<TAsset>));
        
        public async UniTask WarmupAssetsByLabel(string label)
        {
            var assetsList = await ListOfLabeledAssets(label);
            await LoadAll<object>(assetsList);
        }

        public async UniTask ReleaseAssetsByLabel(string label)
        {
            var assetsList = await ListOfLabeledAssets(label);

            foreach (var assetKey in assetsList)
            {
                if (_assetRequests.TryGetValue(assetKey, out var handle))
                {
                    Addressables.Release(handle);
                    _assetRequests.Remove(assetKey);
                }
            }
        }

        public async UniTask<long> BytesToLoadByLabel(string label)
        {
            var assetsList = await ListOfLabeledAssets(label);
            return await Addressables.GetDownloadSizeAsync(assetsList).ToUniTask();
        }
        
        public async UniTask<long> BytesToLoadByLabels(IEnumerable<string> labels)
        {
            var results = await UniTask.WhenAll(labels.Select(BytesToLoadByLabel));
            return results.Sum();
        }

        public UniTask<IReadOnlyList<string>> ListOfLabeledAssets<TAsset>(string label)
            => ListOfLabeledAssets(label, typeof(TAsset));
        
        public async UniTask<IReadOnlyList<string>> ListOfLabeledAssets(string label, Type type = null)
        {
            var operationHandle = Addressables.LoadResourceLocationsAsync(label, type);
            var locations = await operationHandle.ToUniTask();

            var assetKeys = new List<string>(locations.Count);
            foreach (var location in locations) 
                assetKeys.Add(location.PrimaryKey);
            
            Addressables.Release(operationHandle);
            return assetKeys;
        }
    }
}