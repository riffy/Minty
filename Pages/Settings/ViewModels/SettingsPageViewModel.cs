namespace Minty.Pages.Settings.ViewModels;



[RegisterSingleton]
public sealed partial class SettingsPageViewModel : ViewModelBase
{
	private readonly LogController _logController;
	private readonly AppConfigController _appConfigController;
	private readonly RepositoryController _repositoryController;

	public SettingsPageViewModel(LogController lc, AppConfigController acc, RepositoryController rc)
	{
		_logController = lc;
		_appConfigController = acc;
		_repositoryController = rc;

		_selectedTheme = _appConfigController.Config.Theme;

		_repositoryDescriptor =
			string.IsNullOrEmpty(_appConfigController.Config.RepositoryPath) ?
				Resources.Setting_Repository_Not_Selected :
				_appConfigController.Config.RepositoryPath;
		_repositoryIsSelected = !string.IsNullOrEmpty(_appConfigController.Config.RepositoryPath);

		acc.Config.PropertyChanged += ConfigOnPropertyChanged;
	}

	/// <summary>
	/// Handles changes in the configuration properties and executes necessary logic
	/// based on the property changes.
	/// </summary>
	/// <param name="sender">The source of the property change event.</param>
	/// <param name="e">The event data containing details about the property change.</param>
	private void ConfigOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName != nameof(AppConfig.RepositoryPath)) return;
		ParseRepositoryConfig();
	}

	#region THEME
	/// <summary>
	/// List of available themes
	/// </summary>
	public string[] AppThemes { get;  } = [
		ThemeHelper.THEME_SYSTEM_ID,
		ThemeHelper.THEME_LIGHT_ID,
		ThemeHelper.THEME_DARK_ID];

	/// <summary>
	/// Current selected theme Id (string)
	/// </summary>
	[ObservableProperty]
	private string _selectedTheme;

	/// <summary>
	/// If the theme id changes, applies the corresponding theme to the config (saves it)
	/// </summary>
	partial void OnSelectedThemeChanged(string value)
	{
		if (_appConfigController.Config.Theme == value) return;
		_appConfigController.Config.Theme = value;
		_appConfigController.SaveConfigToFile();
	}
	#endregion

	#region REPOSITORY

	[ObservableProperty]
	private string _repositoryDescriptor;

	[ObservableProperty]
	private bool _repositoryIsSelected;

	/// <summary>
	/// Updates the repository descriptor based on the current repository path from the application configuration.
	/// Assigns a default placeholder if the repository path is not selected and adjusts the repository selection status.
	/// </summary>
	private void ParseRepositoryConfig()
	{
		RepositoryDescriptor =
			string.IsNullOrEmpty(_appConfigController.Config.RepositoryPath) ?
				Resources.Setting_Repository_Not_Selected :
				_appConfigController.Config.RepositoryPath;
		RepositoryIsSelected = !string.IsNullOrEmpty(_appConfigController.Config.RepositoryPath);
	}

	/// <summary>
	/// Displays a repository selection dialog, allowing the user to choose a repository folder.
	/// Updates the repository path upon selection and logs any exceptions that occur during the process.
	/// </summary>
	/// <returns>A task that represents the asynchronous operation of handling the repository selection dialog.</returns>
	[RelayCommand]
	private async Task ShowRepositorySelectDialog()
	{
		try
		{
			var toplevel = TopLevel.GetTopLevel(App.MainWindow);
			if (toplevel is null)
				throw new NullReferenceException("Failed to retrieve top level");
			var result = await toplevel.StorageProvider.OpenFolderPickerAsync(new()
			{
				AllowMultiple = false,
				Title = "Repository Folder Selection"
			});
			if (result.Count <= 0)
				return;

			await _repositoryController.SetNewRepository(result[0].Path.AbsolutePath);
		}
		catch (Exception ex)
		{
			_logController.Exception(ex);
		}
	}

	/// <summary>
	/// Opens the repository folder using the system's file explorer if a valid repository
	/// is selected and the folder exists.
	/// </summary>
	[RelayCommand]
	private void OpenRepositoryFolder()
	{
		if (!RepositoryIsSelected ||
		    RepositoryDescriptor == Resources.Setting_Repository_Not_Selected ||
		    !Directory.Exists(RepositoryDescriptor)) return;
		ExplorerHelper.OpenFolder(RepositoryDescriptor);
	}

	/// <summary>
	/// Clears the repository path in the application configuration
	/// and saves the updated configuration to a file asynchronously.
	/// </summary>
	/// <returns>Returns a task that represents the asynchronous operation.</returns>
	[RelayCommand]
	private async Task ClearRepositoryPath()
	{
		_appConfigController.Config.RepositoryPath = null;
		await _appConfigController.SaveConfigToFile();
	}

	#endregion

	#region APP DATA

	/// <summary>
	/// Opens the app-data directory
	/// </summary>
	[RelayCommand]
	public void OpenAppDataFolder()
	{
		try
		{
			ExplorerHelper.OpenFolder(AppDataService.AppDataDirectory);
		}
		catch (Exception ex)
		{
			_logController.Exception(ex);
		}
	}

	#endregion

}
