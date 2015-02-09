using System;

namespace SnakeBasic.Helpers
{
	/// <summary>
	/// Specifies a direction.
	/// </summary>
	public enum Direction
	{
		/// <summary>
		/// The direction right.
		/// </summary>
		Right,

		/// <summary>
		/// The direction down.
		/// </summary>
		Down,

		/// <summary>
		/// The direction left.
		/// </summary>
		Left,

		/// <summary>
		/// The direction up.
		/// </summary>
		Up
	}

	/// <summary>
	/// Specifies a formation of walls.
	/// </summary>
	[Flags]
	public enum WallFormation
	{
		/// <summary>
		/// A formation in form of a rectangle.
		/// </summary>
		Rectangle = 1,

		/// <summary>
		/// A formation in form of four crosses.
		/// </summary>
		FourCrosses = 2
	}
}
