namespace Minty.Pages.Settings.Data;

public static class SettingsData
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
