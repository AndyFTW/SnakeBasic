using System;
using System.Drawing;

namespace SnakeBasic.Helpers
{
  public static class DrawHelper
  {
    /// <summary>
    /// Draws a specified char to a specified point.
    /// </summary>
    /// <param name="position">Specifies the point the char should get drawn to.</param>
    /// <param name="renderingChar">Specifies the char which should get drawn.</param>
    public static void Draw(Point position, char renderingChar)
    {
      Draw(position.X, position.Y, renderingChar);
    }

    /// <summary>
    /// Draws a specified char to specified coordinates.
    /// </summary>
    /// <param name="x">Specifies the x coordinate of the point where the char should get drawn.</param>
    /// <param name="y">Specifies the y coordinate of the point where the char should get drawn.</param>
    /// <param name="renderingChar">Specifies the char which should get drawn.</param>
    public static void Draw(int x, int y, char renderingChar)
    {
      Console.SetCursorPosition(x, y);
      Console.Write(renderingChar);
      Console.SetCursorPosition(0, 0); // Set back to first line that the scrollbar is always on top
    }
  }
}
