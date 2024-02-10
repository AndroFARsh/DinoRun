using UnityEngine;

namespace Game
{
    public interface ITile
    {
        public Bounds Bounds { get; }
        public Transform Transform { get; }
    }
    
    public class TileMono : MonoBehaviour, ITile
    {
        [SerializeField] private Bounds _bounds;

        public Bounds Bounds => _bounds;
        public Transform Transform => transform;
        
#if UNITY_EDITOR
        [SerializeField] private Color _debugColor = Color.green;
        
        private void OnDrawGizmosSelected() => DrawGizmos(_debugColor);
        
        public void DrawGizmos(Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawWireCube(transform.localPosition + _bounds.center,  _bounds.size);
        }
#endif
    }
}