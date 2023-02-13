namespace JsonBase.Logger;

public class LogEntry
{
    private int Oid { get; set; }
    public DateTime DateCreated { get; } = DateTime.UtcNow;
    public string Message { get; set; }
    public Severity LogLevel { get; } = Severity.Info;

    public LogEntry(string p_message)
    {
        Message = p_message;
        LogLevel = Severity.Info;
    }
    public LogEntry(string p_message, Severity p_logLevel)
    {
        Message = p_message;
        LogLevel = p_logLevel;
    }

    public override string ToString()
    {
        return $"";
    }
    
    public enum Severity
    {
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }
}