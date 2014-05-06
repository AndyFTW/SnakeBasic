using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SnakeBasic.Entities
{
  public abstract class Entity
  {
    /// <summary>
    /// Gets or sets a value indicating the rendering char of the entity.
    /// </summary>
    public char RenderingChar { get; set; }

    /// <summary>
    /// Gets or sets the position of the entity.
    /// </summary>
    public Point Position { get; set; }

    /// <summary>
    /// Initializes a new instance of SnakeBasic.Entities.Entity class.
    /// </summary>
    public Entity()
    {
      this.RenderingChar = 'n'; // n = nil
    }

    #region Position

    /// <summary>
    /// Changes the Y-Position of the entity.
    /// </summary>
    /// <param name="x">Determinates the size of the shifting.</param>
    public void SetPosX(int x)
    {
      this.Position = new Point(this.Position.X + x, this.Position.Y);
    }

    /// <summary>
    /// Changes the Y-Position of the entity.
    /// </summary>
    /// <param name="y">Determinates the size of the shifting.</param>
    public void SetPosY(int y)
    {
      this.Position = new Point(this.Position.X, this.Position.Y + y);
    }

    #endregion

    /// <summary>
    /// Finds the first entity beeing in the close range of the given entity, if found; otherwise: null. Searching clockwise.
    /// </summary>
    /// <param name="obj">Specifies the origin entity.</param>
    public Entity FindNextObject(Entity obj)
    {
      // obj: O
      // to search points: X

      // XXX
      // XOX
      // XXX

      Point origin = obj.Position; // Reference to Position of given entity

      return Program.EntityAt(origin.X, origin.Y - 1) // Check field over entity
          ?? Program.EntityAt(origin.X + 1, origin.Y) // Check field on the right side of entity
          ?? Program.EntityAt(origin.X, origin.Y + 1) // Check field under entity
          ?? Program.EntityAt(origin.X - 1, origin.Y);// Check field on the left side of entity
    }

    /// <summary>
    /// Determines whether the specified SnakeBasic.Entites.Entity is equal to the current SnakeBasic.Entites.Entity.
    /// </summary>
    /// <param name="obj">The entity to compare with the current entity.</param>
    public override bool Equals(object obj)
    {
      Entity ent = (Entity)obj; // Cast to access properties of 'obj'
      if (ReferenceEquals(null, obj)) return false; // obj shows to no heap space
      if (ReferenceEquals(this, obj)) return true; // obj and this shows to same heap space
      if (obj.GetType() != this.GetType()) return false; // Don't have the same type
      return this.Position.Equals(ent.Position) && this.RenderingChar == ent.RenderingChar;
    }
  }
}
