namespace Minty.Pages.Repositories.Models;

public sealed class Repository
{
	public required string RootPath { get; init; }
	public List<RepositoryCategory> Categories = [];
}
