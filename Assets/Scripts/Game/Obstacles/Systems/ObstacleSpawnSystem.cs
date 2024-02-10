using Leopotam.EcsLite;
using Services.StaticData;
using UnityEngine;

namespace Game
{
    public class ObstacleSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IObstacleSpawnPoint _spawnPoint;
        private readonly ObstaclePool _pool;
        private readonly ScoreBoard _scoreBoard;
        private readonly StaticData _staticData;

        private EcsWorld _world;
        
        private EcsFilter _obstacleFilter;
        private EcsFilter _gameStartFilter;
        
        private EcsPool<ObstacleSpawnCooldownComponent> _obstacleSpawnCooldownPool;
        private EcsPool<ObstacleMoveFactorComponent> _moveFactorPool;
        private EcsPool<ObstacleComponent> _obstaclePool;
        private EcsPool<MoveTag> _movePool;
        
        ObstacleSpawnSystem(IObstacleSpawnPoint spawnPoint, ObstaclePool pool, ScoreBoard scoreBoard, StaticData staticData)
        {
            _pool = pool;
            _spawnPoint = spawnPoint;
            _scoreBoard = scoreBoard;
            _staticData = staticData;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _obstacleSpawnCooldownPool = _world.GetPool<ObstacleSpawnCooldownComponent>();
            _obstaclePool = _world.GetPool<ObstacleComponent>();
            _movePool = _world.GetPool<MoveTag>();
            _moveFactorPool = _world.GetPool<ObstacleMoveFactorComponent>();

            _gameStartFilter = _world.Filter<GameStartTag>()
                .Exc<ObstacleSpawnCooldownComponent>()
                .Exc<GameOverTag>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _gameStartFilter)
            {
                var obstacle = _pool.Get(_staticData.GetObstacleType(_scoreBoard.CurrentScore));
                _spawnPoint.ApplyPosition(obstacle);

                var obstacleEntity = _world.NewEntity();
                _obstaclePool.Add(obstacleEntity) = new ObstacleComponent
                {
                    Value = obstacle,
                };
                _obstacleSpawnCooldownPool.Add(entity).Value = _staticData.GetObstacleCooldown(_scoreBoard.CurrentScore);

                if (obstacle.Type == ObstacleType.Movable)
                {
                    _movePool.Add(obstacleEntity);
                    _moveFactorPool.Add(obstacleEntity).Value = (obstacle.Type == ObstacleType.Movable)
                        ? _staticData.MovableObstacleSpeed
                        : 0;
                }
            }
        }
    }
}