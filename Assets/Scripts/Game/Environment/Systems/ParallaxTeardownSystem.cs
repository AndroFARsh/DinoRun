using Leopotam.EcsLite;

namespace Game
{
    public class ParallaxTeardownSystem : IEcsDestroySystem
    {
        public void Destroy(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            foreach (var entity in world.Filter<ParallaxComponent>().End())
            {
                world.DelEntity(entity);
            }
        }
    }
}