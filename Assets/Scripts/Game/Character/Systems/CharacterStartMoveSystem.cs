using Infrastructure.Utils;
using Leopotam.EcsLite;
using Services;

namespace Game
{
    public class CharacterStartMoveSystem : IEcsInitSystem,  IEcsRunSystem
    {
        private readonly IEffectService _effectService;
        
        private EcsFilter _startFilter;
        private EcsFilter _characterFilter;
        
        private EcsPool<GroundedTag> _groundedPool;
        private EcsPool<MoveTag> _movePool;

        CharacterStartMoveSystem(IEffectService effectService)
        {
            _effectService = effectService;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _movePool = world.GetPool<MoveTag>();
            _groundedPool = world.GetPool<GroundedTag>();
            
            _startFilter = world.Filter<GameStartTag>().Exc<GameOverTag>().End();
            _characterFilter = world.Filter<CharacterComponent>().Exc<MoveTag>().End();
        }

        public void Run(IEcsSystems systems)
        {
            if (_startFilter.IsEmpty()) return;

            foreach (var entity in _characterFilter)
            {
                _movePool.Add(entity);
                if (_groundedPool.Has(entity))
                {
                    _effectService.PlayEffect(Effect.Run);
                }
            }
        }
    }
}