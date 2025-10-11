namespace Minty.Components;

public class FormInput : TemplatedControl
{
	public static readonly StyledProperty<string> LabelProperty =
		AvaloniaProperty.Register<FormInput, string>(nameof(Label), string.Empty);

	public static readonly StyledProperty<object> ContentProperty =
		AvaloniaProperty.Register<FormInput, object>(nameof(Content));

	public static readonly StyledProperty<string> ErrorMessageProperty =
		AvaloniaProperty.Register<FormInput, string>(nameof(ErrorMessage), string.Empty);

	public static readonly StyledProperty<bool> HasErrorProperty =
		AvaloniaProperty.Register<FormInput, bool>(nameof(HasError));

	public static readonly StyledProperty<bool> IsRequiredProperty =
		AvaloniaProperty.Register<FormInput, bool>(nameof(IsRequired));

	public static readonly StyledProperty<double> LabelWidthProperty =
		AvaloniaProperty.Register<FormInput, double>(nameof(LabelWidth), 150);

	public string Label
	{
		get => GetValue(LabelProperty);
		set => SetValue(LabelProperty, value);
	}

	public object Content
	{
		get => GetValue(ContentProperty);
		set => SetValue(ContentProperty, value);
	}

	public string ErrorMessage
	{
		get => GetValue(ErrorMessageProperty);
		set => SetValue(ErrorMessageProperty, value);
	}

	public bool HasError
	{
		get => GetValue(HasErrorProperty);
		set => SetValue(HasErrorProperty, value);
	}

	public bool IsRequired
	{
		get => GetValue(IsRequiredProperty);
		set => SetValue(IsRequiredProperty, value);
	}

	public double LabelWidth
	{
		get => GetValue(LabelWidthProperty);
		set => SetValue(LabelWidthProperty, value);
	}

	static FormInput()
	{
		ErrorMessageProperty.Changed.AddClassHandler<FormInput>((x, e) =>
		{
			x.HasError = !string.IsNullOrEmpty(e.NewValue as string);
		});
	}
}
