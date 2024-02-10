using Leopotam.EcsLite;

namespace Game
{
    public class CharacterSpawnSystem : IEcsInitSystem
    {
        private readonly CharacterPool _characterPool;
        
        CharacterSpawnSystem(CharacterPool characterPool)
        {
            _characterPool = characterPool;
        }

        public void Init(IEcsSystems systems) {
            var go = _characterPool.Get();
            var character = go.GetComponent<ICharacter>();
            var world = systems.GetWorld();
            
            var entity = world.NewEntity();
            world.GetPool<CharacterComponent>().Add(entity) = CharacterComponent.Create(character);
            world.GetPool<AnimatorComponent>().Add(entity) = AnimatorComponent.Create(character);
            world.GetPool<GroundCheckComponent>().Add(entity) = GroundCheckComponent.Create(character);
            world.GetPool<ObstacleCheckComponent>().Add(entity) = ObstacleCheckComponent.Create(character);
            world.GetPool<RigidbodyComponent>().Add(entity) = RigidbodyComponent.Create(character);
            world.GetPool<GroundHitComponent>().Add(entity);
            world.GetPool<GravityTag>().Add(entity);
            world.GetPool<VelocityComponent>().Add(entity);
        }
    }
}