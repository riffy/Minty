namespace Minty.Pages.Repositories.Events;

[RegisterSingleton]
public sealed class RepositoryEvents
{
	#region NEW REPOSITORY

	public delegate Task NewRepositoryDelegate(Repository repo);
	public event NewRepositoryDelegate? OnNewRepository;
	public void NewRepositoryApplied(Repository repo) =>
		OnNewRepository?.Invoke(repo);
	#endregion
}
