using System;
using System.Collections.Generic;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Services;
using UnityEngine;

namespace Game
{
    public class CharacterPool : IAsyncInitializer, IDisposable
    {
        private readonly ICharacterSpawnPoint _spawnPoint;
        private readonly IAssetProvider _assetProvider;
        
        private GameObject _prefab;
        private List<GameObject> _pool = new();

        CharacterPool(ICharacterSpawnPoint spawnPoint, IAssetProvider assetProvider)
        {
            _spawnPoint = spawnPoint;
            _assetProvider = assetProvider;
        }

        public async UniTask Initialize()
        {
            _prefab = await _assetProvider.Load<GameObject>(AssetKeys.Character);
        }
        
        public void Dispose()
        {
            foreach (var go in _pool)
            {
                GameObject.Destroy(go);
            }
            _pool.Clear();
        }

        public GameObject Get()
        {
            if (_pool.TryPopRandom(out var go))
            {
                go.SetActive(true);
                go.transform.position = _spawnPoint.Position;
                return go;
            }
            
            return GameObject.Instantiate(_prefab, _spawnPoint.Position, Quaternion.identity);
        }

        public void Return(GameObject go)
        {
            go.SetActive(false);
            go.transform.SetParent(null);
            _pool.Add(go);
        }
    }
}