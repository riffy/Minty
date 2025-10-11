namespace Minty.Dialogs.ViewModels;

using Base;

[RegisterTransient]
public sealed class InfoDialogViewModel : BaseDialogViewModel
{
	public InfoDialogViewModel()
	{
		Message = "Info";
	}
}
