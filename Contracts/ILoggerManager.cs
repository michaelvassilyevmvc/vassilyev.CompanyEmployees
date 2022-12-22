namespace Contracts
{
    public interface ILoggerManager
    {
        void LogWarn(string message);
        void LogError(string message);
        void LogInfo(string message);
        void LogDebug(string message);
    }
}
