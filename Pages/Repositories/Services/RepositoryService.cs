namespace Minty.Pages.Repositories.Services;

[RegisterSingleton]
public sealed class RepositoryService(LogController logController)
{
	/// <summary>
	/// Loads the repository from the specified path, identifying and organizing categories.
	/// </summary>
	/// <param name="path">The path to the repository's root directory.</param>
	/// <returns>A <see cref="Repository"/> object containing the details of the repository and its categories.</returns>
	/// <exception cref="ArgumentException">Thrown when the specified path is invalid or does not exist.</exception>
	public async Task<Repository> LoadRepository(string path)
	{
		logController.Info($"Loading repository from {path}");
		// 1. Check if the path is valid and exists
		if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
			throw new ArgumentException($"Given path {path} is invalid");
		Repository repo = new() { RootPath = path };

		try
		{
			var subDirectories = Directory.GetDirectories(path);
			foreach (var subDirectory in subDirectories)
			{
				var categoryJsonPath = Path.Combine(subDirectory, "category.json");
				if (!File.Exists(categoryJsonPath))
				{
					logController.Debug($"No category.json found in {subDirectory}, skipping...");
					continue;
				}
				var meta = await LoadCategoryMetaFromFile(categoryJsonPath);
				if (meta is null)
				{
					logController.Warn($"category.json found in {subDirectory} is not valid.");
					continue;
				}

				repo.Categories.Add(new()
				{
					Name = subDirectory.Split(Path.DirectorySeparatorChar).Last(),
					Path = subDirectory,
					Meta = meta
				});
			}
		}
		catch (Exception ex)
		{
			logController.Exception(ex);
			throw;
		}

		return repo;
	}

	private async Task<RepositoryCategoryMeta?> LoadCategoryMetaFromFile(string path)
	{
		if (!File.Exists(path))
			return null;
		try
		{
			var jsonContent = await File.ReadAllTextAsync(path);
			return JsonSerializer.Deserialize<RepositoryCategoryMeta>(jsonContent);
		}
		catch (Exception ex)
		{
			logController.Exception(ex);
			return null;
		}
	}
}
