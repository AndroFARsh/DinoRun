using System.Linq;
using UnityEngine;

namespace Game
{
    public readonly struct InitDataFlipTileComponent
    {
        public readonly Vector3[] Value;

        InitDataFlipTileComponent(Vector3[] value)
        {
            Value = value;
        }

        public static InitDataFlipTileComponent Create(IFlipTile value) =>
            new(value.Tiles.Select(tile => tile.Transform.position).ToArray());
    }
}