using Infrastructure;
using UnityEngine;

namespace Game
{
    public class MovableObstacleSpawnPointMono : MonoBehaviour
    {
        [SerializeField] private Vector2[] _offsets;

        public Vector3 Position
        {
            get
            {
                if (_offsets.TryRandom(out var offset))
                {
                    return transform.position + new Vector3(offset.x, offset.y, 0);
                }

                return transform.position;
            }
        }

#if UNITY_EDITOR
        [SerializeField] private Color _color = Color.cyan;

        private void OnDrawGizmosSelected()
        {
            if (_offsets is not { Length: > 0 }) return;
            
            Gizmos.color = _color;
            foreach (var offset in _offsets)
            {
                Gizmos.DrawSphere(transform.position + (Vector3)offset, 0.5f);
            }
        }
#endif

    }
}