namespace Minty.Dialogs.ViewModels;

using Base;

[RegisterTransient]
public sealed class ErrorDialogViewModel : BaseDialogViewModel
{
	public ErrorDialogViewModel()
	{
		Message = "Error";
	}
}
