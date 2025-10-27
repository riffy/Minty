namespace Minty.Pages.Home.ViewModels;

[RegisterSingleton]
public sealed partial class HomePageViewModel : ViewModelBase
{
	#region NAVIGATION

	/// <summary>
	/// Represents a specific navigation item used in the application for navigating
	/// to the repository page.
	/// </summary>
	public readonly NavigationItem NavigationItem = new()
	{
		Name = Resources.Page_Home_Title,
		Icon = Symbol.HomeFilled,
		Target = typeof(HomePageViewModel),
		IsEnabled = true
	};

	#endregion
}
