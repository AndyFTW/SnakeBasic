using System;

namespace SnakeBasic.Helpers
{
	/// <summary>
	/// Specifies a direction.
	/// </summary>
	public enum Direction
	{
		/// <summary>
		/// The directory right.
		/// </summary>
		Right,

		/// <summary>
		/// The directory down.
		/// </summary>
		Down,

		/// <summary>
		/// The directory left.
		/// </summary>
		Left,

		/// <summary>
		/// The directory up.
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
