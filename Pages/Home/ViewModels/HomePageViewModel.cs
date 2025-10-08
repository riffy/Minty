namespace Minty.Pages.Home.ViewModels;

[RegisterSingleton]
public sealed partial class HomePageViewModel : ViewModelBase
{

	private int _buttonPressedCount;

	public string ButtonPressCountText =>
		string.Format(null, _buttonCountFormat, _buttonPressedCount);

	private static readonly CompositeFormat _buttonCountFormat =
		CompositeFormat.Parse(Resources.ButtonPressCount);

	[RelayCommand]
	private void IncrementButtonCount()
	{
		_buttonPressedCount++;
		OnPropertyChanged(nameof(ButtonPressCountText));
	}

}
