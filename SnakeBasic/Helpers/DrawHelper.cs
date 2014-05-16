using System;
using System.Drawing;

namespace SnakeBasic.Helpers
{
  public static class DrawHelper
  {
    public static void Draw(Point position, char renderingChar)
    {
      Draw(position.X, position.Y, renderingChar);
    }

    public static void Draw(int left, int top, char renderingChar)
    {
      Console.SetCursorPosition(left, top);
      Console.Write(renderingChar);
      Console.SetCursorPosition(0, 0); // Set back to first line that the scrollbar is always on top
    }
  }
}
