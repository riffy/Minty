namespace Minty.Services.App.Data;

/// <summary>
/// Provides configuration data and settings for application-level JSON serialization.
/// </summary>
public static class AppConfigData
{
	/// <summary>
	/// Options to be set when reading / writing the config json.
	/// </summary>
	public static readonly JsonSerializerOptions JsonSerializerOptions = new()
	{
		PropertyNameCaseInsensitive = true,
		IncludeFields = true,
		NumberHandling = JsonNumberHandling.AllowReadingFromString,
		WriteIndented = true,
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
	};
}
