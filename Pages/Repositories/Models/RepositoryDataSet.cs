namespace Minty.Pages.Repositories.Models;

public sealed class RepositoryDataSet
{
	public required string Name { get; init; }
	public required string Schema { get; init; }
	public required bool IsSingle { get; init; }

	public NavigationItem GetNavigationItem()
	{
		return new()
		{
			Name = Name,
			Icon = IsSingle ? Symbol.Document : Symbol.Folder,
			Target = typeof(HomePageViewModel),
			IsEnabled = true
		};
	}
}
