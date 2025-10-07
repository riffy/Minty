namespace Minty.Core.Models;

/// <summary>
/// Represents a generic 2D vector with X and Y coordinates.
/// </summary>
/// <typeparam name="T">Type of the vector's components.</typeparam>
/// <param name="x">The X component of the vector.</param>
/// <param name="y">The Y component of the vector.</param>
public sealed class Vector2<T>(T x, T y)
{
	public T X { get; set; } = x;
	public T Y { get; set; } = y;
}
