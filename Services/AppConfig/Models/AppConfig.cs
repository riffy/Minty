namespace Minty.Services.AppConfig.Models;

public class AppConfig : INotifyPropertyChanged
{
	#region WINDOW SIZE / POSITION
	private ConfigIntVector _windowPosition =
		new(MainWindowData.DefaultPosition.X, MainWindowData.DefaultPosition.Y);
	private ConfigIntVector _windowSize =
		new((int)MainWindowData.DefaultSize.Width, (int)MainWindowData.DefaultSize.Height);

	public ConfigIntVector WindowPosition
	{
		get => _windowPosition;
		set
		{
			if (value.X == _windowPosition.X && value.Y == _windowPosition.Y) return;
			_windowPosition = value;
			OnPropertyChanged();
		}
	}

	public ConfigIntVector WindowSize
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

	public event PropertyChangedEventHandler? PropertyChanged;
	private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
