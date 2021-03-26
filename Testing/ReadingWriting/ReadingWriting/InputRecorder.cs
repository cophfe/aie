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
		static Point lastMouse, currentMouse;
		const float mouseSpeed = 1;

		public InputRecorder()
		{
			//SetCursorPos(960, 540);
			GetCursorPos(out lastMouse);
		}
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

			public Point(int x, int y)
			{
				X = x;
				Y = y;
			}
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

		[DllImport("user32.dll")]
		public static extern int ShowCursor(bool bShow);

		[DllImport("user32.dll")]
		public static extern int SetCursor(bool bShow, uint dword);

		public void MouseInput()
		{
			Console.CursorVisible = true;
			Rect screenBounds;
			GetWindowRect(GetConsoleWindow(), out screenBounds);

			GetCursorPos(out currentMouse);
			//SetCursorPos(960, 540);
			Point deltaMouse = new Point(currentMouse.X - lastMouse.X, currentMouse.Y - lastMouse.Y);
			//Console.WriteLine($"x: {deltaMouse.X}\ny: {deltaMouse.Y}");
			lastMouse = currentMouse;

			Random rand = new Random();
			Console.SetCursorPosition(Math.Max(Math.Min(Console.CursorLeft + (int)(deltaMouse.X/mouseSpeed), Console.WindowWidth-1),0), Math.Max(Math.Min(Console.CursorTop + (int)(deltaMouse.Y/mouseSpeed/2), Console.WindowHeight-1), 0));
			//System.Threading.Thread.Sleep(10);
			Console.Write((char)rand.Next(0,120));
			//Console.SetCursorPosition(Console.CursorLeft-1,Console.CursorTop);

		}

		
		public void RestrictMouseToWindow()
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
