using Cysharp.Threading.Tasks;
using Infrastructure;
using Services;
using SceneManager = UnityEngine.SceneManagement.SceneManager;


namespace Projects
{
    public class BootstrapState : ISimpleState
    {
        private const string BootstrapScene = "Bootstrap";
        
        public BootstrapState()
        {}

        public UniTask Enter()
        {
            var activeScene = SceneManager.GetActiveScene();
            if (activeScene.name != BootstrapScene)
            {
                SceneManager.LoadScene(BootstrapScene);
            }
            return UniTask.CompletedTask;
        } 
    }
}