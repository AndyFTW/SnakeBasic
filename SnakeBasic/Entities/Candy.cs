using SnakeBasic.Helpers;
using System.Drawing;
using System.Linq;

namespace SnakeBasic.Entities
{
  public class Candy : Entity
  {
    /// <summary>
    /// Initializes a new instance of SnakeBasic.Entities.Candy class.
    /// </summary>
    public Candy()
    {
      CreateNewCandyPoint();
    }

    void CreateNewCandyPoint()
    {
      while (true)
      {
        Point suggestion = LevelHelper.GetRandomPoint();

        if (Program.ActiveSnake.Coordinates.Contains(suggestion) || Program.Walls.Any(x => x.Coordinates.Contains(suggestion)))
          continue;

        if (this.Coordinates.Count == 1)
          this.Coordinates[0] = suggestion;
        else
          this.Coordinates.Add(suggestion);
        DrawHelper.Draw(this.Coordinates[0], this.RenderingChar);
        return;
      }
    }

    /// <summary>
    /// Gets a value indicating the rendering char of the entity.
    /// </summary>
    public override char RenderingChar
    {
      get { return '*'; }
    }
  }
}
