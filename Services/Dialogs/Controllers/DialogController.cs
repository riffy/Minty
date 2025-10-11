namespace Minty.Services.Dialogs.Controllers;

using Interfaces;
using App = Minty.App;

[RegisterTransient]
public sealed class DialogController : IDialogController
{
	private readonly LogController _logController;

	public DialogController(LogController lc)
	{
		_logController = lc;
	}

	#region CREATE DIALOGS

	/// <summary>
	/// Creates an error dialog with the provided parameters.
	/// </summary>
	public Task ShowErrorDialogAsync(string title, string message)
		=> throw new NotImplementedException();

	/// <summary>
	/// Creates a success dialog with the provided parameters.
	/// </summary>
	public Task ShowSuccessDialogAsync(string title, string message)
		=> throw new NotImplementedException();

	/// <summary>
	/// Creates a warning dialog with the provided parameters.
	/// </summary>
	public Task ShowWarningDialogAsync(string title, string message)
		=> throw new NotImplementedException();

	/// <summary>
	/// Creates an info dialog with the provided parameters.
	/// </summary>
	public Task ShowInfoDialogAsync(string title, string message)
		=> throw new NotImplementedException();

	/// <summary>
	/// Creates an exception dialog with the provided parameters.
	/// </summary>
	public Task ShowExceptionDialogAsync(string title, string message, Exception ex, bool showReportButton = true)
		=> throw new NotImplementedException();

	#endregion

	#region SPAWN DIALOG

	/// <summary>
	/// Shows a content dialog with the provided parameters.
	/// </summary>
	private async Task<ContentDialogResult> ShowContentDialogAsync(string title, Control content,
		string primaryButtonText, string? secondaryButtonText = null)
	{
		var mainWindow = GetMainWindow();
		if (mainWindow is null)
		{
			_logController.Debug("MainWindow is null. Dialog could not be shown.");
			return ContentDialogResult.None;
		}

		var dialog = new ContentDialog()
		{
			Title = title,
			Content = content,
			PrimaryButtonText = primaryButtonText,
			DefaultButton = ContentDialogButton.Primary,
			SecondaryButtonText = secondaryButtonText,
			IsSecondaryButtonEnabled = !string.IsNullOrEmpty(secondaryButtonText)
		};

		return await dialog.ShowAsync(mainWindow);
	}

	#endregion

	#region HELPERS

	/// <summary>
	/// Shorthand to get the main window.
	/// </summary>
	private Window? GetMainWindow()
		=> App.MainWindow;

	/// <summary>
	/// Start the GitHub issues page in the default browser.
	/// </summary>
	private void OpenGithubIssues()
	{
		try
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = "https://github.com/riffy/Minty/issues",
				UseShellExecute = true
			});
		}
		catch(Exception ex)
		{
			_logController.Exception(ex);
		}
	}

	#endregion
}
