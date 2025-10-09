namespace Minty.Screens.Main.Models;

/// <summary>
/// Represents a custom template selector for navigation items,
/// determining the appropriate data template based on the type of item.
/// </summary>
public sealed class NavigationItemTemplateSelector : DataTemplateSelector
{
	[Content]
	public IDataTemplate ItemTemplate { get; set; } = default!;
	public IDataTemplate SeparatorTemplate { get; set; } = default!;
	public IDataTemplate CategoryTemplate { get; set; } = default!;
	public IDataTemplate HeaderTemplate { get; set; } = default!;

	protected override IDataTemplate SelectTemplateCore(object item) =>
		item switch
		{
			NavigationSeperator => SeparatorTemplate,
			NavigationHeader => HeaderTemplate,
			_ => ItemTemplate
		};
}
