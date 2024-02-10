using UnityEngine;

namespace Services
{
    public class UnityLoggerService : ILoggerService
    {
        public void Info(string message, params object[] args)
        {
            if (args == null || args.Length == 0)
            {
                Debug.Log(message);
            }
            else
            {
                Debug.LogFormat(message, args);
            }
        }

        public void Warn(string message, params object[] args)
        {
            if (args == null || args.Length == 0)
            {
                Debug.LogWarning(message);
            }
            else
            {
                Debug.LogWarningFormat(message, args);
            }
        }

        public void Error(string message, params object[] args)
        {
            if (args == null || args.Length == 0)
            {
                Debug.LogError(message);
            }
            else
            {
                Debug.LogErrorFormat(message, args);
            }
        }
    }
}