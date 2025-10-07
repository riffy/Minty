namespace Minty.Windows.Main.Models;

/// <summary>
/// Represents a custom template selector for navigation items,
/// determining the appropriate data template based on the type of item.
/// </summary>
public sealed class NavigationItemTemplateSelector : DataTemplateSelector
{
	[Content]
	public IDataTemplate ItemTemplate { get; set; } = default!;
	public IDataTemplate SeparatorTemplate { get; set; } = default!;
	public IDataTemplate ItemTemplateWithChildren { get; set; } = default!;

	protected override IDataTemplate SelectTemplateCore(object item) =>
		item switch
		{
			NavigationSeperator => SeparatorTemplate,
			NavigationCategory => ItemTemplate,
			_ => ItemTemplateWithChildren
		};
}
