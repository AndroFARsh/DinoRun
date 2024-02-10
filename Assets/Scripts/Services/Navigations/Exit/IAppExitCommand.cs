using System;
using Cysharp.Threading.Tasks;

namespace Services
{
    public interface IAppExitCommand
    {
        void Exit();

        public static Type RegisterType()
        {
            return
#if UNITY_EDITOR
                typeof(EditorAppExitCommand);
#elif UNITY_WEBGL
                typeof(WebGLAppExitCommand);
#else
                typeof(ProdAppExitCommand);
#endif
        }
    }
}