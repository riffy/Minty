namespace Minty.Dialogs.Controllers;

using Avalonia.Data;

[RegisterSingleton]
public sealed class CategoryDialogController(IServiceProvider serviceProvider,
	LogController logController, MessageBoxController messageBoxController, RepositoryController repositoryController)
{
	/// <summary>
	/// Displays a dialog for creating a new category. This dialog allows the user to provide
	/// a name and select an icon for the new category. When the user confirms the action,
	/// a new category is created and stored in the repository.
	/// </summary>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task ShowNewCategoryDialogAsync()
	{
		try
		{
			if (App.MainWindow is null)
			{
				logController.Error("MainWindow is null. Dialog could not be shown.");
				return;
			}

			var dataContext = ActivatorUtilities.CreateInstance<CategoryDialogViewModel>(serviceProvider);
			dataContext.IsNewCategory = true;
			var content = new CategoryDialog { DataContext = dataContext };

			var dialog = new ContentDialog
			{
				Title = "Create New Category",
				Content = content,
				PrimaryButtonText = "Create Category",
				DefaultButton = ContentDialogButton.Primary,
				SecondaryButtonText = "Cancel"
			};

			// Bind the primary button enabled state to a viewmodel property
			dialog.Bind(
				ContentDialog.IsPrimaryButtonEnabledProperty,
				new Binding(nameof(CategoryDialogViewModel.IsPrimaryButtonEnabled))
				{
					Source = dataContext
				});

			var result = await dialog.ShowAsync(App.MainWindow);
			if (result != ContentDialogResult.Primary)
				return;
			repositoryController.CreateCategoryAsync(dataContext.Name, dataContext.SelectedIcon);
		}
		catch (Exception ex)
		{
			logController.Exception(ex);
			messageBoxController.ShowExceptionAsync(ex);
		}
	}

	private async Task ShowCategoryDialogAsync(RepositoryCategory? category)
	{
		try
		{
			if (App.MainWindow is null)
			{
				logController.Error("MainWindow is null. Dialog could not be shown.");
				return;
			}

			var dataContext = ActivatorUtilities.CreateInstance<CategoryDialogViewModel>(serviceProvider, category);
			var content = new CategoryDialog { DataContext = dataContext };

			var dialog = new ContentDialog
			{
				Title = "Create New Category",
				Content = content,
				PrimaryButtonText = "Create Category",
				DefaultButton = ContentDialogButton.Primary,
				SecondaryButtonText = "Cancel"
			};

			// Bind the primary button enabled state to a viewmodel property
			dialog.Bind(
				ContentDialog.IsPrimaryButtonEnabledProperty,
				new Binding(nameof(CategoryDialogViewModel.IsPrimaryButtonEnabled))
				{
					Source = dataContext
				});

			var result = await dialog.ShowAsync(App.MainWindow);
			if (result != ContentDialogResult.Primary)
				return;
			repositoryController.CreateCategoryAsync(dataContext.Name, dataContext.SelectedIcon);
		}
		catch (Exception ex)
		{
			logController.Exception(ex);
			messageBoxController.ShowExceptionAsync(ex);
		}
	}
}
