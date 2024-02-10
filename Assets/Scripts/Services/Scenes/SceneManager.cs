using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Services
{
    public class SceneManager : ISceneManager
    {
        
        public async UniTask LoadScene(Scene scene)
        {
            var handle = Addressables.LoadSceneAsync(scene.Key, LoadSceneMode.Single, false);
            await handle.ToUniTask();
            await handle.Result.ActivateAsync().ToUniTask();
        }
        
        public async UniTask LoadSubScene(Scene scene)
        {
            var handle = Addressables.LoadSceneAsync(scene, LoadSceneMode.Additive, false);
            await handle.ToUniTask();
        }
    }
}