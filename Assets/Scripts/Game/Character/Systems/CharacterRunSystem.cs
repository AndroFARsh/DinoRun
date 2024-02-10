using Leopotam.EcsLite;
using Services.StaticData;
using UnityEngine;

namespace Game
{
    public class CharacterRunSystem : IEcsInitSystem,  IEcsRunSystem
    {
        private readonly StaticData _staticData;
        private readonly ScoreBoard _scoreBoard;

        private EcsPool<VelocityComponent> _velocityPool;
        private EcsFilter _filter;

        CharacterRunSystem(StaticData staticData, ScoreBoard scoreBoard)
        {
            _staticData = staticData;
            _scoreBoard = scoreBoard;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _velocityPool = world.GetPool<VelocityComponent>();
            
            _filter = world.Filter<CharacterComponent>()
                .Inc<VelocityComponent>()
                .Inc<GameStartTag>()
                .Inc<MoveTag>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var velocityComponent = ref _velocityPool.Get(entity);
                velocityComponent.VelocityX = _staticData.GetCharacterSpeed(_scoreBoard.CurrentScore);
            }
        }
    }
}