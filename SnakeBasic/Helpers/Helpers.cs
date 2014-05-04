using SnakeBasic.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SnakeBasic.Helpers
{
  public static class LevelHelper
  {
    /// <summary>
    /// Creates and loads walls.
    /// </summary>
    /// <param name="formation">Specifies the map construction to load.</param>
    public static List<Wall> LoadWalls(WallFormation formation)
    {
      #region Create Walls

      #region Rectangle

      List<Wall> walls = new List<Wall>();;

      if (formation.HasFlag(WallFormation.Rectangle))
      {
        for (int i = 0; i < Console.WindowWidth; i++)
        {
          walls.Add(new Wall(new Point(i, 0)));

          walls.Add(new Wall(new Point(i, Console.WindowHeight - 1)));

          DrawHelper.Draw(walls[walls.Count - 2].Position, walls[walls.Count - 2].RenderingChar);
          DrawHelper.Draw(walls.Last().Position, walls.Last().RenderingChar);
        }

        for (int i = 1; i < Console.WindowHeight; i++) // Start by one, because [0,0] already has been added with first loop
        {
          walls.Add(new Wall(new Point(0, i)));

          walls.Add(new Wall(new Point(Console.WindowWidth - 1, i)));
        }
      }

      #endregion

      #region 4 Crosses

      if (formation.HasFlag(WallFormation.FourCrosses))
      {
        var newWalls = new List<Wall>
                    {
                        #region Cross 1

                        new Wall(new Point(10, 5)),
                        new Wall(new Point(10, 6)),
                        new Wall(new Point(10, 7)),
                        new Wall(new Point(10, 8)),
                        new Wall(new Point(10, 9)),

                        new Wall(new Point(8, 7)),
                        new Wall(new Point(9, 7)),
                        //new Wall(new Point(10, 7)), Already added
                        new Wall(new Point(11, 7)),
                        new Wall(new Point(12, 7)),

                        #endregion

                        #region Cross 2

                        new Wall(new Point(25, 5)),
                        new Wall(new Point(25, 6)),
                        new Wall(new Point(25, 7)),
                        new Wall(new Point(25, 8)),
                        new Wall(new Point(25, 9)),

                        new Wall(new Point(23, 7)),
                        new Wall(new Point(24, 7)),
                        //new Wall(new Point(25, 7)), Already added
                        new Wall(new Point(26, 7)),
                        new Wall(new Point(27, 7)),

                        #endregion

                        #region Cross 3

                        new Wall(new Point(10, 15)),
                        new Wall(new Point(10, 16)),
                        new Wall(new Point(10, 17)),
                        new Wall(new Point(10, 18)),
                        new Wall(new Point(10, 19)),

                        new Wall(new Point(8, 17)),
                        new Wall(new Point(9, 17)),
                        //new Wall(new Point(10, 17)), Already added
                        new Wall(new Point(11, 17)),
                        new Wall(new Point(12, 17)),

                        #endregion

                        #region Cross 4

                        new Wall(new Point(25, 15)),
                        new Wall(new Point(25, 16)),
                        new Wall(new Point(25, 17)),
                        new Wall(new Point(25, 18)),
                        new Wall(new Point(25, 19)),

                        new Wall(new Point(23, 17)),
                        new Wall(new Point(24, 17)),
                        //new Wall(new Point(25, 17)), Already added
                        new Wall(new Point(26, 17)),
                        new Wall(new Point(27, 17))

                        #endregion
                    };

        walls.AddRange(newWalls);

      }

      #endregion

      #endregion

      #region Draw Walls

      foreach (Wall wall in walls)
      {
        DrawHelper.Draw(wall.Position, wall.RenderingChar);
      }

      #endregion

      return walls;
    }

    /// <summary>
    /// Returns a new point in the visible console window.
    /// </summary>
    public static Point GetRandomPoint()
    {
      Random rand = new Random();
      int x = rand.Next(Console.WindowWidth - 1);
      int y = rand.Next(Console.WindowHeight - 1);
      // -1 because the first console line has the index 0, but belongs to the amount of lines, so WindowHeight = 1 after counting first line.

      return new Point(x, y);
    }
  }
}
