using Leopotam.EcsLite;

namespace Game
{
    public class CharacterAnimateSystem : IEcsInitSystem,  IEcsRunSystem
    {
        private EcsPool<AnimatorComponent> _animatorPool;
        private EcsFilter _characterFilter;
        
        private EcsPool<DeadTag> _deadPool;
        private EcsPool<MoveTag> _movePool;
        private EcsPool<CrouchTag> _crouchPool;
        private EcsPool<GroundedTag> _groundedPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _animatorPool = world.GetPool<AnimatorComponent>();
            _groundedPool = world.GetPool<GroundedTag>();
            _crouchPool = world.GetPool<CrouchTag>();
            _deadPool = world.GetPool<DeadTag>();
            _movePool = world.GetPool<MoveTag>();
            
            _characterFilter = world.Filter<CharacterComponent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _characterFilter)
            {
                CharacterAnimatorMono animator = _animatorPool.Get(entity);
                if (_deadPool.Has(entity))
                {
                    animator.Die();
                }
                else if (_movePool.Has(entity) && !_groundedPool.Has(entity))
                {
                    animator.Jump();
                } 
                else if (_movePool.Has(entity) && _crouchPool.Has(entity))
                {
                    animator.Crouch();
                }
                else if (_movePool.Has(entity))
                {
                    animator.Run();
                }
                else
                {
                    animator.Idle();
                }
            }
        }
    }
}