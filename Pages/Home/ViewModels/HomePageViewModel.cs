namespace Minty.Pages.Home.ViewModels;

[RegisterSingleton]
public sealed partial class HomePageViewModel : ViewModelBase
{
	public readonly NavigationItem NavigationItem = new()
	{
		Name = "Startseite",
		Icon = Symbol.Home,
		Target = typeof(HomePageViewModel)
	};
}
