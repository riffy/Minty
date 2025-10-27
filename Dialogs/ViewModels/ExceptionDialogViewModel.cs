namespace Minty.Dialogs.ViewModels;

[RegisterTransient]
public sealed class ExceptionDialogViewModel : BaseDialogViewModel
{
	public Exception? Exception { get; init; }
	public string ExceptionDetails => Exception?.ToString() ?? string.Empty;
}
