namespace Minty.Dialogs.NewCategory.ViewModels;

[RegisterTransient]
public sealed partial class NewCategoryDialogViewModel(LogController logController, ContentDialog dialog) : ViewModelBase
{
	[ObservableProperty]
	private string _folderName = string.Empty;
	[ObservableProperty]
	private string _displayName = string.Empty;

}
