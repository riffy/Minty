namespace Minty.Services.Dialogs.Controllers;

using ViewModels;
using Views;
using App = Minty.App;

[RegisterSingleton]
public sealed class DialogController
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
	public async Task ShowErrorDialogAsync(string title, string message)
	{
		var viewModel = new ErrorDialogViewModel() { Message = message };
		var content = new ErrorDialog() { DataContext = viewModel };
		await ShowContentDialogAsync(title, content, primaryButtonText: "Okay");
	}

	/// <summary>
	/// Creates a success dialog with the provided parameters.
	/// </summary>
	public async Task ShowSuccessDialogAsync(string title, string message)
	{
		var viewModel = new SuccessDialogViewModel() { Message = message };
		var content = new SuccessDialog() { DataContext = viewModel };
		await ShowContentDialogAsync(title, content, primaryButtonText: "Okay");
	}

	/// <summary>
	/// Creates a warning dialog with the provided parameters.
	/// </summary>
	public async Task ShowWarningDialogAsync(string title, string message)
	{
		var viewModel = new WarningDialogViewModel() { Message = message };
		var content = new WarningDialog() { DataContext = viewModel };
		await ShowContentDialogAsync(title, content, primaryButtonText: "Okay");
	}

	/// <summary>
	/// Creates an info dialog with the provided parameters.
	/// </summary>
	public async Task ShowInfoDialogAsync(string title, string message)
	{
		var viewModel = new InfoDialogViewModel() { Message = message };
		var content = new InfoDialog() { DataContext = viewModel };
		await ShowContentDialogAsync(title, content, primaryButtonText: "Okay");
	}

	/// <summary>
	/// Creates an exception dialog with the provided parameters.
	/// </summary>
	public async Task ShowExceptionDialogAsync(string title, string message, Exception ex, bool showReportButton = true)
	{
		var viewModel = new ExceptionDialogViewModel()
		{
			Message = message,
			Exception = ex,
			ShowReportButton = showReportButton
		};

		var content = new ExceptionDialog()
		{
			DataContext = viewModel
		};

		var result = await ShowContentDialogAsync(
			title,
			content,
			primaryButtonText: "Okay",
			secondaryButtonText: showReportButton ? "Report" : null
			);

		if(result == ContentDialogResult.Secondary && showReportButton)
			OpenGithubIssues(ex, message);
	}

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
	private void OpenGithubIssues(Exception? exception = null, string? message = null)
	{
		try
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = GitHubHelper.NewIssue.CreateExceptionIssue(exception, message),
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
