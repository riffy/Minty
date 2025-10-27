namespace Minty.Screens.Main.ViewModels;

[RegisterSingleton]
public sealed partial class MainScreenViewModel : ViewModelBase
{
	private readonly IServiceProvider _serviceProvider;
	private readonly LogController _logController;
	private readonly RepositoryController _repositoryController;
	private readonly HomePageViewModel _homePageViewModel;

	public MainScreenViewModel(IServiceProvider sP, LogController lC, RepositoryController rc,
		HomePageViewModel hpvm, RepositoryEvents re)
	{
		_logController = lC;
		_serviceProvider = sP;
		_repositoryController = rc;
		_homePageViewModel = hpvm;
		CurrentPage = hpvm;
		LoadNavigationItems();
		re.OnNewRepository += async (_) => LoadNavigationItems();
	}

	#region NAVIGATION
	[ObservableProperty]
	private ViewModelBase? _currentPage;

	[ObservableProperty]
	private ObservableCollection<NavigationBase> _navigationCategories = [];

	private static readonly CompositeFormat _categoryHeaderComposite =
		CompositeFormat.Parse(Resources.Nav_Categories);

	/// <summary>
	/// Initializes and populates the collection of navigation items and categories
	/// for the main screen. This method configures the navigation structure by
	/// clearing any existing entries and adding the default navigation items,
	/// including the home page, repository-related items, and a debug entry for icons
	/// in development mode. It also adds a separator and a header for unselected
	/// repository scenarios.
	/// </summary>
	private void LoadNavigationItems()
	{
		NavigationCategories.Clear();
		NavigationCategories.Add(_homePageViewModel.NavigationItem);
		NavigationCategories.Add(_repositoryController.NavigationItem);
		#if DEBUG
		NavigationCategories.Add(new NavigationItem
		{
			Name = "Icons",
			Icon = Symbol.Home,
			Target = typeof(DevIconPageViewModel)
		});
		#endif

		// Seperator + List of categories
		NavigationCategories.Add(new NavigationSeperator());
		if (_repositoryController.Repository is null)
			NavigationCategories.Add(new NavigationHeader
			{
				Name = Resources.Setting_Repository_Not_Selected
			});
		else
			NavigationCategories.Add(new NavigationHeader
			{
				Name = string.Format(null, _categoryHeaderComposite, _repositoryController.Repository.Categories.Count)
			});
	}

	/// <summary>
	/// Navigates to a specified view model by resolving it from the service provider
	/// and sets it as the current page. This method logs the navigation attempt and
	/// handles exceptions that may occur during the resolution process.
	/// </summary>
	/// <param name="viewModelBase">
	/// The type of the target view model to navigate to. This should be a subclass
	/// of <see cref="ViewModelBase"/>.
	/// </param>
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
