namespace Minty.Pages.Repositories.Controllers;

[RegisterSingleton]
public sealed class RepositoryController(LogController logController, AppConfigController appConfigController,
	RepositoryService repositoryService, RepositoryEvents repositoryEvents)
{
	/// <summary>
	/// Initializes the repository by setting up necessary paths and loading the repository data.
	/// Updates the navigation item status based on the presence of a repository path in the configuration.
	/// </summary>
	/// <returns>Returns a task that represents the asynchronous operation. The task result contains a boolean value
	/// indicating the success or failure of the initialization process.</returns>
	public async Task<bool> Initialize()
	{
		try
		{
			if (string.IsNullOrEmpty(appConfigController.Config.RepositoryPath))
			{
				// repositoryPageViewModel.RepositoryPath = string.Empty;
				NavigationItem.IsEnabled = false;
				return true;
			}

			// Set the paths & load the repository
			// repositoryPageViewModel.RepositoryPath = appConfigController.Config.RepositoryPath;
			NavigationItem.IsEnabled = true;
			Repository = await repositoryService.LoadRepository(appConfigController.Config.RepositoryPath);
			return true;
		}
		catch (Exception ex)
		{
			logController.Exception(ex);
			return false;
		}
	}

	#region NAVIGATION

	/// <summary>
	/// Represents a specific navigation item used in the application for navigating
	/// to the repository page.
	/// </summary>
	public readonly NavigationItem NavigationItem = new()
	{
		Name = Resources.Module_Repository,
		Icon = Symbol.Library,
		Target = typeof(RepositoryPageViewModel),
		IsEnabled = false
	};

	#endregion

	#region REPOSITORY

	public Repository? Repository { get; set; }

	/// <summary>
	/// Sets the repository path to the specified value. This updates the
	/// configuration and saves it to a file. Logs a warning if the path is invalid.
	/// </summary>
	/// <param name="path">The file path to be set as the repository path.</param>
	/// <returns>Returns a task that represents the asynchronous operation.</returns>
	public async Task SetNewRepository(string path)
	{
		if (string.IsNullOrEmpty(path))
			return;
		if (path == appConfigController.Config.RepositoryPath)
			return;
		if (!Directory.Exists(path))
		{
			// TODO: Show error message as Error Dialog
			logController.Warn($"Selected folder is invalid: {path}");
			return;
		}
		appConfigController.Config.RepositoryPath = path;
		await appConfigController.SaveConfigToFile();
		// TODO: Show content dialog with loading of repository
		// TODO: Show possible error dialog on exception
		Repository = await repositoryService.LoadRepository(path);
		repositoryEvents.NewRepositoryApplied(Repository);
	}

	#endregion
}
