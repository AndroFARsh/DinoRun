using Leopotam.EcsLite;
using Services;
using Services.StaticData;

namespace Game
{
    public class CharacterGroundJumpSystem : IEcsInitSystem,  IEcsRunSystem
    {
        private readonly StaticData _staticData;
        private readonly InputService _inputService;
        private readonly IEffectService _effectService;

        private EcsPool<VelocityComponent> _velocityPool;
        private EcsPool<GroundedTag> _groundedPool;
        private EcsPool<JumpComponent> _jumpPool;
        private EcsFilter _filter;

        CharacterGroundJumpSystem(StaticData staticData, InputService inputService, IEffectService effectService)
        {
            _staticData = staticData;
            _inputService = inputService;
            _effectService = effectService;
        }
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _velocityPool = world.GetPool<VelocityComponent>();
            _jumpPool = world.GetPool<JumpComponent>();
            _groundedPool = world.GetPool<GroundedTag>();
            
            _filter = world.Filter<CharacterComponent>()
                .Inc<GameStartTag>()
                .Inc<GroundedTag>()
                .Inc<MoveTag>()
                .Inc<VelocityComponent>()
                .Exc<JumpComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var velocityComponent = ref _velocityPool.Get(entity);
                if (_inputService.IsJump)
                {
                    _effectService.PlayEffect(Effect.Jump);
                    
                    _groundedPool.Del(entity);
                    _jumpPool.Add(entity).Delay = _staticData.LongJumpDuration;
                    
                    velocityComponent.VelocityY = _staticData.JumpInitialVelocity;
                }
            }
        }
    }
}