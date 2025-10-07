namespace Minty;

public class ViewLocator : IDataTemplate
{
	private IServiceProvider? _serviceProvider;

	/// <summary>
	///     Build the control to display.
	/// </summary>
	public Control? Build(object? param)
	{
		if (param is null) return null;
		_serviceProvider ??= ((App)Application.Current!).Services!;
		if (_serviceProvider is null) return null;

		var name = param.GetType().FullName;
		if (name is null) return null;

		// Strip ending "ViewModel"
		if (name.EndsWith("ViewModel"))
			name = name[..^"ViewModel".Length];

		// Replace namespace "ViewModels" with "Views"
		name = name.Replace("ViewModel", "View");
		var type = Type.GetType(name);
		if (type == null)
			return new TextBlock { Text = "Not Found: " + name };

		Control? control;
		if (_serviceProvider.GetService(type) is Control registeredControl)
			control = registeredControl;
		else
			control = ActivatorUtilities.CreateInstance(_serviceProvider, type) as Control;
		control!.DataContext = param;
		return control;
	}

	public bool Match(object? data) =>
		data is ViewModelBase;
}
