namespace Minty.Dialogs.ViewModels;

using Base;

[RegisterTransient]
public sealed class WarningDialogViewModel : BaseDialogViewModel
{
	public WarningDialogViewModel()
	{
		Message = "Warning";
	}
}
