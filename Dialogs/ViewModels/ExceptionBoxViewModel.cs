namespace Minty.Dialogs.ViewModels;

[RegisterTransient]
public sealed class ExceptionBoxViewModel : BaseMessageBox
{
	public Exception? Exception { get; init; }
	public string ExceptionDetails => Exception?.ToString() ?? string.Empty;
}
