namespace Minty.Screens.Main.Models;

public sealed class NavigationCategory : NavigationCategoryBase
{
	public required string Name { get; set; }
	public required Symbol Icon { get; set; }
	public Type? Target { get; init; }
}
