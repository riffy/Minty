namespace Minty.Windows.Main.ViewModels;

[RegisterSingleton]
public sealed partial class MainWindowViewModel : ViewModelBase
{
	private readonly IServiceProvider _serviceProvider;
	private readonly LogController _logController;
	private readonly AppConfigController _appConfigController;

	public MainWindowViewModel(IServiceProvider sP, LogController lC, AppConfigController acc)
	{
		_logController = lC;
		_serviceProvider = sP;
		_appConfigController = acc;
		CurrentPage = _serviceProvider.GetRequiredService<SplashScreenViewModel>();
		_appConfigController.Config.PropertyChanged += ApplyTheme;
	}

	/// <summary>
	/// Applies the theme based on the AppConfig's Theme property value.
	/// </summary>
	/// <param name="sender">The source of the event. Typically, this is the object that raises the event.</param>
	/// <param name="e">The event data containing details about the property change.</param>
	private void ApplyTheme(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName != nameof(AppConfig.Theme)) return;
		if (App.MainWindow is null) return;
		App.MainWindow.RequestedThemeVariant = ThemeHelper.GetThemeVariant(_appConfigController.Config.Theme);
	}

	#region NAVIGATION
	[ObservableProperty]
	private ViewModelBase? _currentPage;

	public void NavigateTo(Type viewModelBase)
	{
		try
		{
			_logController.Debug($"MainWindow navigation to {viewModelBase.Name}");
			CurrentPage = (ViewModelBase)_serviceProvider.GetRequiredService(viewModelBase);
		}
		catch (Exception ex)
		{
			_logController.Exception(ex);
		}
	}
	#endregion
}
