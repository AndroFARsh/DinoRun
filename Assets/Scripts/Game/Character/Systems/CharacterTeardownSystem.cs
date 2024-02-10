using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class CharacterTeardownSystem : IEcsDestroySystem
    {
        private readonly CharacterPool _characterPool;

        CharacterTeardownSystem(CharacterPool characterPool)
        {
            _characterPool = characterPool;
        }

        public void Destroy(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CharacterComponent>().End();
            var pool = world.GetPool<CharacterComponent>();
            foreach (var entity in filter)
            {
                var component = pool.Get(entity);
                _characterPool.Return(component.Value);
                
                world.DelEntity(entity);
            }
        }
    }
}