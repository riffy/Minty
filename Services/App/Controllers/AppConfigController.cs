namespace Minty.Services.App.Controllers;

[RegisterSingleton]
public sealed class AppConfigController(LogController logController, AppConfigService appConfigService)
{
	public readonly AppConfig Config = new();

	/// <summary>
	/// Saves the current configuration stored in the Config property
	/// to a file. Returns true if the operation succeeds, otherwise false.
	/// </summary>
	/// <returns>Returns a boolean indicating the success of the operation.</returns>
	public async Task<bool> SaveConfigToFile() =>
		await appConfigService.SaveConfigToFile(Config);

	/// <summary>
	/// Loads the local config file and saves into
	/// the Config property.
	/// If set and the config file doesn't exist, a new one is created.
	/// Returns true on success, else false
	/// </summary>
	/// <returns></returns>
	public async Task<bool> InitializeConfig()
	{
		try
		{
			var config = await appConfigService.LoadConfigFromFile();
			if (config is null)
				return await SaveConfigToFile();
			Config.Initialize(config);
			return true;
		}
		catch (Exception ex)
		{
			logController.Exception(ex);
			return false;
		}
	}
}
