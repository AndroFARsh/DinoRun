using UnityEngine;

namespace Game
{
    public class ObstacleMono : MonoBehaviour, IObstacle
    {
        [SerializeField] private ObstacleType _type;
        [SerializeField] private Bounds _bounds;
        
        public ObstacleType Type => _type;
        public GameObject GameObject => gameObject;
        public Transform Transform => transform;
        public Bounds Bounds => _bounds;
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(transform.localPosition + _bounds.center,  _bounds.size);
        }
#endif
    }
}