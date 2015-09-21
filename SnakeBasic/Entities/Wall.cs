using System.Drawing;

namespace SnakeBasic.Entities
{
	public class Wall : Entity
	{
		/// <summary>
		/// Initializes a new instance of SnakeBasic.Entities.Wall class.
		/// </summary>
		public Wall(Point position)
		{
			Coordinates.Add(position);
		}

		/// <summary>
		/// Gets a value indicating the rendering char of the entity.
		/// </summary>
		public override char RenderingChar => '#';
	}
}
