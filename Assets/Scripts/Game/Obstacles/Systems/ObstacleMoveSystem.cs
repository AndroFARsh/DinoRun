using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class ObstacleMoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        
        private EcsPool<ObstacleComponent> _obstaclePool;
        private EcsPool<ObstacleMoveFactorComponent> _factorPool;

        public void Init(IEcsSystems systems) {
            var world = systems.GetWorld();
            _obstaclePool = world.GetPool<ObstacleComponent>();
            _factorPool = world.GetPool<ObstacleMoveFactorComponent>();
            
            _filter = world.Filter<ObstacleComponent>()
                .Inc<ObstacleMoveFactorComponent>()
                .Inc<MoveTag>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var obstacleComponent = ref _obstaclePool.Get(entity);
                ref var factorComponent = ref _factorPool.Get(entity);

                var offset =  Time.deltaTime * factorComponent.Value * Vector3.left;
                
                obstacleComponent.Value.Transform.localPosition += offset;
            }
        }
    }
}