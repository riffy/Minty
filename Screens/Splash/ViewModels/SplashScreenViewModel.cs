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
				LoadingText = "";
				if (await _loadQueue.Dequeue().Invoke())
					TriggerNextLoadStep();
				else
					return;
			}
			catch (Exception ex)
			{
				DisplayInfoBar(
					"Critical Error",
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
			LoadingText = "Initializing AppData-Directory ...";
			if (_appDataService.EnsureAppDataDirectory()) return true;
			DisplayInfoBar(
				"Error",
				"Error while trying to ensure AppData-Directory",
				InfoBarSeverity.Error);
			return false;

		}
		catch (Exception ex)
		{
			DisplayInfoBar(
				"Critical Error",
				$"Critical Error while initializing App:\n{ex.Message}",
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
			LoadingText = "Initializing Logger ...";
			if (!await _logController.EnsureLogFile())
			{
				DisplayInfoBar(
					"Error",
					"Error while trying to ensure Log-File.",
					InfoBarSeverity.Error);
				return false;
			}
			_logController.Info("Log initialized");
			return true;
		}
		catch (Exception ex)
		{
			DisplayInfoBar(
				"Critical Error",
				$"Cirtial Error while initializing Log-File:\n{ex.Message}",
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
			LoadingText = "Loading Configuration ...";
			if (await _appConfigController.InitializeConfig()) return true;
			DisplayInfoBar(
				"Error",
				"Error while trying to initialize Configuration.",
				InfoBarSeverity.Error);
			return false;
		}
		catch (Exception ex)
		{
			_logController.Exception(ex);
			DisplayInfoBar(
				"Critical Error",
				$"Cirtial Error while initializing Database:\n{ex.Message}",
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
	#endregion

	#region INFO BAR
	[ObservableProperty]
	private bool _showInfoBar;
	[ObservableProperty]
	private string _infoBarTitle = "Yo";
	[ObservableProperty]
	private string _infoBarText = "Message";
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
