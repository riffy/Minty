namespace Minty.Pages.Debug.ViewModels;

[RegisterSingleton]
public sealed partial class DebugDialogsPageViewModel(DialogController dialogController) : ViewModelBase
{
	[RelayCommand]
	private async Task ShowErrorDialogTestAsync()
		=> await dialogController.ShowErrorDialogAsync("Error", "This is an error");

	[RelayCommand]
	private async Task ShowWarningDialogTestAsync()
		=> await dialogController.ShowWarningDialogAsync("Warning", "This is an warning");

	[RelayCommand]
	private async Task ShowInfoDialogTestAsync()
		=> await dialogController.ShowInfoDialogAsync("Info", "This is an info");

	[RelayCommand]
	private async Task ShowSuccessDialogTestAsync()
		=> await dialogController.ShowSuccessDialogAsync("Success", "This is an success");

	[RelayCommand]

	private async Task ShowExceptionDialogTestAsync()
	{
		try
		{
			throw new InvalidOperationException("Das ist eine Beispiel absturz nachricht");
		}
		catch (Exception ex)
		{
			await dialogController.ShowExceptionDialogAsync("Fehler 1", "Beim irgendwas tun kaputt gegangen", ex, showReportButton: true);
		}
	}

	[RelayCommand]
	private async Task ShowExceptionDialogWithoutReportTestAsync()
	{
		try
		{
			throw new InvalidOperationException("Das ist eine Beispiel absturz nachricht ohne report funktion");
		}
		catch (Exception ex)
		{
			await dialogController.ShowExceptionDialogAsync("Fehler 2", "Beim irgendwas tun kaputt gegangen nur ohne report", ex, showReportButton: false);
		}
	}
}
