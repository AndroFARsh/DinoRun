using Leopotam.EcsLite;
using Services;

namespace Game
{
    public class GameStartSystem : IEcsInitSystem,  IEcsRunSystem
    {
        private readonly InputService _inputService;
        
        private EcsPool<GameStartTag> _startPool;
        private EcsFilter _filter;
        private EcsWorld _world;

        GameStartSystem(InputService inputService)
        {
            _inputService = inputService;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _startPool = _world.GetPool<GameStartTag>();
            
            _filter = _world.Filter<CharacterComponent>()
                .Exc<GameStartTag>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                if (_inputService.StartGame)
                {
                    _startPool.Add(entity);
                }
            }
        }
    }
}