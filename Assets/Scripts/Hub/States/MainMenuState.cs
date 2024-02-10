using Cysharp.Threading.Tasks;
using Infrastructure;
using MainMenu;
using Services;

namespace Hub
{
    public class MainMenuState : ISimpleState
    {
        private readonly ICurtain _curtain;
        private readonly IWindowService _windowService;

        MainMenuState(
            ICurtain curtain,
            IWindowService windowService
        )
        {
            _curtain = curtain;
            _windowService = windowService;
        }

        public async UniTask Enter()
        {
            await _curtain.Show();
            await _windowService.Push<MainMenuWindow>();
            await _curtain.Hide();
        }
    }
}