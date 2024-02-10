using Leopotam.EcsLite;
using Services;

namespace Game
{
    public class CharacterCrouchSystem : IEcsInitSystem,  IEcsRunSystem
    {
        private readonly InputService _inputService;
        
        private EcsPool<CrouchTag> _crouchPool;
        private EcsFilter _filter;
        
        CharacterCrouchSystem(InputService inputService)
        {
            _inputService = inputService;
        }
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _crouchPool = world.GetPool<CrouchTag>();
            _filter = world.Filter<CharacterComponent>()
                .Exc<JumpComponent>()
                .Inc<GroundedTag>()
                .Inc<MoveTag>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                if (_inputService.IsCrouch)
                {
                    if (!_crouchPool.Has(entity)) _crouchPool.Add(entity);
                }
                else
                {
                    if (_crouchPool.Has(entity)) _crouchPool.Del(entity); 
                }
            }
        }
    }
}