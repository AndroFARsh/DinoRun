using Cysharp.Threading.Tasks;
using Services;
using UnityEngine;
using UnityEngine.UIElements;

namespace Infrastructure.Utils
{
    public static class AssetProviderUtils
    {
        public static async UniTask<GameObject> CreateGameObject(this IAssetProvider assetProvider, string assetKey)
            => await CreateGameObject(assetProvider, assetKey, Vector3.zero);
        
        public static async UniTask<GameObject> CreateGameObject(this IAssetProvider assetProvider, string assetKey, Vector3 position)
            => await CreateGameObject(assetProvider, assetKey,position, Quaternion.identity);
        
        public static async UniTask<GameObject> CreateGameObject(this IAssetProvider assetProvider, string assetKey, Vector3 position, Quaternion rotation)
        {
            var prefab = await assetProvider.Load<GameObject>(assetKey);
            var go = Object.Instantiate(prefab, position, rotation);
            
            go.name = go.name.Replace("(Clone)", "");

            return go;
        }

        public static async UniTask<TVisualElement> CreateVisualElement<TVisualElement>(
            this IAssetProvider assetProvider,
            string assetKey,
            string name = null,
            string className = null
        )
            where TVisualElement : VisualElement
        {
            var prefab = await assetProvider.Load<VisualTreeAsset>(assetKey);
            var templateContainer = prefab.Instantiate();
            var element = templateContainer.Q<TVisualElement>(name, className);
            return element;
        }
    }
}