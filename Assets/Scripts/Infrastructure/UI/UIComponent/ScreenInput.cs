using UnityEngine;
using UnityEngine.InputSystem.OnScreen;

namespace UI
{
    public interface IScreenInput
    {
        void Perform();

        void Release();
    }
    
    [RequireComponent(typeof(OnScreenButton))]
    public abstract class BaseScreenInput : MonoBehaviour, IScreenInput
    {
        [SerializeField] private OnScreenButton _screenButton;
        
        private void Awake()
        {
            if (_screenButton == null)
            {
                _screenButton = GetComponent<OnScreenButton>();
            }
        }

        public void Perform() => _screenButton.OnPointerDown(null);

        public void Release() => _screenButton.OnPointerUp(null);
    }
}