using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace ReadingWriting
{
	class InputRecorder
	{
		//Gets the cursor position in screen pixels
		[DllImport("user32.dll")]
		static extern bool GetCursorPos(
			out Point lpPoint
		);

		//Sets cursor position to a place in screen pixels
		[DllImport("user32.dll")]
		static extern bool SetCursorPos(int X, int Y);

		//A point
		[StructLayout(LayoutKind.Sequential)]
		public struct Point
		{
			public int X;
			public int Y;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct Rect
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool GetWindowRect(
			IntPtr hWnd, 
			out Rect lpRect);
		//might need to use coredll.dll

		[DllImport("Kernel32.dll")]
		static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll")]
		static extern IntPtr GetDesktopWindow();

		[DllImport("dwmapi.dll")]
		static extern int DwmGetWindowAttribute(IntPtr hWnd, int dwAttribute, out Rect lpRect, int cbAttribute);

		public static void MouseInput()
		{
			Rect screenBounds;
			Point pt = new Point();
			Console.CursorVisible = false;
			int x = 0, y = 0;
			GetWindowRect(GetConsoleWindow(), out screenBounds);
			float consoleResX = screenBounds.Right - screenBounds.Left, consoleResY = screenBounds.Bottom - screenBounds.Top;
			GetWindowRect(GetConsoleWindow(), out screenBounds);
			GetCursorPos(out pt);
			x = pt.X;
			y = pt.Y;

			int consoleWidth = Console.WindowWidth, consoleHeight = Console.WindowHeight;
			float normalizedX = (pt.X - screenBounds.Left) / consoleResX, normalizedY = (float)((pt.Y - screenBounds.Top) / consoleResY);
			//Console.WriteLine(normalizedX + ", " + normalizedY + ", " + x + ", " + y) ;
			if (normalizedX > 0 && normalizedX < 1 && normalizedY > 0 && normalizedY < 1)
			{

				Console.SetCursorPosition((int)(normalizedX * consoleWidth), (int)(normalizedY * consoleHeight));
				Console.Write('X');
			}
			System.Threading.Thread.Sleep(10);
			


		}

		public static void RestrictMouseToWindow()
		{
			Rect screenBounds;
			Point pt = new Point();
			//GetWindowRect(GetDesktopWindow(), out screenBounds);
			GetWindowRect(GetConsoleWindow(), out screenBounds);

			GetCursorPos(out pt);
			int x=pt.X, y=pt.Y;
			bool outOfBounds = false;
			

			// might need to look into DwmGetWindowAttribute if this is inaccurate
			Console.CursorVisible = false;
			

			if (pt.X < screenBounds.Left)
			{
				outOfBounds = true;
				x = screenBounds.Left;
			}
			else if (pt.X > screenBounds.Right)
			{
				outOfBounds = true;
				x = screenBounds.Right;
			}

			if (pt.Y < screenBounds.Top)
			{
				outOfBounds = true;
				y = screenBounds.Top;
			}
			else if (pt.Y > screenBounds.Bottom)
			{
				outOfBounds = true;
				y = screenBounds.Bottom;
			}
			if (outOfBounds)
				SetCursorPos(x, y);

			
		}
	}
}
