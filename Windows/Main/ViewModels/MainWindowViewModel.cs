namespace Minty.Windows.Main.ViewModels;

[RegisterSingleton]
public sealed partial class MainWindowViewModel : ViewModelBase
{
	[ObservableProperty]
	private ViewModelBase? _currentPage;

	public MainWindowViewModel(IServiceProvider sP)
	{
		CurrentPage = sP.GetRequiredService<HomePageViewModel>();
		LoadNavigationItems();
	}

	#region NAVIGATION
	[ObservableProperty]
	private ObservableCollection<NavigationCategoryBase> _navigationCategories = [];

	private void LoadNavigationItems()
	{
		NavigationCategories.Clear();
		NavigationCategories.Add(new NavigationCategory
		{
			Name = "Startseite", Icon = Symbol.Home
		});
	}
	#endregion
}
