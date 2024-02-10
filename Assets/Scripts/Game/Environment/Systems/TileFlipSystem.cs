using CodeBase.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class TileFlipSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly Camera _camera;
        private EcsFilter _flipFilter;
        
        private EcsPool<FlipTileComponent> _dataPool;
        private EcsPool<MiddleTileIndexComponent> _activePool;
        
        TileFlipSystem(Camera camera)
        {
            _camera = camera;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            _dataPool = world.GetPool<FlipTileComponent>();
            _activePool = world.GetPool<MiddleTileIndexComponent>();
            
            _flipFilter = world.Filter<FlipTileComponent>().Inc<MiddleTileIndexComponent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _flipFilter)
            {
                ref var dataComponent = ref _dataPool.Get(entity);
                ref var indexComponent = ref _activePool.Get(entity);

                var tile = dataComponent.Tiles[CircleIndex(indexComponent.Value-1, dataComponent.Tiles.Length)];
                var cameraBounds = _camera.OrthographicBounds();
                var tileBounds = new Bounds(center: tile.Transform.localPosition + tile.Bounds.center, size: tile.Bounds.size);

                if (!cameraBounds.Intersects(tileBounds))
                {
                    // move first ot the end 
                    tile.Transform.localPosition += new Vector3{ x = 3 * tile.Bounds.size.x };
                    indexComponent.Value += 1;
                }
            }
        }
        
        private static int CircleIndex(int index, int length)
        {
            index %= length;
            if (index < 0)
            {
                index = length - index;
            }

            return index;
        }
    }
}