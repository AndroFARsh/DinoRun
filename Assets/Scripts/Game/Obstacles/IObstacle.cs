using UnityEngine;

namespace Game
{
    public interface IObstacle
    {
        ObstacleType Type { get; }
        
        GameObject GameObject { get; }
        Transform Transform { get; }
        Bounds Bounds { get; }
    }
}