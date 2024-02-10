using Infrastructure.Utils;
using UnityEngine;

namespace Game
{
    
    public class GroundFlipTileMono : MonoBehaviour, IFlipTile
    {
        [SerializeField] private TileMono[] _tiles;
        
        ITile[] IFlipTile.Tiles => _tiles;

#if UNITY_EDITOR
        public void OnDrawGizmosSelected()
        {
            foreach (var tile in _tiles)
            {
                if (tile)
                {
                    tile.DrawGizmos(GizmosUtils.RandomColor());
                }
            }
        }
#endif
    }
}