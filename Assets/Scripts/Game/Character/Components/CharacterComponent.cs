using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Game
{
    public readonly struct CharacterComponent 
    {
        public readonly GameObject Value;

        CharacterComponent(GameObject value)
        {
            Value = value;
        }

        public static CharacterComponent Create([NotNull] ICharacter value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return new CharacterComponent(value.GameObject);
        }
    }
}