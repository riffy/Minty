namespace Minty.Windows.Main.Models;

/// <summary>
/// A Navigation target constructed by the viewmodel and possible args
/// </summary>
/// <param name="target">The ViewModel service (singleton / transient) to navigate to</param>
/// <param name="args">Possible arguments while creating an instance</param>
public sealed class NavigationTarget(Type target, params object[] args)
{
	public Type Target { get; } = target;
	public object[] Args { get; } = args;
}
