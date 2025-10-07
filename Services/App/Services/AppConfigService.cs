namespace Minty.Services.App.Services;

[RegisterSingleton]
public sealed class AppConfigService(LogController logController)
{
	private readonly string _configPath = Path.Combine(AppDataService.AppDataDirectory, "config.json");

	#region DIRECTORY / FILE

	/// <summary>
	/// Saves the current local config to the file in %appdata%
	/// </summary>
	public async Task<bool> SaveConfigToFile(AppConfig config)
	{
		try
		{
			await File.WriteAllTextAsync(_configPath, JsonSerializer.Serialize(config, AppConfigData.JsonSerializerOptions));
			return true;
		}
		catch (Exception ex)
		{
			logController.Exception(ex);
			return false;
		}
	}

	/// <summary>
	/// Loads the local config file from %appdata% and returns it.
	/// Returns null on error.
	/// </summary>
	public async Task<AppConfig?> LoadConfigFromFile()
	{
		try
		{
			if (!File.Exists(_configPath)) return null;

			var configJson = await File.ReadAllTextAsync(_configPath);
			var config = JsonSerializer.Deserialize<AppConfig>(configJson, AppConfigData.JsonSerializerOptions);
			return config;
		}
		catch (Exception ex)
		{
			logController.Exception(ex);
			return null;
		}
	}
	#endregion
}
