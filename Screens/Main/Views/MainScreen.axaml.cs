namespace Minty.Screens.Main.Views;

public sealed partial class MainScreen : UserControl
{
	public MainScreen()
	{
		InitializeComponent();
	}

	/// <summary>
	/// Handles the event triggered when the selection in the navigation panel changes.
	/// Based on the selected item, it directs navigation to the appropriate target view model or page.
	/// </summary>
	/// <param name="sender">The source of the event, typically the navigation panel control.</param>
	/// <param name="e">The event data containing details of the selection change, including the selected item.</param>
	private async void NavigationPanel_OnSelectionChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
	{
		if (DataContext is not MainScreenViewModel viewModel) return;

		switch (e.SelectedItem)
		{
			case NavigationItem item:
				viewModel.NavigateTo(item.Target);
				break;
			case NavigationViewItem {Tag: "Settings" or "Einstellungen"}:
				viewModel.NavigateTo(typeof(SettingsPageViewModel));
				break;
			case NavigationCategory navCategory:
				viewModel.NavigateToCategory(navCategory.Category);
				break;
		}
	}
}

