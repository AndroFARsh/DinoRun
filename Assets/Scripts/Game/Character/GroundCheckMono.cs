using System;
using UnityEngine;

namespace Game
{
    public readonly struct GroundHit
    {
        public readonly float Distance;
        public readonly Vector2 Direction;
        public readonly Vector2 Normal;
        public readonly Vector2 Point;

        GroundHit(
            float distance,
            Vector2 direction,
            Vector2 normal,
            Vector2 point
        )
        {
            Distance = distance;
            Direction = direction;
            Normal = normal;
            Point = point;
        }

        public static GroundHit FromRaycastHit(RaycastHit2D hit, Vector2 direction) => new (hit.distance, direction, hit.normal, hit.point);
    }

    public interface IGroundCheck
    {
        GroundHit CheckGround();
    }
    
    public class GroundCheckMono : MonoBehaviour, IGroundCheck
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Vector2 _direction = Vector2.down;
        [SerializeField] private float _distance = float.MaxValue;
        
        public GroundHit CheckGround()
        {
            var hit = Physics2D.Raycast(transform.position, _direction, _distance, _layerMask);
            return GroundHit.FromRaycastHit(hit, _direction);
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            var hit = CheckGround();
            Gizmos.color = hit.Distance < 0.05f ? Color.red : Color.green;
            Gizmos.DrawRay(transform.position, hit.Direction * hit.Distance);
        }
#endif
    }
}