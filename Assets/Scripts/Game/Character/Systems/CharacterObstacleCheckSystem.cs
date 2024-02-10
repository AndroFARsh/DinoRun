using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class CharacterObstacleCheckSystem : IEcsInitSystem,  IEcsRunSystem
    {
        private EcsPool<ObstacleCheckComponent> _obstacleCheckPool;
        private EcsPool<DeadTag> _deadPool;
        private EcsFilter _filter;
        private EcsPool<CrouchTag> _crouchPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _obstacleCheckPool = world.GetPool<ObstacleCheckComponent>();
            _deadPool = world.GetPool<DeadTag>();
            _crouchPool = world.GetPool<CrouchTag>();
            
            _filter = world.Filter<CharacterComponent>()
                .Inc<ObstacleCheckComponent>()
                .Inc<MoveTag>()
                .Exc<DeadTag>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                var obstacleCheckComponent = _obstacleCheckPool.Get(entity);
                var collider = _crouchPool.Has(entity) 
                    ? obstacleCheckComponent.CrouchObstacleCheck 
                    : obstacleCheckComponent.RunObstacleCheck;
                
                if (collider.IsOverlapped())
                {
                  _deadPool.Add(entity);
                }
            }
        }
    }
}