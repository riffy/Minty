namespace Minty.Windows.Main.Views;

using Avalonia.Interactivity;

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
	/// Handles the logic that should execute when the window is loaded, such as setting
	/// the default dimensions and centering the window on the primary screen.
	/// </summary>
	/// <param name="e">The event data associated with the loaded event.</param>
	protected override void OnLoaded(RoutedEventArgs e)
	{
		Width = MainWindowData.DefaultSize.Width;
		Height = MainWindowData.DefaultSize.Height;
		CenterWindow();
		base.OnLoaded(e);
	}

	/// <summary>
	/// Centers the window on the primary screen by calculating the appropriate position
	/// based on the screen's working area and the window's dimensions.
	/// </summary>
	private void CenterWindow()
	{
		if (Screens.Primary is null) return;

		PixelSize screenSize = Screens.Primary.WorkingArea.Size;
		Position = new(
			(screenSize.Width - (int)Width) / 2,
			(screenSize.Height - (int)Height) / 2);
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

	public void Dispose() =>
		_updateTimer.Dispose();
}
