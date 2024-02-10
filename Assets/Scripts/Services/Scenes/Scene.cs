namespace Services
{
    public readonly struct Scene
    {
        public static readonly Scene UnknownScene = default;
        public static readonly Scene LoaderScene = new("Loader.Scene");
        public static readonly Scene HubScene = new("Hub.Scene");
        public static readonly Scene GameScene = new("Game.Scene");

        public string Key { get; }

        private Scene(string sceneKey)
        {
            Key = sceneKey;
        }

        public override int GetHashCode() => Key.GetHashCode();

        public override bool Equals(object obj) => obj is Scene scene && scene.Key.Equals(Key);

        public override string ToString() => Key;
        
        public static implicit operator string(Scene v) => v.Key;
    }
}