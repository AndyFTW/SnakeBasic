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

			Drawn[HeadPosition] = HEAD_CHARACTER;
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

			Coordinates = new List<Point>(); // Initialize

			HeadPosition = startingPoint;

			DrawHelper.Draw(HeadPosition, HEAD_CHARACTER); // Draw head the first time

			for (int i = length - 1; i > 0; i--) // -1 because head belongs to length
			{
				Coordinates.Add(new Point(startingPoint.X - i, startingPoint.Y));
				DrawHelper.Draw(startingPoint.X - i, startingPoint.Y, RenderingChar);
			}
		}

		/// <summary>
		/// Updates the position of the snake depending on the move direction.
		/// </summary>
		/// <param name="ateCandy">Indicates whether a candy was eaten by the snake.</param>
		public Entity UpdatePosition(bool ateCandy = false)
		{
			#region Update Body

			// Remove the last tail if no candy has been eaten.
			// If a candy has been eaten, do not remove last tail, then the snake becomes one unit longer
			if (!ateCandy)
			{
				var lastBodyPart = Coordinates.First();

				// Clear first body part
				DrawHelper.Draw(lastBodyPart, ' ');

				Coordinates.RemoveAt(0); // Remove the snaketail
			}

			Coordinates.Add(HeadPosition); // Convert head to part of body

			var convertedBodyPart = Coordinates.Last();
			DrawHelper.Draw(convertedBodyPart, RenderingChar);

			#endregion

			#region Update Head

			switch (MoveDirection)
			{
				case Direction.Up:
					HeadPosition = new Point(HeadPosition.X, HeadPosition.Y - 1);
					break;

				case Direction.Right:
					HeadPosition = new Point(HeadPosition.X + 1, HeadPosition.Y);
					break;

				case Direction.Down:
					HeadPosition = new Point(HeadPosition.X, HeadPosition.Y + 1);
					break;

				case Direction.Left:
					HeadPosition = new Point(HeadPosition.X - 1, HeadPosition.Y);
					break;
			}

			// These 'if'-clauses make sure it's possible for the snake to go through edges
			if (HeadPosition.X == -1)
				HeadPosition = new Point(Console.WindowWidth - 1, HeadPosition.Y);
			else if (HeadPosition.X == Console.WindowWidth)
				HeadPosition = new Point(0, HeadPosition.Y);
			else if (HeadPosition.Y == -1)
				HeadPosition = new Point(HeadPosition.X, Console.WindowHeight - 1);
			else if (HeadPosition.Y == Console.WindowHeight)
				HeadPosition = new Point(HeadPosition.X, 0);

			DrawHelper.Draw(HeadPosition, HEAD_CHARACTER);

			#endregion

			#region Collision Entity

			Entity collisionEntity = Program.EntityAt(HeadPosition);

			// Prevents that it always returns itself because it will always find its own head because we just moved it to this place.
			if (collisionEntity is Snake && collisionEntity.Equals(this))
			{
				return Coordinates.Contains(HeadPosition) ? this : null;
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
			if (MoveDirection == Direction.Down && dir == Direction.Up)
				return false;

			if (MoveDirection == Direction.Up && dir == Direction.Down)
				return false;

			if (MoveDirection == Direction.Left && dir == Direction.Right)
				return false;

			if (MoveDirection == Direction.Right && dir == Direction.Left)
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