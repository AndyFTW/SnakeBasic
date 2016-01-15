using System;

namespace SnakeBasic.Helpers
{
    public enum Direction
    {
        Right,
        Down,
        Left,
        Up
    }

    /// <summary>
    /// Specifies a formation of walls.
    /// </summary>
    [Flags]
    public enum WallFormation
    {
        Rectangle = 1,
        FourCrosses = 2
    }
}
