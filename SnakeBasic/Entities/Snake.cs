using SnakeBasic.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SnakeBasic.Entities
{
    public class Snake : Entity
    {
        /// <summary>
        /// Gets value indicating the position of the head. The body automatic fits.
        /// </summary>
        public Point Head { get; private set; }

        /// <summary>
        /// Gets a list of points containing the positions of the body.
        /// </summary>
        public List<Point> Body { get; private set; }

        /// <summary>
        /// Gets a list of all points the snake uses. The head is always the last point.
        /// </summary>
        public new List<Point> Position
        {
            get
            {
                return new List<Point>(this.Body)
                    {
                        this.Head
                    };
            }
        }

        /// <summary>
        /// Gets a value indicating the length of the snake. Including the head.
        /// </summary>
        public int Length
        {
            get { return this.Position.Count; }
        }

        /// <summary>
        /// Gets or sets a value indicating the rendering char of the head of the snake.
        /// </summary>
        public char HeadRenderingChar { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the direction the snake moves.
        /// </summary>
        public Direction MoveDirection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the next direction the snake moves.
        /// </summary>
        public Direction QueuedDirection { get; set; }

        /// <summary>
        /// Initializes a new instance of SnakeBasic.Entitys.Snake class.
        /// </summary>
        public Snake(Point startingPoint, int length)
        {
            this.RenderingChar = '0';
            this.HeadRenderingChar = '@';

            this.MoveDirection = this.QueuedDirection = Direction.Right; // Set default direction

            this.Body = new List<Point>(); // Initialize

            this.Head = startingPoint;

            DrawHelper.Draw(this.Head, this.HeadRenderingChar); // Draw head the first time

            for (int i = length - 1; i > 0; i--) // -1 because head belongs to length
            {
                this.Body.Add(new Point(startingPoint.X - i, startingPoint.Y));
                DrawHelper.Draw(startingPoint.X - i, startingPoint.Y, this.RenderingChar);
            }
        }

        /// <summary>
        /// Updates the position of the snake depending on the move direction.
        /// </summary>
        /// <param name="ateGoody">Indicates whether a goody was eaten by the snake.</param>
        public Entity UpdatePosition(bool ateGoody = false)
        {
            this.MoveDirection = this.QueuedDirection; // Assign pending direction

            #region Update Body

            if (!ateGoody) // Don't remove it, then the snake gets one unit longer
            {
                var firstBodyPart = this.Body.First();

                DrawHelper.Draw(firstBodyPart, ' ');

                this.Body.RemoveAt(0); // Remove the snaketail
                // or
                // this.Body.Remove(this.Body.First());
                // but the first solution is faster 
            }

            this.Body.Add(this.Head); // Convert head to part of body

            var convertedBodyPart = this.Body.Last();
            DrawHelper.Draw(convertedBodyPart, this.RenderingChar);

            #endregion

            #region Update Head

            switch (this.MoveDirection)
            {
                case Direction.Up:
                    this.Head = new Point(this.Head.X, this.Head.Y - 1);
                    break;

                case Direction.Right:
                    this.Head = new Point(this.Head.X + 1, this.Head.Y);
                    break;

                case Direction.Down:
                    this.Head = new Point(this.Head.X, this.Head.Y + 1);
                    break;

                case Direction.Left:
                    this.Head = new Point(this.Head.X - 1, this.Head.Y);
                    break;
            }

            // These 'if'-clauses make sure it's possible for the snake to go through the walls
            if (this.Head.X == -1)
                this.Head = new Point(Console.WindowWidth - 1, this.Head.Y);
            else if (this.Head.X == Console.WindowWidth)
                this.Head = new Point(0, this.Head.Y);
            else if (this.Head.Y == -1)
                this.Head = new Point(this.Head.X, Console.WindowHeight - 1);
            else if (this.Head.Y == Console.WindowHeight)
                this.Head = new Point(this.Head.X, 0);

            DrawHelper.Draw(this.Head, this.HeadRenderingChar);

            #endregion

            #region Collision Entity

            Entity collisionEntity = Program.EntityAt(this.Head);

            // Prevents that it always returns itself because it will always find its own head because we just moved it to this place.
            if (collisionEntity is Snake && collisionEntity.Equals(this))
            {
                if (this.Body.Contains(this.Head))
                    return this;

                return null;
            }

            return collisionEntity;

            #endregion
        }

        /// <summary>
        /// Returns a value indicating whether the movement directory change is valid.
        /// </summary>
        /// <param name="dir">Specifies the new direction.</param>
        public bool IsValidMovementChange(Direction dir)
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
    }
}
