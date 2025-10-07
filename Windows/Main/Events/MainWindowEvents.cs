namespace Minty.Windows.Main.Events;

/// <summary>
/// Represents the event handler for the main window's size and position changes.
/// </summary>
[RegisterSingleton]
public sealed class MainWindowEvents
{
	#region WINDOW SIZE / POSITION
	/// <summary>
	/// Event that is fired if the main window gets moved or resized.
	/// </summary>
	public delegate Task WindowSizePositionDelegate(PixelPoint position, Size size);
	public event WindowSizePositionDelegate? OnWindowSizePositionChanged;
	public void WindowSizePositionChanged(PixelPoint position, Size size) =>
		OnWindowSizePositionChanged?.Invoke(position, size);
	#endregion
}
