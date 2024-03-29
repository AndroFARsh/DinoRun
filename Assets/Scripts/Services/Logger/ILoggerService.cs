namespace Services
{
    public interface ILoggerService
    {
        void Info(string message, params object[] args);
        
        void Warn(string message, params object[] args);
        
        void Error(string message, params object[] args);
    }
}