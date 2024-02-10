using CodeBase.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class ObstacleTeardownSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly Camera _camera;
        private readonly ObstaclePool _pool;
        
        private EcsWorld _world;
        private EcsFilter _obstacleFilter;
        private EcsFilter _groundFilter;
        
        private EcsPool<ObstacleComponent> _obstaclePool;
        private EcsPool<ObstacleOnScreenTag> _obstacleOnScreenPool;

        ObstacleTeardownSystem(Camera camera, ObstaclePool pool)
        {
            _camera = camera;
            _pool = pool;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _obstaclePool = _world.GetPool<ObstacleComponent>();
            _obstacleOnScreenPool = _world.GetPool<ObstacleOnScreenTag>();
            
            _obstacleFilter = _world.Filter<ObstacleComponent>().End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _obstacleFilter)
            {
                ref var obstacle = ref _obstaclePool.Get(entity);
                var obstacleBounds = new Bounds(
                    center: obstacle.Value.Transform.localPosition + obstacle.Value.Bounds.center,
                    size: obstacle.Value.Bounds.size
                );
                var cameraBounds = _camera.OrthographicBounds();
                var intersected = cameraBounds.Intersects(obstacleBounds);
                var obstacleOnScreen = _obstacleOnScreenPool.Has(entity);
                
                if (!obstacleOnScreen && intersected)
                {
                    _obstacleOnScreenPool.Add(entity);
                } 
                else if (obstacleOnScreen && !intersected)
                {
                    _pool.Return(obstacle.Value);
                    _world.DelEntity(entity);
                }
            }
        }
        
        public void Destroy(IEcsSystems systems)
        {
            foreach (var e in _obstacleFilter)
            {
                var obstacle = _obstaclePool.Get(e);
                
                _pool.Return(obstacle.Value); 
                _world.DelEntity(e);
            }
        }
    }
}