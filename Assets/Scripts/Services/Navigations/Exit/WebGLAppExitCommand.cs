using System;
using UnityEngine;
using VContainer;

namespace Services
{
    public class WebGLAppExitCommand : IAppExitCommand
    {
        private readonly IObjectResolver _resolver;

        public WebGLAppExitCommand(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        [Obsolete("Obsolete")]
        public void Exit()
        {
            _resolver.Dispose();
            Application.ExternalEval("window.open('about:blank','_self')");
            //Application.OpenURL("about:blank");
        }
    }
}