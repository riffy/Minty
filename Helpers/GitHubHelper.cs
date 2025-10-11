namespace Minty.Helpers;

/// <summary>
/// This class contains helper methods for GitHub.
/// </summary>
public static class GitHubHelper
{
	/// <summary>
	/// This class contains helper methods for creating new issues.
	/// </summary>
	public static class NewIssue
	{
		/// <summary>
		/// Creates a new issue on GitHub and return the url to the issue to open it.
		/// </summary>
		public static string CreateExceptionIssue(Exception? exception, string? message)
		{
			var title = CreateIssueTitle(message);
			var body = CreateExceptionBody(exception, message);
			var encodedBody = Uri.EscapeDataString(body);
			return $"https://github.com/riffy/Minty/issues/new?title={title}&body={encodedBody}&labels=bug";
		}

		/// <summary>
		/// Creates a new issue title based on the provided message.
		/// </summary>
		private static string CreateIssueTitle(string? message)
			=> $"Bug Report: {message ?? "Application Error"}";

		/// <summary>
		/// Creates a new issue body based on the provided exception and message.
		/// </summary>
		private static string CreateExceptionBody(Exception? exception, string? message)
		{
			var body = "## Bug Description\n";

			if (!string.IsNullOrEmpty(message))
				body += $"{message}\n\n";

			body += "## Steps to Reproduce\n";
			body += "1. \n";
			body += "2. \n";
			body += "3. \n\n";

			body += "## Expected Behavior\n\n\n";
			body += "## Actual Behavior\n\n\n";

			if (exception != null)
			{
				body += "## Exception Details\n";
				body += "```\n";
				body += exception.ToString();
				body += "\n```\n\n";
			}

			body += "## Environment\n";
			body += $"- OS: {Environment.OSVersion}\n";
			body += $"- .NET Version: {Environment.Version}\n";
			body += $"- Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n";

			return body;
		}
	}
}
