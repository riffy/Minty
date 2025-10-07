namespace Minty.Helpers;

public static class ExplorerHelper
{
	/// <summary>
	/// Tries to open the given folder path using the OS explorer.
	/// </summary>
	public static void OpenFolder(string folderPath)
	{
		if (string.IsNullOrEmpty(folderPath))
			return;

		if (OperatingSystem.IsWindows())
			Process.Start("explorer.exe", folderPath);
		else if (OperatingSystem.IsMacOS())
			Process.Start("open", folderPath);
		else if (OperatingSystem.IsLinux())
			Process.Start("xdg-open", folderPath);
		else
			try
			{
				// Use .NET Core 3.0+ approach
				using Process process = new();
				process.StartInfo = new()
				{
					FileName = folderPath,
					UseShellExecute = true
				};
				process.Start();
			}
			catch
			{
				// Last resort fallback
				var url = new Uri(folderPath).AbsoluteUri;
				Process.Start(new ProcessStartInfo
				{
					FileName = url,
					UseShellExecute = true
				});
			}
	}
}
