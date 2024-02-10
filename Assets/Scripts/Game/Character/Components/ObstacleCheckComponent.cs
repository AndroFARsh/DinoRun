using System;
using JetBrains.Annotations;

namespace Game
{
    readonly struct ObstacleCheckComponent 
    {
        public readonly IObstacleCheck RunObstacleCheck;
        public readonly IObstacleCheck CrouchObstacleCheck;

        ObstacleCheckComponent(IObstacleCheck runChecker, IObstacleCheck crouchChecker)
        {
            RunObstacleCheck = runChecker;
            CrouchObstacleCheck = crouchChecker;
        }

        public static ObstacleCheckComponent Create([NotNull] ICharacter value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return new ObstacleCheckComponent(value.RunObstacleCheck, value.CrouchObstacleCheck);
        }
    }
}