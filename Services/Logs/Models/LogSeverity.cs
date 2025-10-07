namespace Minty.Services.Logs.Models;

/// <summary>
/// Severity of the log message
/// </summary>
public enum LogSeverity : byte
{
	Informational = 0,
	Success = 1,
	Warning = 2,
	Error = 3,
	Debug = 4
}
