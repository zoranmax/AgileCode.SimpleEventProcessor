namespace AgileCode.SimpleEventProcessor.Logging
{
    public interface ILogger
    {
        void Debug(LogMessage message);
        void Info(LogMessage message);
        void Warn(LogMessage message);
        void Error(LogMessage message);
        void Fatal(LogMessage message);
    }
}