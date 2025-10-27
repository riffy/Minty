namespace Minty.Pages.Debug.ViewModels;

[RegisterSingleton]
public sealed partial class DebugDialogsPageViewModel(DialogController dialogController) : ViewModelBase
{
	[RelayCommand]
	private async Task ShowErrorDialogTestAsync()
		=> await dialogController.ShowErrorDialogAsync("This is an error");

	[RelayCommand]
	private async Task ShowWarningDialogTestAsync()
		=> await dialogController.ShowWarningDialogAsync("This is a warning");

	[RelayCommand]
	private async Task ShowInfoDialogTestAsync()
		=> await dialogController.ShowInfoDialogAsync("This is an info");

	[RelayCommand]
	private async Task ShowSuccessDialogTestAsync()
		=> await dialogController.ShowSuccessDialogAsync("This is a success");

	[RelayCommand]
	private async Task ShowExceptionDialogTestAsync()
	{
		try
		{
			throw new InvalidOperationException("Das ist eine Beispiel absturz nachricht");
		}
		catch (Exception ex)
		{
			await dialogController.ShowExceptionDialogAsync(ex);
		}
	}
}
