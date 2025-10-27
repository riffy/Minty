namespace Minty.Pages.Repositories.Controllers;

[RegisterSingleton]
public sealed class RepositoryController(LogController logController, AppConfigController appConfigController,
	RepositoryService repositoryService, RepositoryEvents repositoryEvents, DialogController dialogController)
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
				NavigationItem.IsEnabled = false;
				return true;
			}
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
		try
		{
			if (string.IsNullOrEmpty(path))
				return;
			if (path == appConfigController.Config.RepositoryPath)
				return;
			if (!Directory.Exists(path))
			{
				logController.Info($"Selected folder is invalid or does not exist: {path}");
				dialogController.ShowWarningDialogAsync(Resources.Diag_NewRepo_Warn_InvalidDirectory);
				return;
			}
			appConfigController.Config.RepositoryPath = path;
			await appConfigController.SaveConfigToFile();
			// TODO: Show content dialog with loading of repository
			Repository = await repositoryService.LoadRepository(path);
			NavigationItem.IsEnabled = true;
			repositoryEvents.NewRepositoryApplied(Repository);
		}
		catch (Exception ex)
		{
			logController.Exception(ex);
			dialogController.ShowExceptionDialogAsync(ex);
		}
	}

	/// <summary>
	/// Clears the current repository by resetting the repository path in the application configuration,
	/// saving the updated configuration to a file, and notifying relevant listeners about the change.
	/// </summary>
	/// <returns>Returns a task that represents the asynchronous operation.</returns>
	public async Task ClearRepository()
	{
		appConfigController.Config.RepositoryPath = string.Empty;
		await appConfigController.SaveConfigToFile();
		Repository = null;
		NavigationItem.IsEnabled = false;
		repositoryEvents.NewRepositoryApplied(Repository);
	}

	#endregion
}
