using Cysharp.Threading.Tasks;

namespace Game
{
    public interface IHUDFactory
    {
        UniTask<HUD[]> Create();
    }
}