using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Services;
using UnityEngine;

namespace Game
{
    public class ObstaclePool : IAsyncInitializer, IDisposable
    {
        private readonly IAssetProvider _assetProvider;
        private readonly Dictionary<ObstacleType, GameObject> _prefabs = new ();
        private readonly Dictionary<ObstacleType, Queue<IObstacle>> _pools = new();
        
        ObstaclePool(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask Initialize()
        {
            _prefabs.Add(ObstacleType.StaticSmallOne, await _assetProvider.Load<GameObject>(AssetKeys.ObstacleSmall1));
            _prefabs.Add(ObstacleType.StaticSmallTwo, await _assetProvider.Load<GameObject>(AssetKeys.ObstacleSmall2));
            _prefabs.Add(ObstacleType.StaticSmallThree, await _assetProvider.Load<GameObject>(AssetKeys.ObstacleSmall3));
            _prefabs.Add(ObstacleType.StaticLargeOne, await _assetProvider.Load<GameObject>(AssetKeys.ObstacleLarge1));
            _prefabs.Add(ObstacleType.StaticLargeTwo, await _assetProvider.Load<GameObject>(AssetKeys.ObstacleLarge2));
            _prefabs.Add(ObstacleType.StaticLargeThree, await _assetProvider.Load<GameObject>(AssetKeys.ObstacleLarge3));
            _prefabs.Add(ObstacleType.Movable, await _assetProvider.Load<GameObject>(AssetKeys.ObstacleMovable));
        }
        
        public void Dispose()
        {
            _pools.Clear();
            _prefabs.Clear();
        }

        public IObstacle Get(ObstacleType obstacleType)
        {
            if (_pools.TryGetValue(obstacleType, out var pool) && pool.Count > 0)
            {
                var obstacle = pool.Dequeue();
                obstacle.GameObject.SetActive(true);
                return obstacle;
            } 
            
            var go = GameObject.Instantiate(_prefabs[obstacleType], Vector3.up * 100, Quaternion.identity);
            return go.GetComponent<IObstacle>();
        }

        public void Return(IObstacle obstacle)
        {
            if (!_pools.ContainsKey(obstacle.Type))
            {
                _pools.Add(obstacle.Type, new Queue<IObstacle>());
            }
        
            obstacle.GameObject.SetActive(false);
            _pools[obstacle.Type].Enqueue(obstacle);
        }
    }
}