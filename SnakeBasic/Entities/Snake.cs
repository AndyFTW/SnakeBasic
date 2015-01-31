using SnakeBasic.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SnakeBasic.Entities
{
  public class Snake : Entity
  {
    public const char HEAD_CHARACTER = '@';

    /// <summary>
    /// Gets value indicating the position of the head..
    /// </summary>
    public Point HeadPosition { get; private set; }

    public override void Update()
    {
      base.Update();

      drawn[HeadPosition] = HEAD_CHARACTER;
    }

    Direction _moveDirection;
    /// <summary>
    /// Gets or sets a value indicating the direction the snake moves.
    /// </summary>
    public Direction MoveDirection
    {
      get { return _moveDirection; }
      set
      {
        if (IsValidMovementChange(value))
          _moveDirection = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of SnakeBasic.Entities.Snake class.
    /// </summary>
    public Snake(Point startingPoint, int length)
    {
      _moveDirection = Direction.Right; // Set default direction

      this.Coordinates = new List<Point>(); // Initialize

      this.HeadPosition = startingPoint;

      DrawHelper.Draw(this.HeadPosition, HEAD_CHARACTER); // Draw head the first time

      for (int i = length - 1; i > 0; i--) // -1 because head belongs to length
      {
        this.Coordinates.Add(new Point(startingPoint.X - i, startingPoint.Y));
        DrawHelper.Draw(startingPoint.X - i, startingPoint.Y, this.RenderingChar);
      }
    }

    /// <summary>
    /// Updates the position of the snake depending on the move direction.
    /// </summary>
    /// <param name="ateGoody">Indicates whether a goody was eaten by the snake.</param>
    public Entity UpdatePosition(bool ateGoody = false)
    {
      #region Update Body

      // Remove the last tail if no goody has been eaten.
      // If a goody has been eaten, do not remove last tail, then the snake becomes one unit longer
      if (!ateGoody)
      {
        var lastBodyPart = this.Coordinates.First();

        // Clear first body part
        DrawHelper.Draw(lastBodyPart, ' ');

        this.Coordinates.RemoveAt(0); // Remove the snaketail
      }

      this.Coordinates.Add(this.HeadPosition); // Convert head to part of body

      var convertedBodyPart = this.Coordinates.Last();
      DrawHelper.Draw(convertedBodyPart, this.RenderingChar);

      #endregion

      #region Update Head

      switch (this.MoveDirection)
      {
        case Direction.Up:
          this.HeadPosition = new Point(this.HeadPosition.X, this.HeadPosition.Y - 1);
          break;

        case Direction.Right:
          this.HeadPosition = new Point(this.HeadPosition.X + 1, this.HeadPosition.Y);
          break;

        case Direction.Down:
          this.HeadPosition = new Point(this.HeadPosition.X, this.HeadPosition.Y + 1);
          break;

        case Direction.Left:
          this.HeadPosition = new Point(this.HeadPosition.X - 1, this.HeadPosition.Y);
          break;
      }

      // These 'if'-clauses make sure it's possible for the snake to go through edges
      if (this.HeadPosition.X == -1)
        this.HeadPosition = new Point(Console.WindowWidth - 1, this.HeadPosition.Y);
      else if (this.HeadPosition.X == Console.WindowWidth)
        this.HeadPosition = new Point(0, this.HeadPosition.Y);
      else if (this.HeadPosition.Y == -1)
        this.HeadPosition = new Point(this.HeadPosition.X, Console.WindowHeight - 1);
      else if (this.HeadPosition.Y == Console.WindowHeight)
        this.HeadPosition = new Point(this.HeadPosition.X, 0);

      DrawHelper.Draw(this.HeadPosition, HEAD_CHARACTER);

      #endregion

      #region Collision Entity

      Entity collisionEntity = Program.EntityAt(this.HeadPosition);

      // Prevents that it always returns itself because it will always find its own head because we just moved it to this place.
      if (collisionEntity is Snake && collisionEntity.Equals(this))
      {
        if (this.Coordinates.Contains(this.HeadPosition))
          return this;

        return null;
      }

      return collisionEntity;

      #endregion
    }

    /// <summary>
    /// Returns a value indicating whether the movement directory change is valid.
    /// </summary>
    /// <param name="dir">Specifies the direction to check.</param>
    private bool IsValidMovementChange(Direction dir)
    {
      if (this.MoveDirection == Direction.Down && dir == Direction.Up)
        return false;

      if (this.MoveDirection == Direction.Up && dir == Direction.Down)
        return false;

      if (this.MoveDirection == Direction.Left && dir == Direction.Right)
        return false;

      if (this.MoveDirection == Direction.Right && dir == Direction.Left)
        return false;

      return true;
    }

    /// <summary>
    /// Gets a value indicating the rendering char of the entity.
    /// </summary>
    public override char RenderingChar
    {
      get { return '0'; }
    }
  }
}