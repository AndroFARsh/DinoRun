using UnityEngine;

namespace Game
{
    public class CameraOffsetMono : MonoBehaviour, IOffsetPosition
    {
        [SerializeField] private Vector2 _offset = Vector2.zero;

        public Vector2 Offset => _offset;
    }
}