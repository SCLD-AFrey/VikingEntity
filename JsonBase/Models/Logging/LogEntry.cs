namespace JsonBase.Models.Logging;

public class LogEntry : JsonItem
{
    public string Message { get; init; } = "";
    public Severity LogSeverity { get; init; } = Severity.Info;

    public override string ToString()
    {
        return $"{DateCreated} {LogSeverity, -10} {Message}";
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