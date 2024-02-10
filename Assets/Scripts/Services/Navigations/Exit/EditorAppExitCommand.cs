#if UNITY_EDITOR
using Cysharp.Threading.Tasks;
using VContainer;

namespace Services
{
    public class EditorAppExitCommand : IAppExitCommand
    {
        private readonly IObjectResolver _resolver;

        public EditorAppExitCommand(IObjectResolver resolver)
        {
            _resolver = resolver;
            UnityEditor.EditorApplication.playModeStateChanged += ModeChanged;
        }

        public void Exit()
        {
            _resolver.Dispose();
            UnityEditor.EditorApplication.isPlaying = false;
        }

        private void ModeChanged(UnityEditor.PlayModeStateChange state)
        {
            if (state == UnityEditor.PlayModeStateChange.ExitingPlayMode)
            {
                UnityEditor.EditorApplication.playModeStateChanged -= ModeChanged;
                Exit();
            }
        }
    }
}
#endif