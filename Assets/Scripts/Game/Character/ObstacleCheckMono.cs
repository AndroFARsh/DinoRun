using System;
using Infrastructure.Utils;
using UnityEngine;

namespace Game
{
    public interface IObstacleCheck
    {
        bool IsOverlapped();
    }

    public class ObstacleCheckMono : MonoBehaviour, IObstacleCheck
    {
        enum ShapeType
        {
            Box,
            Circle,
            CapsuleVertical,
            CapsuleHorizontal
        }
    
        [Serializable]
        class Shape
        {
            public ShapeType Type;
        
            public Vector2 Offset;
            public Vector2 Size;
        }
        
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Shape[] _shapes;

        private Collider2D _overlapped;

        public bool IsOverlapped() => _overlapped != null;

        private void FixedUpdate()
        {
            _overlapped = CheckOverlapping();
        }

        public Collider2D CheckOverlapping()
        {
            var position = transform.position;
            foreach (var shape in _shapes)
            {
                var shapePosition = (Vector2)position + shape.Offset;
                var overlapped = shape.Type switch
                {
                    ShapeType.Circle => Physics2D.OverlapCircle(shapePosition, shape.Size.x, _layerMask),
                    ShapeType.Box => Physics2D.OverlapBox(shapePosition, shape.Size, 0, _layerMask),
                    ShapeType.CapsuleVertical => Physics2D.OverlapCapsule(shapePosition, shape.Size, CapsuleDirection2D.Vertical, 0, _layerMask),
                    ShapeType.CapsuleHorizontal => Physics2D.OverlapCapsule(shapePosition, shape.Size, CapsuleDirection2D.Horizontal, 0,_layerMask),
                    _ => default
                };

                if (overlapped != null)
                {
                    return overlapped;
                }
            }
            return null;
        }
        
#if UNITY_EDITOR
        [SerializeField] private Color _hitColor = Color.red;
        [SerializeField] private Color _idleColor = Color.green;

        private void OnDrawGizmosSelected() 
        {
            if (_shapes == null || _shapes.Length == 0) return;
            var overlapped = _overlapped;
            var color = overlapped != null ? _hitColor : _idleColor;
            var position = transform.position;
            foreach (var shape in _shapes)
            {
                var shapePosition = position + (Vector3)shape.Offset;
                Switch.CreateLambdaSwitch<ShapeType>()
                    .Case(ShapeType.Circle, () => GizmosUtils.DrawCircle(shapePosition, shape.Size.x, color))
                    .Case(ShapeType.Box, () =>  GizmosUtils.DrawBox(shapePosition, shape.Size, color))
                    .Case(ShapeType.CapsuleVertical, () => GizmosUtils.DrawCapsuleVertical(shapePosition, shape.Size, color))
                    .Case(ShapeType.CapsuleHorizontal, () => GizmosUtils.DrawCapsuleHorizontal(shapePosition, shape.Size, color))
                    .Switch(shape.Type);
            }
            
            Switch.CreateTypeSwitch()
                    .Case<CircleCollider2D>((c) => GizmosUtils.DrawCircle(c.transform.position + (Vector3)c.offset, c.radius, color))
                    .Case<BoxCollider2D>((c) => GizmosUtils.DrawBox(c.transform.position + (Vector3)c.offset, c.size, color))
                    .Case<CapsuleCollider2D>((c) => GizmosUtils.DrawCapsule(c.transform.position + (Vector3)c.offset, c.size, c.direction, color))
                    .Default((obj) =>
                    {
                        if (obj is Collider2D c && c != null)
                        {
                            GizmosUtils.DrawMarker(c.transform.position + (Vector3)c.offset, 1f, color);
                        }
                    })
                    .Switch(overlapped);

        }
#endif
    }
}