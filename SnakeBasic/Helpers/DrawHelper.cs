using Microsoft.Win32.SafeHandles;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace SnakeBasic.Helpers
{
    public static class DrawHelper
    {
        #region Native methods

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
                string fileName,
                [MarshalAs(UnmanagedType.U4)] uint fileAccess,
                [MarshalAs(UnmanagedType.U4)] uint fileShare,
                IntPtr securityAttributes,
                [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
                [MarshalAs(UnmanagedType.U4)] int flags,
                IntPtr template);

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            public short X;
            public short Y;

            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutputCharacter(
            SafeFileHandle hConsoleOutput,
            string lpCharacter,
            int nLength,
            Coord dwWriteCoord,
            ref int lpumberOfCharsWritten);

        #endregion

        /// <summary>
        /// Specifies the handle to the output buffer of the console.
        /// </summary>
        static SafeFileHandle consoleHandle;

        static DrawHelper()
        {
            consoleHandle = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
        }

        /// <summary>
        /// Draws a specified char to a specified point.
        /// </summary>
        /// <param name="position">Specifies the point the char should get drawn to.</param>
        /// <param name="renderingChar">Specifies the char which should get drawn.</param>
        public static void Draw(Point position, char renderingChar)
        {
            Draw(position.X, position.Y, renderingChar);
        }

        /// <summary>
        /// Draws a specified char to specified coordinates.
        /// </summary>
        /// <param name="x">Specifies the x coordinate of the point where the char should get drawn.</param>
        /// <param name="y">Specifies the y coordinate of the point where the char should get drawn.</param>
        /// <param name="renderingChar">Specifies the char which should get drawn.</param>
        public static void Draw(int x, int y, char renderingChar)
        {
            // Draw with this native method because this method does NOT move the cursor.
            // So even if writing to the last column of the last row the cursor does not move into next line (outside of window size) and break the game.
            int n = 0;
            WriteConsoleOutputCharacter(consoleHandle, renderingChar.ToString(), 1, new Coord((short)x, (short)y), ref n);
        }
    }
}
