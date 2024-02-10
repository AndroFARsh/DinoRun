using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Game
{
    readonly struct RigidbodyComponent
    {
        public readonly Rigidbody2D Value;

        private RigidbodyComponent(Rigidbody2D value)
        {
            Value = value;
        }
        
        public static implicit operator Rigidbody2D(RigidbodyComponent c) => c.Value;
        public static RigidbodyComponent Create([NotNull] ICharacter value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return new RigidbodyComponent(value.Rigidbody);
        }
    }
}