namespace Minty.Services.Logs.Models;

public readonly record struct LogMessage(string Title, string Message, LogSeverity Severity = LogSeverity.Informational)
{
	public string Title { get; } = Title;
	public string Message { get; } = Message;
	public LogSeverity Severity { get; } = Severity;

	/// <summary>
	/// Parser for the infobar severity
	/// </summary>
	public InfoBarSeverity InfoBarSeverity
	{
		get
		{
			if (Severity == LogSeverity.Debug)
				return InfoBarSeverity.Informational;
			return (InfoBarSeverity)(int)Severity;
		}
	}

	public Guid Id { get; } = Guid.NewGuid();
}
