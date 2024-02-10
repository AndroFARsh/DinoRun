using UnityEngine;

namespace Game
{
    interface ICharacterSpawnPoint
    {
        Vector3 Position { get; }
    }
    
    public class CharacterSpawnPointMono : MonoBehaviour, ICharacterSpawnPoint
    {
        public Vector3 Position => transform.position;

#if UNITY_EDITOR
        [SerializeField] private Color _color = Color.blue;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(Position, 0.5f);
        }
#endif
    }
}