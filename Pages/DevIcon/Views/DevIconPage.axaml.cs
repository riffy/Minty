namespace Minty.Pages.DevIcon.Views;

public partial class DevIconPage : UserControl
{
	public DevIconPage()
	{
		InitializeComponent();
		CreateIconDisplays();
	}

	/// <summary>
	/// Creates the icon display based on the Symbol enum.
	/// The symbols are ordered alphabetically and added to the wrappanel
	/// </summary>
	private void CreateIconDisplays()
	{
		List<Border> symbolContainers = [];
		var symbols = Enum.GetValues<Symbol>();
		foreach (Symbol symbol in symbols)
		{
			Border container = new()
			{
				Margin = Thickness.Parse("5"),
				BorderBrush = Brushes.Black,
				BorderThickness = Thickness.Parse("1"),
				Padding = Thickness.Parse("5"),
				CornerRadius = CornerRadius.Parse("10")
			};
			StackPanel sp = new() { HorizontalAlignment = HorizontalAlignment.Center };
			sp.Children.Add(new SymbolIcon() { Symbol = symbol, FontSize = 24 });
			sp.Children.Add(new TextBlock() { Text = $"{symbol}"});
			container.Child = sp;
			symbolContainers.Add(container);
		}

		// Sort the containers based on the
		symbolContainers = symbolContainers
			.OrderBy(c => ((c.Child as StackPanel)!.Children[1] as TextBlock)!.Text)
			.ToList();
		foreach (var sc in symbolContainers)
			WrapPanel.Children.Add(sc);
	}
}

