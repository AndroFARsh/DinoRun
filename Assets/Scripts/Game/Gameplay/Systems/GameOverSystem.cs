using Infrastructure;
using Leopotam.EcsLite;
using NUnit.Framework.Internal;
using Services;

namespace Game
{
    public class GameOverSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IStateMachine _stateMachine;
        private readonly ILoggerService _logger;

        private EcsWorld _world;
        
        private EcsPool<GameOverTag> _gameOverPool;
        private EcsPool<MoveTag> _movePool;
        
        private EcsFilter _gameOverFilter;
        private EcsFilter _moveFilter;

        GameOverSystem(IStateMachine stateMachine, ILoggerService logger)
        {
            _stateMachine = stateMachine;
            _logger = logger;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _movePool = _world.GetPool<MoveTag>();
            _gameOverPool = _world.GetPool<GameOverTag>();
            _gameOverFilter = _world.Filter<DeadTag>()
                .Exc<GameOverTag>()
                .End();
            
            _moveFilter = _world.Filter<MoveTag>().End();
        }

        public void Run(IEcsSystems systems)
        {
            if (_gameOverFilter.GetEntitiesCount() > 1)
            {
                _logger.Error($"Amount of game over entities can be one or less. {_gameOverFilter.GetEntitiesCount()}");
            }
            
            foreach (var entity in _gameOverFilter)
            {
                StopMove();
                    
                _gameOverPool.Add(entity);
                _stateMachine.Enter<GameOverState>();
            }
        }
        
        private void StopMove()
        {
            foreach (var entity in _moveFilter)
            {
                _movePool.Del(entity);
            }
        }
    }
}