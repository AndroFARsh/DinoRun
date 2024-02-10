namespace Game
{
    public readonly struct FlipTileComponent : IFlipTile
    {
        private readonly IFlipTile _value;

        public ITile[] Tiles => _value.Tiles;

        FlipTileComponent(IFlipTile value)
        {
            _value = value;
        }

        public static FlipTileComponent Create(IFlipTile value) => new (value);
    }
}