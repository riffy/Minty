namespace Minty.Pages.Repositories.ViewModels;

[RegisterSingleton]
public sealed partial class RepositoryPageViewModel : ViewModelBase
{
	private readonly CategoryDialogController _categoryDialogController;

	public RepositoryPageViewModel(CategoryDialogController categoryDialogController,
		RepositoryController repositoryController)
	{
		_categoryDialogController = categoryDialogController;
		RepositoryPath = repositoryController.Repository?.RootPath ?? "No path set ...";
	}

	[ObservableProperty]
	private string _repositoryPath = string.Empty;

	[RelayCommand]
	private async Task ShowNewCategoryDialog() =>
		_categoryDialogController.ShowNewCategoryDialogAsync();
}
