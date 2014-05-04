using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Timers;
using SnakeBasic.Entities;
using Timer = System.Timers.Timer;
using SnakeBasic.Helpers;

namespace SnakeBasic
{
  static partial class Program
  {
    /// <summary>
    /// Gets all entities.
    /// </summary>
    public static List<Entity> Entities
    {
      get
      {
        List<Entity> ents = new List<Entity>();
        ents.AddRange(Walls);
        ents.Add(ActiveSnake);
        ents.Add(ActiveCandy);
        return ents;
      }
    }

    /// <summary>
    /// Gets the timer who controls the movement interval of the snake.
    /// </summary>
    public static Timer SnakeTimer { get; private set; }

    /// <summary>
    /// Entry point.
    /// </summary>
    static void Main()
    {
      InitializeGame();

      Thread keepConsoleOpen = new Thread(() => Thread.Sleep(Timeout.InfiniteTimeSpan));
      keepConsoleOpen.Start(); // Keeps the console open
    }

    /// <summary>
    /// Returns the entity at specified point, if found; otherwise: null.
    /// </summary>
    /// <param name="position">The point of the entity to retrieve.</param>
    public static Entity EntityAt(Point position)
    {
      Entity result = Entities.FirstOrDefault(x => x.Position == position);
      if (result != null)
        return result;

      return ActiveSnake.Position.Contains(position) ? ActiveSnake : null;
    }

    /// <summary>
    /// Returns the entity at specified coordinates, if found; otherwise: null.
    /// </summary>
    /// <param name="x">The x-coordinate of the entity to retrieve.</param>
    /// <param name="y">The y-coordinate of the entity to retrieve.</param>
    public static Entity EntityAt(int x, int y)
    {
      return EntityAt(new Point(x, y));
    }

    /// <summary>
    /// Initializes necessary components.
    /// </summary>
    static void InitializeGame()
    {
      Console.Clear(); // Make sure there is nothing
      Console.BufferWidth = Console.WindowWidth = 35;
      Console.BufferHeight = Console.WindowHeight + 1;
      Console.Title = "SnakeBasic";
      Console.CursorVisible = false;

      Walls = new List<Wall>(); // Initialize
      ActiveSnake = new Snake(new Point(10, 10), 4); // Initialize snake

      Walls = Helper1.LoadWalls(WallFormation.FourCrosses | WallFormation.Rectangle);

      ActiveCandy = new Candy();

      #region KeyDown Thread

      // Clear existing characters from the buffer
      while (Console.KeyAvailable)
      {
        Console.ReadKey(true);
      }

      keyThread = new Thread(KeyDownLoop);
      keyThread.IsBackground = true;
      keyThread.Start();

      #endregion

      // Initialize timer, subscribe and start
      SnakeTimer = new Timer(100.0);
      SnakeTimer.Elapsed += timer_Elapsed;
      SnakeTimer.AutoReset = false;
      SnakeTimer.Start();
    }

    static void timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      /*========== Update position of snakes ==========*/

      bool goodyEaten = ActiveSnake.Head == ActiveCandy.Position; // Checks whether the candy has been eaten

      Entity collisionEntity = ActiveSnake.UpdatePosition(ActiveSnake.Head == ActiveCandy.Position); // Can't use 'goodyEaten' here because when the first snake made the bool variable true, it will stay true, even if the next snake didn't eat a candy

      if (collisionEntity is Wall || collisionEntity is Snake) // If snake collided with a wall or itself, the game is over
      {
        GameOver();
      }

      /*==================================================*/
      /*==================================================*/

      if (goodyEaten)
      {
        ActiveCandy = new Candy();
        if (SnakeTimer.Interval > 20.0)
          SnakeTimer.Interval -= 1.0;
      }

      SnakeTimer.Start();
    }





    /// <summary>
    /// Shows the game over dialog.
    /// </summary>
    static void GameOver()
    {
      SnakeTimer.Elapsed -= timer_Elapsed; // Necessary, otherwise the event might fire one more time!
      SnakeTimer.Stop();
      SnakeTimer.Close();
      SnakeTimer = new Timer();

      Console.Clear();
      Console.CursorVisible = true;

      Console.WriteLine();
      Console.WriteLine("GameOver");
      Console.WriteLine();
      Console.WriteLine("Repeat?");
      Console.Write("[Y,N]: ");

      keyThread.Abort();


      string answer = Console.ReadLine();

      if (answer.Length > 0)
      {
        char key = answer.ToUpper()[0];

        if (key == 'Y')
          InitializeGame();
        else
          Environment.Exit(0);
      }
    }

    static void KeyDownLoop()
    {
      while (true)
      {
        if (Console.KeyAvailable)
        {
          ConsoleKeyInfo key = Console.ReadKey(true);

          switch (key.Key)
          {
            case ConsoleKey.DownArrow:
              if (ActiveSnake.IsValidMovementChange(Direction.Down))
                ActiveSnake.QueuedDirection = Direction.Down;
              break;

            case ConsoleKey.LeftArrow:
              if (ActiveSnake.IsValidMovementChange(Direction.Left))
                ActiveSnake.QueuedDirection = Direction.Left;
              break;

            case ConsoleKey.RightArrow:
              if (ActiveSnake.IsValidMovementChange(Direction.Right))
                ActiveSnake.QueuedDirection = Direction.Right;
              break;

            case ConsoleKey.UpArrow:
              if (ActiveSnake.IsValidMovementChange(Direction.Up))
                ActiveSnake.QueuedDirection = Direction.Up;
              break;
          }
        }
      }
    }
  }
}