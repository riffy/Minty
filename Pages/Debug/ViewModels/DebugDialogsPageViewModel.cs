namespace Minty.Pages.Debug.ViewModels;

[RegisterSingleton]
public sealed partial class DebugDialogsPageViewModel(MessageBoxController messageBoxController) : ViewModelBase
{
	[RelayCommand]
	private async Task ShowErrorDialogTestAsync()
		=> await messageBoxController.ShowErrorAsync("This is an error");

	[RelayCommand]
	private async Task ShowWarningDialogTestAsync()
		=> await messageBoxController.ShowWarningAsync("This is a warning");

	[RelayCommand]
	private async Task ShowInfoDialogTestAsync()
		=> await messageBoxController.ShowInfoAsync("This is an info");

	[RelayCommand]
	private async Task ShowSuccessDialogTestAsync()
		=> await messageBoxController.ShowSuccessAsync("This is a success");

	[RelayCommand]
	private async Task ShowExceptionDialogTestAsync()
	{
		try
		{
			throw new InvalidOperationException("Das ist eine Beispiel absturz nachricht");
		}
		catch (Exception ex)
		{
			await messageBoxController.ShowExceptionAsync(ex);
		}
	}
}
