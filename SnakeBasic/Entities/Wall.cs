using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeBasic.Entities
{
  public class Wall : Entity
  {
    /// <summary>
    /// Initializes a new instance of SnakeBasic.Entitys.Wall class.
    /// </summary>
    public Wall(Point position)
    {
      this.RenderingChar = '#';

      this.Position = position;
    }
  }
}
