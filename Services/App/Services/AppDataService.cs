namespace Minty.Services.App.Services;

[RegisterSingleton]
public sealed class AppDataService
{
	/// <summary>
	/// The path to the folder of the %appdata% directory for Taggle
	/// </summary>
	public static string AppDataDirectory { get; } =
		Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppDomain.CurrentDomain.FriendlyName);

	#region DIRECTORY
	/// <summary>
	/// Checks if the app data directory exists, if not tries to create it.
	/// </summary>
	/// <returns>
	/// <c>True</c> - If the app directory exists or was created
	/// <c>False</c> - On error
	/// </returns>
	public bool EnsureAppDataDirectory() =>
		Directory.Exists(AppDataDirectory) || Directory.CreateDirectory(AppDataDirectory).Exists;

	#endregion
}
