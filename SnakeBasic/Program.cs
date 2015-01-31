using SnakeBasic.Entities;
using SnakeBasic.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace SnakeBasic
{
  static class Program
  {
    /// <summary>
    /// Gets or sets all existing walls.
    /// </summary>
    public static List<Wall> Walls { get; set; }

    /// <summary>
    /// Gets or sets the snake.
    /// </summary>
    public static Snake ActiveSnake { get; set; }

    /// <summary>
    /// Gets or sets the current goody.
    /// </summary>
    public static Candy ActiveCandy { get; set; }

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
    /// Gets the timer which controls the movement interval of the snake.
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
      return Entities.FirstOrDefault(x => x.Coordinates.Contains(position));
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
      Console.BufferHeight = Console.WindowHeight + 1; // + 1 necessary because without the scroll bar jumps if the snake crosses the edges in the last column
      Console.SetWindowSize(Console.BufferWidth, Console.BufferHeight); // Set window size to buffer size to avoid scroll bars
      Console.Title = "SnakeBasic";
      Console.CursorVisible = false;

      Walls = new List<Wall>(); // Initialize
      ActiveSnake = new Snake(new Point(10, 10), 4); // Initialize snake

      Walls = LevelHelper.LoadWalls(WallFormation.FourCrosses);

      ActiveCandy = new Candy();

      // Clear existing characters from the buffer
      while (Console.KeyAvailable)
      {
        Console.ReadKey(true);
      }

      // Initialize timer, subscribe and start
      SnakeTimer = new Timer(100.0);
      SnakeTimer.Elapsed += timer_Elapsed;
      SnakeTimer.AutoReset = false;
      SnakeTimer.Start();
    }

    static void timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      HandleLastKey();

      #region Update position of snake

      bool goodyEaten = ActiveSnake.HeadPosition == ActiveCandy.Coordinates[0]; // Checks whether the candy has been eaten

      Entity collisionEntity = ActiveSnake.UpdatePosition(goodyEaten);

      if (collisionEntity is Wall || collisionEntity is Snake) // If snake collided with a wall or itself, the game is over
      {
        GameOver();
        return;
      }

      #endregion


      // If goody eaten, we increase the speed by 1 ms
      // Minimum interval is 20 ms
      if (goodyEaten)
      {
        ActiveCandy = new Candy();
        if (SnakeTimer.Interval > 20.0)
          SnakeTimer.Interval -= 1.0;
      }

      SnakeTimer.Start();
    }



    /// <summary>
    /// Handles the last key if there is one.
    /// </summary>
    static void HandleLastKey()
    {
      while (Console.KeyAvailable)
      {
        ConsoleKeyInfo key = Console.ReadKey(true);

        // Only process key if it is the last one.
        // We only want to the handle the last clicked key.
        if (Console.KeyAvailable) continue;

        switch (key.Key)
        {
          case ConsoleKey.DownArrow:
            ActiveSnake.MoveDirection = Direction.Down;
            break;

          case ConsoleKey.LeftArrow:
            ActiveSnake.MoveDirection = Direction.Left;
            break;

          case ConsoleKey.RightArrow:
            ActiveSnake.MoveDirection = Direction.Right;
            break;

          case ConsoleKey.UpArrow:
            ActiveSnake.MoveDirection = Direction.Up;
            break;
        }
      }
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
  }
}