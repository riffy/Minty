namespace Minty.Windows.Main.Views;

public sealed partial class MainWindow : AppWindow, IDisposable
{
	private const int RESIZE_TIMEOUT = 500; // 500ms timeout
	private readonly System.Timers.Timer _updateTimer = new (RESIZE_TIMEOUT) { AutoReset = false };

	public MainWindow()
	{
		InitializeComponent();
		PositionChanged += OnPositionChanged;
		SizeChanged += OnSizeChanged;
		_updateTimer.Elapsed += (_, _) => Dispatcher.UIThread.Post(OnPositionSizeChangeComplete);
	}

	/// <summary>
	/// Triggers or retriggers a short timeout.
	/// If the timeout is elapsed, OnPositionSizeChangeComplete will be called.
	/// </summary>
	private void OnSizeChanged(object? sender, SizeChangedEventArgs e) =>
		TriggerUpdateTimer();


	/// <summary>
	/// Triggers or retriggers a short timeout.
	/// If the timeout is elapsed, OnPositionSizeChangeComplete will be called.
	/// </summary>
	private void OnPositionChanged(object? sender, PixelPointEventArgs e) =>
		TriggerUpdateTimer();

	/// <summary>
	/// Restarts the timer.
	/// </summary>
	private void TriggerUpdateTimer()
	{
		_updateTimer.Stop(); // Stop any existing timer
		_updateTimer.Start(); // Restart the timer
	}

	/// <summary>
	/// The following function is executed after the user has finished resizing or moving the window.
	/// This function will console write the final output of size and position and fire
	/// the global event.
	/// </summary>
	private void OnPositionSizeChangeComplete() =>
		App.ServiceProvider?
			.GetRequiredService<MainWindowEvents>()
			.WindowSizePositionChanged(Position, ClientSize);

	// Don't forget to clean up
	protected override void OnClosed(EventArgs e)
	{
		base.OnClosed(e);
		_updateTimer.Dispose();
	}

	private async void NavigationPanel_OnSelectionChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
	{
		if (DataContext is not MainWindowViewModel viewModel) return;

		switch (e.SelectedItem)
		{
			case NavigationViewItem { Tag: NavigationCategory cat }:
				// viewModel.NavigateTo(cat);
				Console.WriteLine($"NavigationViewItem: {cat.Name}");
				break;
			case NavigationCategory category:
				// viewModel.NavigateTo(category);
				Console.WriteLine($"Category: {category.Name}");
				break;
			case NavigationViewItem {Tag: "Settings" or "Einstellungen"}:
				Console.WriteLine("Settings");
				// NavigateToSettings();
				break;
		}
	}

	public void Dispose() =>
		_updateTimer.Dispose();
}
