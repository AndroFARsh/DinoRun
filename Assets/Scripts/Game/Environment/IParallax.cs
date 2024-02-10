using UnityEngine;

namespace Game
{
    public interface IParallax
    {
        Transform Transform { get; }
        float Factor { get; }
    }
}