namespace Minty.Core.Models;

/// <summary>
/// Represents the outcome of an operation that can either be successful or unsuccessful.
/// </summary>
/// <typeparam name="T">The type of the value associated with a successful operation.</typeparam>
public class Result<T>
{
	public bool IsSuccess { get; }
	public T? Value { get; }
	public string? Error { get; }

	private Result(bool isSuccess, T? value, string? error)
	{
		IsSuccess = isSuccess;
		Value = value;
		Error = error;
	}

#pragma warning disable CA1000
	public static Result<T> Ok(T value) => new(true, value, null);
	public static Result<T> Fail(string error) => new(false, default, error);
#pragma warning restore CA1000
}
