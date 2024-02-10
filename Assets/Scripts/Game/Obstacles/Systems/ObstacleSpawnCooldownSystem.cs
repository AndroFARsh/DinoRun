using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class ObstacleSpawnCooldownSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<ObstacleSpawnCooldownComponent> _pool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _pool = _world.GetPool<ObstacleSpawnCooldownComponent>();
            _filter = _world.Filter<ObstacleSpawnCooldownComponent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var component = ref _pool.Get(entity);
                
                component.Value -= Time.deltaTime;
                if (component.Value < 0)
                {
                    _pool.Del(entity);
                }
            }
        }
    }
}