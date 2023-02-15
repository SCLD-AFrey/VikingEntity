namespace JsonBase.Models.Logging;

public class LoggerBase : JsonBase<LogEntry>, ILoggerBase
{
    public void Log(string p_message, LogEntry.Severity p_severity = LogEntry.Severity.Info, int p_userOid = 0)
    {
        try
        {
            LogEntry logEntry = new()
            {
                Message = p_message,
                LogSeverity = p_severity,
                Oid = GetNextOid()
            };
            this.Add(logEntry);
            Commit();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

}