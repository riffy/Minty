namespace Minty.Screens.Main.Models;

public sealed class NavigationCategory : NavigationItemBase
{
	public required RepositoryCategory Category { get; init; }
	public Symbol Icon { get; set; }
	public ObservableCollection<NavigationItemBase> Children { get; set; } = [];
}
