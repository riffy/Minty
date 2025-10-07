namespace Minty.Services.App.Models;

public class AppConfig : INotifyPropertyChanged
{
	#region THEME
	/// <summary>
	/// The saved theme of the app
	/// </summary>
	private string _theme = ThemeHelper.THEME_SYSTEM_ID;

	public string Theme
	{
		get => _theme;
		set
		{
			_theme = value;
			OnPropertyChanged();
		}
	}

	#endregion

	#region WINDOW SIZE / POSITION
	private Vector2<int> _windowPosition =
		new(MainWindowData.DefaultPosition.X, MainWindowData.DefaultPosition.Y);
	private Vector2<int> _windowSize =
		new((int)MainWindowData.DefaultSize.Width, (int)MainWindowData.DefaultSize.Height);

	public Vector2<int> WindowPosition
	{
		get => _windowPosition;
		set
		{
			if (value.X == _windowPosition.X && value.Y == _windowPosition.Y) return;
			_windowPosition = value;
			OnPropertyChanged();
		}
	}

	public Vector2<int> WindowSize
	{
		get => _windowSize;
		set
		{
			if (value.X == _windowSize.X && value.Y == _windowSize.Y) return;
			_windowSize = value;
			OnPropertyChanged();
		}
	}

	#endregion

	/// <summary>
	/// Initializes the current app configuration with the values from the provided configuration object.
	/// </summary>
	/// <param name="config">The configuration object containing the values used to initialize the current instance.</param>
	public void Initialize(AppConfig config)
	{
		_theme = config.Theme;
		_windowPosition = config.WindowPosition;
		_windowSize = config.WindowSize;
		OnPropertyChanged();
	}

	public event PropertyChangedEventHandler? PropertyChanged;
	private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
