using SnakeBasic.Helpers;
using System;
using System.Drawing;
using System.Linq;

namespace SnakeBasic.Entities
{
  public class Candy : Entity
  {
    /// <summary>
    /// Initializes a new instance of SnakeBasic.Entitys.Candy class.
    /// </summary>
    public Candy()
    {
      this.RenderingChar = '*';

      CreateNewCandyPoint();
    }

    void CreateNewCandyPoint()
    {
      while (true)
      {
        Point suggestion = LevelHelper.GetRandomPoint();

        if (Program.ActiveSnake.Body.Contains(suggestion) || Program.ActiveSnake.Head == suggestion || Program.Walls.Any(x => x.Position == suggestion))
          continue;

        this.Position = suggestion;
        DrawHelper.Draw(this.Position, this.RenderingChar);
        return;
      }
    }
  }
}
