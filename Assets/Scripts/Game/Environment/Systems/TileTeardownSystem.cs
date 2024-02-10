using Leopotam.EcsLite;

namespace Game
{
    public class TileTeardownSystem : IEcsDestroySystem
    {
        public void Destroy(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            var dataPool = world.GetPool<FlipTileComponent>();
            var initPool = world.GetPool<InitDataFlipTileComponent>();
            var filter = world.Filter<FlipTileComponent>().Inc<InitDataFlipTileComponent>().End();
            foreach (var entity in filter)
            {
                ref var dataComponent = ref dataPool.Get(entity);
                ref var initComponent = ref initPool.Get(entity);
                
                for (var i = 0; i < dataComponent.Tiles.Length; i++)
                {
                    dataComponent.Tiles[i].Transform.localPosition = initComponent.Value[i];
                }
                world.DelEntity(entity);
            }
        }
    }
}