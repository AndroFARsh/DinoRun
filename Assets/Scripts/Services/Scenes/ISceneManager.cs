using Cysharp.Threading.Tasks;

namespace Services
{
    public interface ISceneManager
    {
        UniTask LoadScene(Scene scene);
        
        UniTask LoadSubScene(Scene scene);
    }
}