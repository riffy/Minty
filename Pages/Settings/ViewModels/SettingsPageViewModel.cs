namespace Minty.Pages.Settings.ViewModels;

[RegisterSingleton]
public sealed partial class SettingsPageViewModel(LogController logController,
	AppConfigController appConfigController) : ViewModelBase
{
	#region THEME
	/// <summary>
	/// Current selected theme Id (string)
	/// </summary>
	[ObservableProperty]
	private string _selectedTheme = appConfigController.Config.Theme;

	/// <summary>
	/// List of available themes
	/// </summary>
	public string[] AppThemes { get;  } = [
		ThemeHelper.THEME_SYSTEM_ID,
		ThemeHelper.THEME_LIGHT_ID,
		ThemeHelper.THEME_DARK_ID];

	/// <summary>
	/// If the theme id changes, applies the corresponding theme to the config (saves it)
	/// </summary>
	partial void OnSelectedThemeChanged(string value)
	{
		if (appConfigController.Config.Theme == value) return;
		appConfigController.Config.Theme = value;
		appConfigController.SaveConfigToFile();
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
			logController.Exception(ex);
		}
	}

	#endregion
}
