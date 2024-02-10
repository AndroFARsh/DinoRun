using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public interface ICharacter
    {
        GameObject GameObject { get; }
        Rigidbody2D Rigidbody { get; }
        CharacterAnimatorMono AnimatorMono { get; }
        IGroundCheck GroundCheck { get; }
        IObstacleCheck RunObstacleCheck { get; }
        IObstacleCheck CrouchObstacleCheck { get; }
    }
    
    public class CharacterMono : MonoBehaviour, ICharacter
    {
        [FormerlySerializedAs("_animator")] [SerializeField] private CharacterAnimatorMono animatorMono;
        [SerializeField] private GroundCheckMono _groundCheck;
        [SerializeField] private ObstacleCheckMono _runCollider;
        [SerializeField] private ObstacleCheckMono _crouchCollider;
        [SerializeField] private Rigidbody2D _rigidbody; 

        public GameObject GameObject => gameObject;
        
        public Rigidbody2D Rigidbody => _rigidbody;
        
        public CharacterAnimatorMono AnimatorMono => animatorMono;

        public IGroundCheck GroundCheck => _groundCheck;
        public IObstacleCheck RunObstacleCheck => _runCollider;
        public IObstacleCheck CrouchObstacleCheck => _crouchCollider;
    }
}