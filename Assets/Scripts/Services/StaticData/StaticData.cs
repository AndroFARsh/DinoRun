using System;
using Game;
using Infrastructure;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Services.StaticData
{
    [Serializable]
    public class LimitRange<T>
    {
        public int Limit = int.MaxValue;
        public T Min;
        public T Max;
    }
    
    [Serializable]
    public class LimitAllowed<T>
    {
        public int Limit = int.MaxValue;
        public T[] Values;
    }
    
    [CreateAssetMenu(fileName = "StaticData", menuName = "ScriptableObjects/Game Static Data")]
    public class StaticData : ScriptableObject
    {
        [SerializeField] private float _cameraMoveSpeed = 1;
        
        [SerializeField] private float _gravity = -100;
        [SerializeField] private float _maxVelocity = 20f;
        [SerializeField] private float _jumpInitialVelocity = 30f;
        [SerializeField] private float _longJumpFactor = 1;
        [SerializeField] private float _longJumpDuration = 0.3f;
        [SerializeField] private AnimationCurve _speeds = AnimationCurve.EaseInOut(0, 0, 100, 30);
        [SerializeField] private LimitRange<float>[] _obstacleCooldown;
        [SerializeField] private LimitAllowed<ObstacleType>[] _obstacleType;
        [SerializeField] private Vector2 _movableObstacleSpeedRange = new (0, 1);
        
        public float MovableObstacleSpeed => Random.Range(_movableObstacleSpeedRange.x, _movableObstacleSpeedRange.y);
        
        public float CameraMoveSpeed => _cameraMoveSpeed;
        
        public float MaxVelocity => _maxVelocity;
        
        public float Gravity => _gravity;
        
        public float JumpInitialVelocity => _jumpInitialVelocity;
        
        public float LongJumpFactor => _longJumpFactor;
        
        public float LongJumpDuration => _longJumpDuration;
        
        public float GetCharacterSpeed(int score) => _speeds.Evaluate(score);
        
        public float GetObstacleCooldown(int score)
        {
            var index = 0;
            while (_obstacleCooldown.TryGet(index, out var cooldownRange) && score > cooldownRange.Limit)
            {
                index++;
            }
            
            index = index >= _obstacleCooldown.Length ? _obstacleCooldown.Length - 1 : index;
            
            return _obstacleCooldown.TryGet(index, out var v) 
                ? Random.Range(v.Min, v.Max) : 0f;
        }
        
        public ObstacleType GetObstacleType(int score)
        {
            var index = 0;
            while (_obstacleType.TryGet(index, out var obstacleType) && score > obstacleType.Limit)
            {
                index++;
            }

            index = index >= _obstacleType.Length ? _obstacleType.Length - 1 : index;
            
            return _obstacleType[index].Values.GetRandom();
        }
    }
}