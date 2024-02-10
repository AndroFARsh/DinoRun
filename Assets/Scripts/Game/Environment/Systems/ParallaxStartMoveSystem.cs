using System.Collections.Generic;
using Infrastructure.Utils;
using Leopotam.EcsLite;

namespace Game
{
    public class ParallaxStartMoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _startFilter;
        private EcsFilter _parallaxFilter;
        
        private EcsPool<MoveTag> _movePool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _movePool = world.GetPool<MoveTag>();
            
            _startFilter = world.Filter<GameStartTag>().Exc<GameOverTag>().End();
            _parallaxFilter = world.Filter<ParallaxComponent>().Exc<MoveTag>().End();
        }

        public void Run(IEcsSystems systems)
        {
            if (_startFilter.IsEmpty()) return;

            foreach (var entity in _parallaxFilter)
            {
                _movePool.Add(entity);
            }
        }
    }
}