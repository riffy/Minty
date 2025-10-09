namespace Minty.Pages.Repositories.ViewModels;

[RegisterSingleton]
public sealed partial class RepositoryPageViewModel : ViewModelBase
{
	[ObservableProperty]
	private string _repositoryPath = string.Empty;

	public RepositoryPageViewModel(RepositoryController repositoryController)
	{
		RepositoryPath = repositoryController.Repository?.RootPath ?? "No path set ...";
	}
}
