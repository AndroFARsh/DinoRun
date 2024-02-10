using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Services
{
    public class ProdAppExitCommand : IAppExitCommand
    {
        private readonly IObjectResolver _resolver;

        public ProdAppExitCommand(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public void Exit()
        {
            _resolver.Dispose();
            Application.Quit();
        }
    }
}