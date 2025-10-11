namespace Minty.Pages.Home.ViewModels;

using Services.Dialogs.Controllers;

[RegisterSingleton]
public sealed partial class HomePageViewModel : ViewModelBase
{
	private readonly DialogController _dialogController;

	public HomePageViewModel(DialogController dc)
	{
		_dialogController = dc;
	}

	[RelayCommand]
	private async Task ShowErrorDialogTestAsync()
		=> await _dialogController.ShowErrorDialogAsync("Error", "This is an error");

	[RelayCommand]
	private async Task ShowWarningDialogTestAsync()
		=> await _dialogController.ShowWarningDialogAsync("Warning", "This is an warning");

	[RelayCommand]
	private async Task ShowInfoDialogTestAsync()
		=> await _dialogController.ShowInfoDialogAsync("Info", "This is an info");

	[RelayCommand]
	private async Task ShowSuccessDialogTestAsync()
		=> await _dialogController.ShowSuccessDialogAsync("Success", "This is an success");

	[RelayCommand]

	private async Task ShowExceptionDialogTestAsync()
	{
		try
		{
			throw new InvalidOperationException("Das ist eine Beispiel absturz nachricht");
		}
		catch (Exception ex)
		{
			await _dialogController.ShowExceptionDialogAsync("Fehler 1", "Beim irgendwas tun kaputt gegangen", ex, showReportButton: true);
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
			await _dialogController.ShowExceptionDialogAsync("Fehler 2", "Beim irgendwas tun kaputt gegangen nur ohne report", ex, showReportButton: false);
		}
	}
}
