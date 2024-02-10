using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class VelocityApplySystem : IEcsInitSystem,  IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<VelocityComponent> _velocityPool;
        private EcsPool<RigidbodyComponent> _rigidBodyPool;
        private EcsPool<GroundHitComponent> _groundHitPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _groundHitPool = world.GetPool<GroundHitComponent>();
            _rigidBodyPool = world.GetPool<RigidbodyComponent>();
            _velocityPool = world.GetPool<VelocityComponent>();
            
            _filter = world.Filter<RigidbodyComponent>()
                .Inc<VelocityComponent>()
                .Exc<GameOverTag>()
                .End();
        }
    
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                var velocityComponent = _velocityPool.Get(entity);
                Rigidbody2D rigidBody = _rigidBodyPool.Get(entity);

                var velocityY = velocityComponent.VelocityY * Time.deltaTime;
                if (velocityY < 0 && _groundHitPool.Has(entity))
                {
                    var hit = _groundHitPool.Get(entity).Value;
                    velocityY = -Mathf.Min(Mathf.Abs(velocityY), hit.Distance);
                }

                rigidBody.position += velocityY * Vector2.up + 
                                      velocityComponent.VelocityX * Time.deltaTime * Vector2.right; 
            }
        }
    }
}