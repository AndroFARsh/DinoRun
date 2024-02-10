namespace Services
{
    public readonly struct Effect
    {
        public static readonly Effect Unknown = default;
        public static readonly Effect Click = new("Fx/Click.mp3");
        public static readonly Effect Achieve = new("Fx/Achieve.mp3");
        public static readonly Effect Run = new("Fx/Run.mp3", true);
        public static readonly Effect Jump = new("Fx/Jump.mp3");
        
        public bool Loop { get; }
        
        public string Key { get; }

        private Effect(string effectClipKey, bool loop = false)
        {
            Key = effectClipKey;
            Loop = loop;
        }

        public override int GetHashCode() => Key.GetHashCode();

        public override bool Equals(object obj) => obj is Effect effect && effect.Key.Equals(Key);

        public override string ToString() => Key;

        public static implicit operator string(Effect v) => v.Key;
    }
}