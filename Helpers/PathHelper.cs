namespace Minty.Helpers;

public static class PathHelper
{
	/// <summary>
	/// Path to the temporary directory of the app
	/// </summary>
	public static readonly string AppTempPath =
		Path.Combine(Path.GetTempPath(), AppDomain.CurrentDomain.FriendlyName);
}
