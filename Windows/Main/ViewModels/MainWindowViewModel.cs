namespace Minty.Windows.Main.ViewModels;

[RegisterSingleton]
public sealed partial class MainWindowViewModel : ViewModelBase
{
	[ObservableProperty]
	private ViewModelBase? _currentPage;

	public MainWindowViewModel(IServiceProvider sP)
	{
		CurrentPage = sP.GetRequiredService<MainPageViewModel>();
	}
}
