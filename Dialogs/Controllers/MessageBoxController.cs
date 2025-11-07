namespace Minty.Dialogs.Controllers;

/// <summary>
/// Provides functionality for displaying various types of message boxes,
/// such as informational messages, warnings, errors, successes, and exceptions.
/// </summary>
[RegisterSingleton]
public sealed class MessageBoxController(IServiceProvider serviceProvider, LogController logController)
{
	#region INFO / WARN / SUCCESS / ERROR

	/// <summary>
	/// Displays an error dialog with the specified message.
	/// </summary>
	/// <param name="message">The error message to be displayed in the dialog.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	public async Task ShowErrorAsync(string message) =>
		await ShowMessageBoxAsync(
			Resources.Diag_Error_Title,
			CreateMessageBoxViewModel(message, InfoBarSeverity.Error),
			primaryButtonText: Resources.Okay);

	/// <summary>
	/// Displays a success dialog with the specified message.
	/// </summary>
	/// <param name="message">The success message to be displayed in the dialog.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	public async Task ShowSuccessAsync(string message) =>
		await ShowMessageBoxAsync(
			Resources.Diag_Success_Title,
			CreateMessageBoxViewModel(message, InfoBarSeverity.Success),
			primaryButtonText: Resources.Okay);


	/// <summary>
	/// Displays a warning dialog with the specified message.
	/// </summary>
	/// <param name="message">The warning message to be displayed in the dialog.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	public async Task ShowWarningAsync(string message) =>
		await ShowMessageBoxAsync(
			Resources.Diag_Warn_Title,
			CreateMessageBoxViewModel(message, InfoBarSeverity.Warning),
			primaryButtonText: Resources.Okay);


	/// <summary>
	/// Displays an informational message dialog with the specified message.
	/// </summary>
	/// <param name="message">The informational message to be displayed in the dialog.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	public async Task ShowInfoAsync(string message) =>
		await ShowMessageBoxAsync(
			Resources.Diag_Info_Title,
			CreateMessageBoxViewModel(message, InfoBarSeverity.Informational),
			primaryButtonText: Resources.Okay);

	#endregion

	#region EXCEPTION

	/// <summary>
	/// Displays an exception dialog with the specified exception details.
	/// </summary>
	/// <param name="ex">The exception to be displayed in the dialog.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	public async Task ShowExceptionAsync(Exception ex) =>
		await ShowExceptionAsync(ex, string.Empty);

	/// <summary>
	/// Creates an exception dialog with the provided parameters.
	/// </summary>
	public async Task ShowExceptionAsync(Exception ex, string additionalMessage)
	{
		if (App.MainWindow is null)
		{
			logController.Error("MainWindow is null. Dialog could not be shown.");
			return;
		}

		var message = Resources.Diag_Exception_Description;
		if (!string.IsNullOrEmpty(additionalMessage))
			message += $": \n{additionalMessage}";
		var viewModel = new ExceptionBoxViewModel
		{
			Message = message,
			Exception = ex
		};
		var content = new ExceptionDialog { DataContext = viewModel };
		var dialog = new ContentDialog
		{
			Title = Resources.Diag_Exception_Title,
			Content = content,
			PrimaryButtonText = Resources.Okay,
			DefaultButton = ContentDialogButton.Primary,
			SecondaryButtonText = Resources.Report,
			IsSecondaryButtonEnabled = true
		};

		var result = await dialog.ShowAsync(App.MainWindow);

		if (result == ContentDialogResult.Secondary)
			GitHubHelper.NewIssue.OpenGithubIssueReport(ex, message);
	}

	#endregion

	#region SPAWN DIALOG

	/// <summary>
	/// Shows a content dialog with the provided parameters.
	/// </summary>
	private async Task<ContentDialogResult> ShowMessageBoxAsync(
		string title, MessageBoxViewModel viewModel,
		string primaryButtonText, string? secondaryButtonText = null)
	{
		if (App.MainWindow is null)
		{
			logController.Error("MainWindow is null. Dialog could not be shown.");
			return ContentDialogResult.None;
		}

		var content = new MessageBox { DataContext = viewModel };
		var dialog = new ContentDialog
		{
			Title = title,
			Content = content,
			PrimaryButtonText = primaryButtonText,
			DefaultButton = ContentDialogButton.Primary,
			SecondaryButtonText = secondaryButtonText,
			IsSecondaryButtonEnabled = !string.IsNullOrEmpty(secondaryButtonText)
		};

		return await dialog.ShowAsync(App.MainWindow);
	}

	/// <summary>
	/// Creates and initializes a MessageBoxViewModel with the specified message and severity level.
	/// </summary>
	/// <param name="message">The message to be displayed in the message box.</param>
	/// <param name="severity">The severity level of the message, indicating its importance or type (e.g., error, warning, informational).</param>
	/// <returns>An instance of <see cref="MessageBoxViewModel"/> populated with the specified parameters.</returns>
	private MessageBoxViewModel CreateMessageBoxViewModel(string message, InfoBarSeverity severity)
	{
		var vm = ActivatorUtilities.CreateInstance<MessageBoxViewModel>(serviceProvider);
		vm.Message = message;
		vm.Severity = severity;
		return vm;
	}
	#endregion
}
