namespace Minty.Pages.Repositories.Models;

public sealed class RepositoryCategory
{
	/// <summary>
	/// Configuration options for JSON serialization and deserialization in the RepositoryCategory class.
	/// </summary>
	public static readonly JsonSerializerOptions JsonSerializerOptions = new()
	{
		PropertyNameCaseInsensitive = true,
		IncludeFields = true,
		NumberHandling = JsonNumberHandling.AllowReadingFromString,
		WriteIndented = true,
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
	};

	/// <summary>
	/// Folder name of the category
	/// </summary>
	public required string Name { get; init; }

	/// <summary>
	/// The folder name of the category
	/// </summary>
	public required string Folder { get; init; }

	/// <summary>
	/// List of data sets in the category, sorted by appearance
	/// </summary>
	public required List<RepositoryDataSet> DataSets { get; init; } = [];

	public Symbol? Icon { get; set; }

	public NavigationCategory GetNavigationItem()
	{
		NavigationCategory ret = new()
		{
			Name = Name,
			Icon = Icon ?? Symbol.Folder,
			Children = [],
			Category = this
		};
		foreach (var dataSet in DataSets)
			ret.Children.Add(dataSet.GetNavigationItem());
		return ret;
	}
}
