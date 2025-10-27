namespace Minty.Screens.Main.Models;

/// <summary>
/// A simple navigation item that represents a view or destination to navigate to.
/// </summary>
public sealed class NavigationItem : NavigationItemBase
{
	/// <summary>
	/// Gets the target type associated with this navigation item.
	/// Represents the destination or view to navigate to when the item is selected.
	/// </summary>
	public required Type Target { get; init; }
	public required Symbol Icon { get; set; }
}
