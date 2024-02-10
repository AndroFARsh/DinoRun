using Leopotam.EcsLite;
using Services.StaticData;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Game
{
    public class ParallaxMoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsPool<ParallaxComponent> _parallaxPool;
        
        private readonly StaticData _staticData;
        private readonly ScoreBoard _scoreBoard;
        private EcsFilter _parallaxFilter;
        
        ParallaxMoveSystem(StaticData staticData, ScoreBoard scoreBoard)
        {
            _staticData = staticData;
            _scoreBoard = scoreBoard;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _parallaxPool = world.GetPool<ParallaxComponent>();
            _parallaxFilter = world.Filter<ParallaxComponent>().Inc<MoveTag>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _parallaxFilter)
            {
                ref var parallaxComponent = ref _parallaxPool.Get(entity);
                parallaxComponent.Transform.position += parallaxComponent.Factor * _staticData.GetCharacterSpeed(_scoreBoard.CurrentScore) * Time.deltaTime * Vector3.right;
            }
        }
    }
}