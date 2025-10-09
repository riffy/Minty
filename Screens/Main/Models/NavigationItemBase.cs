namespace Minty.Screens.Main.Models;

/// <summary>
/// Represents the base class for navigation items, encapsulating the core properties
/// and behaviors shared across different navigation elements within the application.
/// </summary>
public abstract class NavigationItemBase : NavigationBase
{
	private string _name = string.Empty;
	private bool _isEnabled = true;

	/// <summary>
	/// The name displayed in the navigation bar.
	/// </summary>
	public required string Name
	{
		get => _name;
		set => SetField(ref _name, value);
	}

	/// <summary>
	/// If the navigation item is enabled.
	/// </summary>
	public bool IsEnabled
	{
		get => _isEnabled;
		set => SetField(ref _isEnabled, value);
	}
}
