namespace Minty.Screens.Splash.ViewModels;

[RegisterSingleton]
public sealed partial class SplashScreenViewModel : ViewModelBase
{
	private readonly AppDataService _appDataService;
	private readonly LogController _logController;
	private readonly AppConfigController _appConfigController;
	private readonly IServiceProvider _serviceProvider;
	private readonly MainWindowController _mainWindowController;

	public SplashScreenViewModel(IServiceProvider sp, LogController lc,
		AppDataService ads, AppConfigController acc, MainWindowController mwc)
	{
		_serviceProvider = sp;
		_appDataService = ads;
		_logController = lc;
		_appConfigController = acc;
		_mainWindowController = mwc;
		_loadQueue.Enqueue(InitializeApp);
		_loadQueue.Enqueue(InitializeLog);
		_loadQueue.Enqueue(InitializeConfig);
		_loadQueue.Enqueue(WaitForMainWindow);
		TriggerNextLoadStep();
	}

	#region LOAD QUEUE
	private readonly Queue<Func<Task<bool>>> _loadQueue = new();

	/// <summary>
	/// Triggers the next entry in the load queue.
	/// If the queue is empty, routes the HomePage.
	/// </summary>
	private async Task TriggerNextLoadStep()
	{
		if (_loadQueue.Count > 0)
			try
			{
				if (await _loadQueue.Dequeue().Invoke())
					TriggerNextLoadStep();
				else
					return;
			}
			catch (Exception ex)
			{
				DisplayInfoBar(
					Resources.Error,
					$"Error while trying to ensure AppData-Directory:\n{ex.Message}",
					InfoBarSeverity.Error);
			}
		else
			_serviceProvider
				.GetRequiredService<MainWindowViewModel>()
				.NavigateTo(typeof(MainScreenViewModel));

	}
	#endregion

	#region LOAD STEPS

	/// <summary>
	/// Initializes the app directory.
	/// </summary>
	private async Task<bool> InitializeApp()
	{
		try
		{
			FormatLoadingText("App Data");
			return _appDataService.EnsureAppDataDirectory() ?
				true :
				throw new("AppData-Directory could not be ensured");
		}
		catch (Exception ex)
		{
			DisplayInfoBar(
				Resources.Error,
				$"Error while initializing App:\n{ex.Message}",
				InfoBarSeverity.Error);
			return false;
		}
	}

	/// <summary>
	/// Initialize log file
	/// </summary>
	private async Task<bool> InitializeLog()
	{
		try
		{
			FormatLoadingText("Logger");
			if (!await _logController.EnsureLogFile())
				throw new("Log-File could not be ensured");
			_logController.Info("Logger initialized");
			return true;
		}
		catch (Exception ex)
		{
			DisplayInfoBar(
				Resources.Error,
				$"Error while initializing Log-File:\n{ex.Message}",
				InfoBarSeverity.Error);
			return false;
		}
	}

	/// <summary>
	/// Initialize database and apply migrations.
	/// </summary>
	private async Task<bool> InitializeConfig()
	{
		try
		{
			FormatLoadingText("Configuration");
			return await _appConfigController.InitializeConfig() ?
				true :
				throw new("Config could not be initialized");
		}
		catch (Exception ex)
		{
			_logController.Exception(ex);
			DisplayInfoBar(
				Resources.Error,
				$"Error while initializing Database:\n{ex.Message}",
				InfoBarSeverity.Error);
			return false;
		}
	}

	/// <summary>
	/// Waits for the application's main window to be initialized and fully loaded.
	/// </summary>
	/// <returns>
	/// A task that resolves to true once the main window is initialized and loaded.
	/// </returns>
	private async Task<bool> WaitForMainWindow()
	{
		// Wait until App.MainWindow is not null && App.MainWindow.IsLoaded
		while (App.MainWindow is null || !App.MainWindow.IsLoaded)
			await Task.Delay(500);
		return await _mainWindowController.Initialize();
	}
	#endregion

	#region LOADING TEXT
	[ObservableProperty]
	private string _loadingText = "";

	private static readonly CompositeFormat _loadingTextComposite =
		CompositeFormat.Parse(Resources.Splash_Initializing);

	/// <summary>
	/// Updates the loading text displayed on the splash screen with the name of the module being initialized.
	/// </summary>
	/// <param name="module">The name of the module currently being initialized.</param>
	private void FormatLoadingText(string module) =>
		LoadingText = string.Format(null, _loadingTextComposite, module);

	#endregion

	#region INFO BAR
	[ObservableProperty]
	private bool _showInfoBar;
	[ObservableProperty]
	private string _infoBarTitle = "";
	[ObservableProperty]
	private string _infoBarText = "";
	[ObservableProperty]
	private InfoBarSeverity _infoBarSeverity = InfoBarSeverity.Informational;

	/// <summary>
	/// Shows the info bar with the given data.
	/// </summary>
	/// <param name="title">The title of the info</param>
	/// <param name="text">The text of the info</param>
	/// <param name="severity">The severity of the info</param>
	private void DisplayInfoBar(string title, string text, InfoBarSeverity severity)
	{
		ShowInfoBar = true;
		InfoBarTitle = title;
		InfoBarText = text;
		InfoBarSeverity = severity;
	}
	#endregion

	#region INFINITE PROGRESSBAR
	[ObservableProperty]
	private bool _showInfiniteProgress = true;
	#endregion

	#region VALUE PROGRESSBAR
	[ObservableProperty]
	private bool _showValueProgress;
	[ObservableProperty]
	private double _valueProgress;
	[ObservableProperty]
	private double _valueProgressMin;
	[ObservableProperty]
	private double _valueProgressMax = 100;
	#endregion

}
