namespace Minty.Dialogs.ViewModels;

[RegisterTransient]
public sealed class ExceptionDialogViewModel : BaseDialogViewModel
{
	public Exception? Exception { get; set; }
	public bool ShowReportButton { get; set; } = true;
	public string ExceptionDetails => Exception?.ToString() ?? string.Empty;
}
