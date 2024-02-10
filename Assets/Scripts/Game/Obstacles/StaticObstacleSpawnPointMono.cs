using UnityEngine;

namespace Game
{
    public class StaticObstacleSpawnPointMono : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        
#if UNITY_EDITOR
        [SerializeField] private Color _color = Color.magenta;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(Position, 0.5f);
        }
#endif
    }
}