using System;
using JetBrains.Annotations;

namespace Game
{
    readonly struct AnimatorComponent 
    {
        public readonly CharacterAnimatorMono Value;
        AnimatorComponent(CharacterAnimatorMono value)
        {
            Value = value;
        }

        public static implicit operator CharacterAnimatorMono(AnimatorComponent c) => c.Value;
        public static AnimatorComponent Create([NotNull] ICharacter value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return new AnimatorComponent(value.AnimatorMono);
        }
    }
}