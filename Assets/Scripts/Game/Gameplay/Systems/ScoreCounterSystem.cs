using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class ScoreCounterSystem : IEcsInitSystem,  IEcsRunSystem
    {
        private readonly ScoreBoard _scoreBoard;
        private EcsFilter _filter;

        ScoreCounterSystem(ScoreBoard scoreBoard)
        {
            _scoreBoard = scoreBoard;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            _filter = world.Filter<GameStartTag>()
                .Exc<GameOverTag>()
                .End();
        }
    
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                _scoreBoard.Tick();
            }
        }
    }
}