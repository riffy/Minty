namespace Minty.Pages.Repositories.Models;

public sealed class RepositoryCategory
{
	/// <summary>
	/// Folder name of the category
	/// </summary>
	public required string Name { get; init; }

	/// <summary>
	/// Absolute path of the category
	/// </summary>
	public required string Path { get; init; }

	/// <summary>
	/// The metadata of the category loaded from the category.json file
	/// </summary>
	public required RepositoryCategoryMeta Meta { get; init; }
}
