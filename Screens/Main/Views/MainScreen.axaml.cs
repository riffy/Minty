namespace Minty.Screens.Main.Views;

public sealed partial class MainScreen : UserControl
{
	public MainScreen()
	{
		InitializeComponent();
	}

	private async void NavigationPanel_OnSelectionChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
	{
		if (DataContext is not MainScreenViewModel viewModel) return;

		switch (e.SelectedItem)
		{
			case NavigationViewItem { Tag: NavigationCategory cat }:
				// viewModel.NavigateTo(cat);
				Console.WriteLine($"NavigationViewItem: {cat.Name}");
				break;
			case NavigationCategory category:
				if (category.Target is not null)
					viewModel.NavigateTo(category.Target);
				break;
			case NavigationViewItem {Tag: "Settings" or "Einstellungen"}:
				viewModel.NavigateTo(typeof(SettingsPageViewModel));
				break;
		}
	}
}

