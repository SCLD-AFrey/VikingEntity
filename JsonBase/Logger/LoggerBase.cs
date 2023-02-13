using System.Runtime.CompilerServices;

namespace JsonBase.Logger;

public class LoggerBase : JsonBase<LogEntry>, ILoggerBase
{
    public LoggerBase()
    {
        
    }
    
    public void AddEntry(LogEntry p_logEntry)
    {
        Add(p_logEntry);
        Commit();
    }
    public void AddEntry(string p_message)
    {
        Add(new LogEntry(p_message, LogEntry.Severity.Info));
        Commit();
    }
    public void AddEntry(string p_message, LogEntry.Severity p_logLevel)
    {
        Add(new LogEntry(p_message, p_logLevel));
        Commit();
    }
}