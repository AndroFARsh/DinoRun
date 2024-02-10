using UnityEngine;

namespace Game
{
    public class ParallaxMono : MonoBehaviour, IParallax
    {
        [SerializeField] private float _parallaxFactor = 0.9f;

        public Transform Transform => transform;
        public float Factor => _parallaxFactor;
    }
}