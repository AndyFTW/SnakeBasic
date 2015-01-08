using System.Collections.Generic;
using System.Drawing;

namespace SnakeBasic.Entities
{
  public abstract class Entity
  {
    /// <summary>
    /// Gets a value indicating the rendering char of the entity.
    /// </summary>
    public abstract char RenderingChar { get; }


    public Entity()
    {
      this.Coordinates = new List<Point>();
    }

    public List<Point> Coordinates { get; set; }

    protected Dictionary<Point, char> drawn = new Dictionary<Point, char>();

    public Dictionary<Point, char> Drawn
    {
      get { return drawn; }
    }

    public virtual void Update()
    {
      drawn.Clear();
      foreach (var coord in this.Coordinates)
      {
        drawn.Add(coord, this.RenderingChar);
      }
    }

    /// <summary>
    /// Gets a value indicating the length of the snake. Including the head.
    /// </summary>
    public int Length
    {
      get { return this.Coordinates.Count; }
    }

    #region Position

    /// <summary>
    /// Changes the X-Position of the entity.
    /// </summary>
    /// <param name="x">Determinates the size of the shifting.</param>
    public void MovePosX(int x)
    {
      for (int i = 0; i < this.Coordinates.Count; i++)
      {
        this.Coordinates[i] = new Point(this.Coordinates[i].X + x, this.Coordinates[i].Y);
      }
    }

    /// <summary>
    /// Changes the Y-Position of the entity.
    /// </summary>
    /// <param name="y">Determinates the size of the shifting.</param>
    public void MovePosY(int y)
    {
      for (int i = 0; i < this.Coordinates.Count; i++)
      {
        this.Coordinates[i] = new Point(this.Coordinates[i].X, this.Coordinates[i].Y + y);
      }
    }

    #endregion

    /// <summary>
    /// Finds the first entity beeing in the close range of the given entity, if found; otherwise: null.
    /// </summary>
    /// <param name="obj">Specifies the origin entity.</param>
    /// <remarks>The search is performed clockwise.</remarks>
    public Entity FindNextEntity()
    {
      // obj: O
      // to search points: X

      // XXX
      // XOX
      // XXX

      foreach (var coord in this.Coordinates)
      {
        var result = Program.EntityAt(coord.X, coord.Y - 1) // Check field over entity
            ?? Program.EntityAt(coord.X + 1, coord.Y) // Check field on the right side of entity
            ?? Program.EntityAt(coord.X, coord.Y + 1) // Check field under entity
            ?? Program.EntityAt(coord.X - 1, coord.Y);// Check field on the left side of entity

        if (result != null && result != this)
          return result;
      }

      return null;
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
      return this.Coordinates.Equals(ent.Coordinates) && this.RenderingChar == ent.RenderingChar;
    }
  }
}
