namespace Minty.Dialogs.ViewModels;

[RegisterTransient]
public sealed partial class CategoryDialogViewModel : ViewModelBase
{
	private readonly RepositoryController _repositoryController;

	public CategoryDialogViewModel(RepositoryController repositoryController)
	{
		_repositoryController = repositoryController;
		// Get all available icons and sort them alphabetically
		var icons = Enum.GetValues<Symbol>().ToList();
		icons.Sort((a,b) => string.Compare(a.ToString(), b.ToString(), StringComparison.OrdinalIgnoreCase));
		AvailableIcons = icons.ToArray();
	}

	/// <summary>
	/// Indicates whether the dialog is for creating a new category or editing an existing one.
	/// </summary>
	public bool IsNewCategory { get; set; }

	#region NAME

	/// <summary>
	/// Represents the name of the category being created or edited in the dialog.
	/// </summary>
	[ObservableProperty] private string _name = string.Empty;

	/// <summary>
	/// Stores the validation message related to the name field in the category dialog.
	/// </summary>
	[ObservableProperty] private string _nameValidationMessage = string.Empty;

	/// <summary>
	/// Executes logic when the name property changes, validating it based on the current context
	/// (whether it is a new category or an existing one being edited).
	/// </summary>
	/// <param name="value">The new name value to be validated.</param>
	partial void OnNameChanged(string value)
	{
		if (IsNewCategory)
			ValidateNewName(value);
		else
			ValidateEditName(value);
	}

	private void ValidateNewName(string value)
	{
		if (_repositoryController.Repository is null)
			NameValidationMessage = "Repository not selected";
		else if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
			NameValidationMessage = "Name cannot be empty";
		// Check if the name already exists
		else if (_repositoryController.Repository.Categories.Any(c => c.Name == value))
			NameValidationMessage = "Category with the same name already exists";
		// TODO: Validate more name checks
		else
			NameValidationMessage = string.Empty;
		IsPrimaryButtonEnabled = string.IsNullOrEmpty(NameValidationMessage);
	}

	private void ValidateEditName(string value)
	{

	}
	#endregion

	#region ICON

	/// <summary>
	/// Represents the currently selected icon for the category dialog,
	/// with a default value of <see cref="Symbol.Folder"/>.
	/// </summary>
	[ObservableProperty]
	private Symbol _selectedIcon = Symbol.Folder;

	/// <summary>
	/// Gets the list of all available icons, represented as an array of <see cref="Symbol"/> values,
	/// sorted alphabetically by their string representation.
	/// </summary>
	public Symbol[] AvailableIcons { get; }

	#endregion

	#region PRIMARY BUTTON

	private bool _isPrimaryButtonEnabled;

	public bool IsPrimaryButtonEnabled
	{
		get => _isPrimaryButtonEnabled;
		set => SetProperty(ref _isPrimaryButtonEnabled, value);
	}

	private void UpdatePrimaryButtonState() =>
		IsPrimaryButtonEnabled = !string.IsNullOrWhiteSpace(Name) && SelectedIcon != null;

	#endregion
}
