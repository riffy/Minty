namespace Minty.Services.Dialogs.Interfaces;

public interface IDialogController
{
	Task ShowErrorDialogAsync(string title, string message);

	Task ShowSuccessDialogAsync(string title, string message);

	Task ShowWarningDialogAsync(string title, string message);

	Task ShowInfoDialogAsync(string title, string message);

	Task ShowExceptionDialogAsync(string title, string message, Exception ex, bool showReportButton = true);
}
