namespace Minty.Dialogs.ViewModels;

using Base;

[RegisterTransient]
public sealed class SuccessDialogViewModel : BaseDialogViewModel
{
	public SuccessDialogViewModel()
	{
		Message = "Success";
	}
}
