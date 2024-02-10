using System;
using JetBrains.Annotations;

namespace Game
{
    readonly struct GroundCheckComponent 
    {
        public readonly IGroundCheck Value;

        GroundCheckComponent(IGroundCheck value)
        {
            Value = value;
        }

        public static GroundCheckComponent Create([NotNull] ICharacter value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return new GroundCheckComponent(value.GroundCheck);
        }
    }
}