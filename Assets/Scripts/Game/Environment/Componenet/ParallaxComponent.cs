using UnityEngine;

namespace Game
{
    public readonly struct ParallaxComponent : IParallax
    {
        private readonly IParallax _value;

        public Transform Transform => _value.Transform;
        
        public float Factor => _value.Factor;

        private ParallaxComponent(IParallax value)
        {
            _value = value;
        }

        public static ParallaxComponent Create(IParallax parallax) => new (parallax);

    }
}