using UnityEngine;

namespace Game
{
    public class CharacterAnimatorMono : MonoBehaviour
    {
        private static readonly int AnimationTypeKey = Animator.StringToHash("AnimationType");

        private enum AnimationType
        {
            Idle = 0,
            Run = 1,
            Jump = 2,
            Crouch = 3,
            Die = 4
        }

        [SerializeField] private Animator _animator;

        public void SetSpeed(float speed) => _animator.speed = speed;
        
        public void Idle() => SetAnimation(AnimationType.Idle);
        
        public void Run() => SetAnimation(AnimationType.Run);
        
        public void Crouch() => SetAnimation(AnimationType.Crouch);
        
        public void Jump() =>SetAnimation(AnimationType.Jump);
        
        public void Die() => SetAnimation(AnimationType.Die);

        private void SetAnimation(AnimationType newType)
        {
            if (_animator.GetInteger(AnimationTypeKey) != (int)newType)
            {
                _animator.SetInteger(AnimationTypeKey, (int)newType);
            }
        }
    }
}