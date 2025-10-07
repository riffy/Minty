namespace Minty.Screens.Main.ViewModels;

[RegisterSingleton]
public sealed partial class MainScreenViewModel : ViewModelBase
{
	private readonly IServiceProvider _serviceProvider;
	private readonly LogController _logController;

	public MainScreenViewModel(IServiceProvider sP, LogController lC)
	{
		_logController = lC;
		_serviceProvider = sP;
		CurrentPage = _serviceProvider.GetRequiredService<HomePageViewModel>();
		LoadNavigationItems();
	}

	#region NAVIGATION
	[ObservableProperty]
	private ViewModelBase? _currentPage;

	[ObservableProperty]
	private ObservableCollection<NavigationCategoryBase> _navigationCategories = [];

	private void LoadNavigationItems()
	{
		NavigationCategories.Clear();
		NavigationCategories.Add(new NavigationCategory
		{
			Name = "Startseite",
			Icon = Symbol.Home,
			Target = typeof(HomePageViewModel)
		});
	}

	public void NavigateTo(Type viewModelBase)
	{
		try
		{
			_logController.Debug($"Screen navigation to {viewModelBase.Name}");
			CurrentPage = (ViewModelBase)_serviceProvider.GetRequiredService(viewModelBase);
		}
		catch (Exception ex)
		{
			_logController.Exception(ex);
		}
	}
	#endregion
}
