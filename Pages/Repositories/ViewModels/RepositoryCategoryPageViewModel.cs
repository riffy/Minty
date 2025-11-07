namespace Minty.Pages.Repositories.ViewModels;

[RegisterTransient]
public sealed partial class RepositoryCategoryPageViewModel(RepositoryCategory category) : ViewModelBase
{
	public RepositoryCategory Category => category;
}
