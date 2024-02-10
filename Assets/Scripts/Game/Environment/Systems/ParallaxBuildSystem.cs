using System.Collections.Generic;
using Leopotam.EcsLite;

namespace Game
{
    public class ParallaxBuildSystem : IEcsInitSystem
    {
        private readonly IEnumerable<IParallaxes> _parallaxes;

        ParallaxBuildSystem(IEnumerable<IParallaxes> parallaxes)
        {
            _parallaxes = parallaxes;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var parallaxPool = world.GetPool<ParallaxComponent>();
            foreach (var parallax in _parallaxes)
            {
                foreach (var p in parallax.Parallaxes)
                {
                    parallaxPool.Add(world.NewEntity()) = ParallaxComponent.Create(p);    
                }
            }
        }
    }
}