namespace Minty.Windows.Main.Controllers;

[RegisterSingleton]
public sealed class MainWindowController
{
	private readonly LogController _logController;

	public MainWindowController(MainWindowEvents mwe, LogController lc)
	{
		_logController = lc;
	}
}
