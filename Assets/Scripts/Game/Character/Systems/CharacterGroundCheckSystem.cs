using Leopotam.EcsLite;
using Services;

namespace Game
{
    public class CharacterGroundCheckSystem : IEcsInitSystem,  IEcsRunSystem
    {
        private readonly IEffectService _effectService;
        
        private EcsPool<GroundHitComponent> _groundDistancePool;
        private EcsPool<GroundCheckComponent> _groundCheckPool;
        private EcsPool<GameStartTag> _gameStartPool;
        private EcsPool<GroundedTag> _groundedPool;
        
        private EcsFilter _filter;

        CharacterGroundCheckSystem(IEffectService effectService)
        {
            _effectService = effectService;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _groundDistancePool = world.GetPool<GroundHitComponent>();
            _groundCheckPool = world.GetPool<GroundCheckComponent>();
            _groundedPool = world.GetPool<GroundedTag>();
            _gameStartPool = world.GetPool<GameStartTag>();
            
            _filter = world.Filter<GroundCheckComponent>()
                .Exc<JumpComponent>()
                .Exc<GroundedTag>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var groundDistanceComponent = ref _groundDistancePool.Get(entity);
                var groundCheckComponent = _groundCheckPool.Get(entity);

                groundDistanceComponent.Value = groundCheckComponent.Value.CheckGround();
                if (groundDistanceComponent.Value.Distance < 0.05f)
                {
                    _groundedPool.Add(entity);
                    
                    if (_gameStartPool.Has(entity))
                    {
                        _effectService.PlayEffect(Effect.Run);
                    }
                }
            }
        }
    }
}