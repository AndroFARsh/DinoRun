namespace Services
{
    public readonly struct Music
    {
        public static readonly Music Unknown = default;
        public static readonly Music GameBackgroundMusic = new("Music/GameBackgroundMusic.mp3");
        public static readonly Music HubBackgroundMusic = new("Music/HubBackgroundMusic.mp3");

        public string Key { get; }

        private Music(string musicClipKey)
        {
            Key = musicClipKey;
        }

        public override int GetHashCode() => Key.GetHashCode();

        public override bool Equals(object obj) => obj is Music music && music.Key.Equals(Key);

        public override string ToString() => Key;

        public static implicit operator string(Music v) => v.Key;
    }
}