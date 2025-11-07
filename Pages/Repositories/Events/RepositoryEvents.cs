namespace Minty.Pages.Repositories.Events;

[RegisterSingleton]
public sealed class RepositoryEvents
{
	#region NEW REPOSITORY
	public delegate Task NewRepositoryDelegate(Repository? repo);

	/// <summary>
	/// Event triggered when a new repository is applied.
	/// Null, when no repository is applied.
	/// </summary>
	public event NewRepositoryDelegate? OnNewRepository;
	public void NewRepositoryApplied(Repository? repo) =>
		OnNewRepository?.Invoke(repo);
	#endregion

	#region CATEGORIES CHANGED

	public delegate Task CategoriesChangedDelegate();

	/// <summary>
	/// Event triggered when the categories of the repository are changed.
	/// </summary>
	public event CategoriesChangedDelegate? OnCategoriesChanged;
	public void CategoriesChanged() =>
		OnCategoriesChanged?.Invoke();

	#endregion
}
