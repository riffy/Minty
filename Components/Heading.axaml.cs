namespace Minty.Components;

public class Heading : TemplatedControl
{
	public static readonly StyledProperty<string> TextProperty =
		AvaloniaProperty.Register<Heading, string>(nameof(Text), string.Empty);

	public static readonly StyledProperty<int> LevelProperty =
		AvaloniaProperty.Register<Heading, int>(nameof(Level), 1);

	public string Text
	{
		get => GetValue(TextProperty);
		set => SetValue(TextProperty, value);
	}

	public int Level
	{
		get => GetValue(LevelProperty);
		set => SetValue(LevelProperty, Math.Clamp(value, 1, 6));
	}
}

