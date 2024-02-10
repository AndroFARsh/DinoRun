using Leopotam.EcsLite;
using Services.StaticData;
using UnityEngine;

namespace Game
{
    public class GravitySystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly StaticData _staticData;
        
        private EcsFilter _filter;
        private EcsPool<GroundedTag> _groundedPool;
        private EcsPool<VelocityComponent> _velocityPool;

        GravitySystem(StaticData staticData)
        {
            _staticData = staticData;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _groundedPool = world.GetPool<GroundedTag>();
            _velocityPool = world.GetPool<VelocityComponent>();
            _filter = world.Filter<CharacterComponent>()
                .Inc<GravityTag>()
                .Exc<DeadTag>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var velocityComponent = ref _velocityPool.Get(entity);
                var newVelocityY = !_groundedPool.Has(entity)
                    ? Mathf.Clamp(
                        value: velocityComponent.VelocityY + _staticData.Gravity * Time.deltaTime,
                        min: -_staticData.MaxVelocity,
                        max: _staticData.MaxVelocity
                    )
                    : 0;
                
                velocityComponent.VelocityY = newVelocityY;
            }
        }
    }
}