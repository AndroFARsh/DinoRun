using Leopotam.EcsLite;
using Services.StaticData;
using UnityEngine;

namespace Game
{
    public class CameraFollowSystem : IEcsInitSystem,  IEcsRunSystem
    {
        private readonly Camera _camera;
        private readonly IOffsetPosition _offsetPosition;
        private readonly StaticData _staticData;
        
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<FollowComponent> _followPool;
        private EcsPool<CharacterComponent> _characterPool;

        CameraFollowSystem(Camera camera, IOffsetPosition offsetPosition, StaticData staticData)
        {
            _camera = camera;
            _offsetPosition = offsetPosition;
            _staticData = staticData;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _followPool = _world.GetPool<FollowComponent>();
            _characterPool = _world.GetPool<CharacterComponent>();
            _filter = _world.Filter<CharacterComponent>()
                .Inc<FollowComponent>()
                .Inc<GameStartTag>()
                .Exc<GameOverTag>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var followComponent = ref _followPool.Get(entity);
                var characterComponent = _characterPool.Get(entity);

                followComponent.Value = Mathf.Clamp01(followComponent.Value + Time.deltaTime * _staticData.CameraMoveSpeed);

                var offset = Vector2.Lerp(Vector2.zero, _offsetPosition.Offset, followComponent.Value);
                var characterPosition = characterComponent.Value.transform.position;
                var transform = _camera.transform;
                
                transform.position = new Vector3
                {
                    x = characterPosition.x + offset.x,
                    y = transform.position.y + offset.y,
                    z = transform.position.z
                };
            }
        }
    }
}