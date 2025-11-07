namespace Minty.Dialogs.ViewModels;

[RegisterTransient]
public sealed partial class MessageBoxViewModel : BaseMessageBox
{
	public InfoBarSeverity Severity { get; set; } = InfoBarSeverity.Informational;
}
