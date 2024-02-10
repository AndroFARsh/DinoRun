using Infrastructure.Utils;
using UnityEngine;

namespace Game
{
    public class SkyFlipTileMono : MonoBehaviour, IFlipTile, IParallaxes
    {
        [SerializeField] private TileMono[] _tiles;
        [SerializeField] private ParallaxMono[] _parallaxes;
        
        ITile[] IFlipTile.Tiles => _tiles;
        IParallax[] IParallaxes.Parallaxes => _parallaxes;

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