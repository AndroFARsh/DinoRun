using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Services
{
    public class CurtainMono : MonoBehaviour, ICurtain
    {
        [SerializeField] [Tooltip("How fast should the texture be Faded In?")]
        private float _fadeInDuration = 5.0f;
        [SerializeField] private AnimationCurve _fadeInCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        [SerializeField] [Tooltip("How fast should the texture be Faded Out?")]
        private float _fadeOutDuration = 5.0f;
        [SerializeField] private AnimationCurve _fadeOutCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

        [SerializeField] [Tooltip("Choose the color, which will fill the screen.")]
        private Color _fadeColor = new Color(0, 0, 0, 1.0f);

        private Texture2D _texture;
        private bool _hideInProgress;
        private bool _showInProgress;
        private bool _shown;

        private void Start()
        {
            Debug.Log("Test");
        }

        public UniTask Initialize()
        {
            _texture = new Texture2D(1, 1);
            _texture.SetPixel(0, 0, new Color(_fadeColor.r, _fadeColor.g, _fadeColor.b, 1));
            _texture.Apply();

            DontDestroyOnLoad(gameObject);
            return UniTask.CompletedTask;
        }
        
        public async UniTask Show()
        {
            if (_showInProgress || _shown) return;
            
            _showInProgress = true;
            _shown = true;
            
            var time = 0.0f;
            while (time < _fadeInDuration)
            {
                time += Time.deltaTime;
                var alpha = _fadeInCurve.Evaluate(time / _fadeInDuration);
                if (_texture)
                {
                    _texture.SetPixel(0, 0, new Color(_fadeColor.r, _fadeColor.g, _fadeColor.b, alpha));
                    _texture.Apply();
                }

                await UniTask.NextFrame();
            }
            
            _showInProgress = false;
        }

        public async UniTask Hide()
        {
            if (_hideInProgress || !_shown) return;

            _hideInProgress = true;
            
            var time = 0.0f;
            while (time < _fadeOutDuration)
            {
                time += Time.deltaTime;
                var alpha = _fadeOutCurve.Evaluate(time / _fadeOutDuration);
                _texture.SetPixel(0, 0, new Color(_fadeColor.r, _fadeColor.g, _fadeColor.b, alpha));
                _texture.Apply();
                
                await UniTask.NextFrame();
            }
            
            _shown = false;
            _hideInProgress = false;
        }

        public void OnGUI()
        {
            if (_shown)
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _texture);
            }
        }
    }
}