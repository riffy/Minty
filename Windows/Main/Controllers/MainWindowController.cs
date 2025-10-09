namespace Minty.Windows.Main.Controllers;

[RegisterSingleton]
public sealed class MainWindowController : IDisposable
{
	/// <summary>
	/// Indicator to check if local changes should be saved.
	/// </summary>
	private bool _initialized;
	private bool _disposed;
	private readonly LogController _logController;
	private readonly AppConfigController _appConfigController;
	private readonly CancellationTokenSource _cancellationTokenSource = new();

	public MainWindowController(MainWindowEvents mwe, LogController lc, AppConfigController acc)
	{
		_logController = lc;
		_appConfigController = acc;
		mwe.OnWindowSizePositionChanged += OnPositionSizeChanged;
	}

	/// <summary>
	/// Reads the screen position and screen size from the config and sets the window position and size.
	/// </summary>
	public async Task<bool> Initialize()
	{
		try
		{
			if (_initialized || _disposed)
				return true;
			if (App.MainWindow is not { IsLoaded: true })
				return false;
			App.MainWindow.RequestedThemeVariant = ThemeHelper.GetThemeVariant(_appConfigController.Config.Theme);
			App.MainWindow.Position = new(_appConfigController.Config.WindowPosition.X, _appConfigController.Config.WindowPosition.Y);
			App.MainWindow.Width = _appConfigController.Config.WindowSize.X;
			App.MainWindow.Height = _appConfigController.Config.WindowSize.Y;
			_initialized = true;
			return true;
		}
		catch (Exception ex)
		{
			_logController.Exception(ex);
			return false;
		}
	}

	/// <summary>
	/// Handles the event triggered when the window's position or size changes.
	/// Updates the local configuration with the new position and size values,
	/// and saves the updated configuration to a file.
	/// </summary>
	/// <param name="position">The new position of the window, represented as a pixel point.</param>
	/// <param name="size">The new size of the window, represented as a size object.</param>
	private async Task OnPositionSizeChanged(PixelPoint position, Size size)
	{
		try
		{
			if (_disposed || _cancellationTokenSource.Token.IsCancellationRequested)
				return;

			_logController.Debug($"Window Position: X={position.X}, Y={position.Y}");
			_logController.Debug($"Window Size: Width={size.Width}, Height={size.Height}");
			if (!_initialized) return;
			// Save window properties to config
			_appConfigController.Config.WindowPosition = new(position.X, position.Y);
			_appConfigController.Config.WindowSize = new((int)size.Width, (int)size.Height);
			await _appConfigController.SaveConfigToFile();
		}
		catch (OperationCanceledException)
		{
			// Expected during shutdown, don't log as error
			return;
		}
		catch (Exception ex)
		{
			_logController.Exception(ex);
		}
	}

	public void Dispose()
	{
		if (_disposed) return;
		_disposed = true;

		_cancellationTokenSource.Cancel();
		_cancellationTokenSource.Dispose();
	}
}
