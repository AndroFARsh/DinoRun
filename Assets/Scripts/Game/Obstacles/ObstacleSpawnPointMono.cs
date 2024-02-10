using System;
using UnityEngine;

namespace Game
{
    public interface IObstacleSpawnPoint
    {
        void ApplyPosition(IObstacle obstacle);
    } 
    
    public class ObstacleSpawnPointMono : MonoBehaviour, IObstacleSpawnPoint
    {
        [SerializeField] private MovableObstacleSpawnPointMono _movableObstacle;
        [SerializeField] private StaticObstacleSpawnPointMono _staticObstacle;

        public void ApplyPosition(IObstacle obstacle)
        {
            switch (obstacle.Type)
            {
                case ObstacleType.StaticSmallOne:
                case ObstacleType.StaticSmallTwo:
                case ObstacleType.StaticLargeOne:
                case ObstacleType.StaticSmallThree:
                case ObstacleType.StaticLargeTwo:
                case ObstacleType.StaticLargeThree:
                    obstacle.Transform.position = _staticObstacle.Position;
                    break;
                case ObstacleType.Movable:
                    obstacle.Transform.position = _movableObstacle.Position;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}