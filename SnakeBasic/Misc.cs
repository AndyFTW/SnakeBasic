using SnakeBasic.Entities;
using System.Collections.Generic;
using System.Threading;

namespace SnakeBasic
{
    public static partial class Program
    {
        /// <summary>
        /// Offers option to capture the last clicked key asynchronous.
        /// </summary>
        static Thread keyThread;

        /// <summary>
        /// Gets or sets all existing walls.
        /// </summary>
        public static List<Wall> Walls { get; set; }

        /// <summary>
        /// Gets or sets all existing snakes.
        /// </summary>
        public static Snake ActiveSnake { get; set; }

        /// <summary>
        /// Gets or sets the current goody.
        /// </summary>
        public static Candy ActiveCandy { get; set; }
    }
}
