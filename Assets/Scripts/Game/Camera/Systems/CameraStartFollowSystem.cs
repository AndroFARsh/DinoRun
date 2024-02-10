using Leopotam.EcsLite;

namespace Game
{
    public class CameraStartFollowSystem : IEcsInitSystem,  IEcsRunSystem
    {
        private EcsPool<FollowComponent> _followPool;
        private EcsFilter _filter;
        private EcsWorld _world;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _followPool = _world.GetPool<FollowComponent>();
            _filter = _world.Filter<CharacterComponent>()
                .Inc<GameStartTag>()
                .Exc<GameOverTag>()
                .Exc<FollowComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                _followPool.Add(entity);
            }
        }
    }
}