namespace Minty.ViewModels;

[RegisterSingleton]
public class MainWindowViewModel : ViewModelBase
{
	public string Greeting { get; } = "Welcome to Avalonia!";
}
