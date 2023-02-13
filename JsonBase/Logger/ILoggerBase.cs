namespace JsonBase.Logger;

public interface ILoggerBase
{
    public void AddEntry(LogEntry p_logEntry);
    public void AddEntry(string p_message);
    public void AddEntry(string p_message, LogEntry.Severity p_logLevel);
}