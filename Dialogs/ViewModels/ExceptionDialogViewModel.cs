namespace Minty.Dialogs.ViewModels;

using Base;

[RegisterTransient]
public sealed class ExceptionDialogViewModel : BaseDialogViewModel
{
	public ExceptionDialogViewModel()
	{
		Message = "Exception";
	}

	public Exception? Exception { get; set; }
	public bool ShowReportButton { get; set; } = true;
	public string ExceptionDetails => Exception?.ToString() ?? string.Empty;
}
