using System.Collections.Generic;
using Leopotam.EcsLite;

namespace Game
{
    public class TileBuildSystem : IEcsInitSystem
    {
        private readonly IEnumerable<IFlipTile> _flipTiles;
        
        TileBuildSystem(IEnumerable<IFlipTile> flipTiles)
        {
            _flipTiles = flipTiles;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var dataPool = world.GetPool<FlipTileComponent>();
            var initPool = world.GetPool<InitDataFlipTileComponent>();
            var indexPool = world.GetPool<MiddleTileIndexComponent>();
            foreach (var flipTile in _flipTiles)
            {
                var entity = world.NewEntity();
                dataPool.Add(entity) = FlipTileComponent.Create(flipTile);
                initPool.Add(entity) = InitDataFlipTileComponent.Create(flipTile);
                indexPool.Add(entity).Value = 1;
            }
        }
    }
}