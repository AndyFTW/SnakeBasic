using System.Drawing;

namespace SnakeBasic.Entities
{
  public class Wall : Entity
  {
    /// <summary>
    /// Initializes a new instance of SnakeBasic.Entitys.Wall class.
    /// </summary>
    public Wall(Point position)
    {
      this.Coordinates.Add(position);
    }

    /// <summary>
    /// Gets a value indicating the rendering char of the entity.
    /// </summary>
    public override char RenderingChar
    {
      get
      {
        return '#';
      }
    }
  }
}
