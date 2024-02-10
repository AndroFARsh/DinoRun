using Leopotam.EcsLite;
using Services;
using Services.StaticData;
using UnityEngine;

namespace Game
{
    public class CharacterLongJumpSystem : IEcsInitSystem,  IEcsRunSystem
    {
        private readonly InputService _inputService;
        private readonly StaticData _staticData;

        private EcsPool<VelocityComponent> _velocityPool;
        private EcsPool<JumpComponent> _jumpPool;
        private EcsFilter _filter;
        
        CharacterLongJumpSystem(InputService inputService, StaticData staticData)
        {
            _inputService = inputService;
            _staticData = staticData;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _velocityPool = world.GetPool<VelocityComponent>();
            _jumpPool = world.GetPool<JumpComponent>();
            
            _filter = world.Filter<CharacterComponent>()
                .Exc<GroundedTag>()
                .Inc<JumpComponent>()
                .Inc<MoveTag>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var velocityComponent = ref _velocityPool.Get(entity);
                ref var jump = ref _jumpPool.Get(entity);

                jump.Delay -= Time.deltaTime;
                if (jump.Delay > 0f && _inputService.IsJumpInProgress)
                {
                    velocityComponent.VelocityY += _staticData.LongJumpFactor * Physics2D.gravity.magnitude * Time.deltaTime;
                }
                else
                {
                    _jumpPool.Del(entity);
                }
            }
        }
    }
}