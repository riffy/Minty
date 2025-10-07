namespace Minty.Helpers;

public static class ThemeHelper
{
	public const string THEME_SYSTEM_ID = "System";
	public const string THEME_DARK_ID = "Dark";
	public const string THEME_LIGHT_ID = "Light";

	/// <summary>
	/// For a given string / theme id, returns the corresponding
	/// theme variant. Returns default theme
	/// </summary>
	public static ThemeVariant GetThemeVariant(string value) =>
		value switch
		{
			THEME_LIGHT_ID => ThemeVariant.Light,
			THEME_DARK_ID => ThemeVariant.Dark,
			_ => ThemeVariant.Default
		};

	/// <summary>
	/// Returns the appropriate ThemeId for a given variant
	/// </summary>
	public static string GetThemeId(ThemeVariant value)
	{
		if (value == ThemeVariant.Light)
			return THEME_LIGHT_ID;
		return value == ThemeVariant.Dark ?
			THEME_DARK_ID : THEME_SYSTEM_ID;
	}
}
