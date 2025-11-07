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
		Repository repo = new()
		{
			RootPath = path,
			Categories = await LoadCategories(path)
		};
		return repo;
	}

	/// <summary>
	/// Loads the categories from the specified repository path by deserializing category JSON files.
	/// </summary>
	/// <param name="repoPath">The path to the repository's root directory containing category definitions.</param>
	/// <returns>A list of <see cref="RepositoryCategory"/> objects representing the loaded categories.</returns>
	/// <exception cref="DirectoryNotFoundException">Thrown when the categories directory does not exist.</exception>
	/// <exception cref="JsonException">Thrown when a category JSON file cannot be deserialized.</exception>
	/// <exception cref="IOException">Thrown when an error occurs reading category files.</exception>
	/// <exception cref="Exception">Thrown when an unexpected error occurs while loading categories.</exception>
	private async Task<List<RepositoryCategory>> LoadCategories(string repoPath)
	{
		string categoriesPath = Path.Combine(repoPath, ".categories");
		if (!Directory.Exists(categoriesPath))
			return [];
		List<RepositoryCategory> categories = [];
		try
		{
			// Find all .json files in the categories directory and deserialize them
			var jsonFiles = Directory.GetFiles(categoriesPath, "*.json", SearchOption.TopDirectoryOnly);

			foreach (var jsonFile in jsonFiles)
			{
				var category = JsonSerializer.Deserialize<RepositoryCategory>(await File.ReadAllTextAsync(jsonFile),
					RepositoryCategory.JsonSerializerOptions);
				if (category is null)
				{
					logController.Warn($"JSON file {jsonFile} is not a valid category file.");
					continue;
				}
				categories.Add(category);
			}
			categories.Sort((x, y) => string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase));
		}
		catch (Exception ex)
		{
			logController.Exception(ex);
			throw;
		}

		return categories;
	}

	/// <summary>
	/// Creates a new category within the specified repository path, serializes it, and saves it as a JSON file.
	/// </summary>
	/// <param name="repoPath">The path to the repository where the category will be created.</param>
	/// <param name="name">The name of the category to be created.</param>
	/// <param name="icon">The icon to use for this category.</param>
	/// <returns>A <see cref="RepositoryCategory"/> object representing the newly created category.</returns>
	/// <exception cref="NullReferenceException">Thrown when the target directory for the new category does not exist.</exception>
	/// <exception cref="Exception">Thrown when an error occurs during the creation or saving of the category.</exception>
	public async Task<RepositoryCategory> CreateCategoryAsync(string repoPath, string name, Symbol icon = Symbol.Folder)
	{
		string categoriesPath = Path.Combine(repoPath, ".categories");
		if (!Directory.Exists(categoriesPath))
			throw new NullReferenceException("Target directory for new category does not exist");
		if (File.Exists(Path.Combine(categoriesPath, $"{name.ToLowerInvariant()}.json")))
			throw new ArgumentException("Category with the same name already exists");
		try
		{
			RepositoryCategory category = new()
			{
				Name = name,
				DataSets = [],
				Folder = name.ToLowerInvariant(),
				Icon = icon
			};
			var json = JsonSerializer.Serialize(category, RepositoryCategory.JsonSerializerOptions);
			await File.WriteAllTextAsync(Path.Combine(categoriesPath, $"{category.Folder}.json"), json);
			return category;
		}
		catch (Exception ex)
		{
			logController.Exception(ex);
			throw;
		}
	}
}
