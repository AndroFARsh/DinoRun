using Cysharp.Threading.Tasks;
using Infrastructure;
using Services;

namespace Projects
{
    public class TeardownState : ISimpleState
    {
        private readonly ICurtain _curtain;
        private readonly IAppExitCommand _appExitCommand;

        public TeardownState(ICurtain curtain, IAppExitCommand appExitCommand)
        {
            _curtain = curtain;
            _appExitCommand = appExitCommand;
        }

        public async UniTask Enter()
        {
            await _curtain.Show();
            _appExitCommand.Exit();
        } 
    }
}