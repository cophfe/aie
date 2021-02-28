using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

class ConsoleControl
{
	#region Input
	//----------------------------------------------------------------------------
	//-----------------------MANAGE CONSOLE INPUT---------------------------------
	//----------------------------------------------------------------------------

	static Coord mousePos = new Coord(0,0);
	public static bool shift = false, writing = false, escapePressedOnce = false;
	static Coord renderCoord;
	static TransparentConsoleColor[,] circle;
	public static List<ConsoleColor[,]> undoList = new List<ConsoleColor[,]>();
	

	[STAThread]
	public static void ReadInput(IntPtr inputHandle, InputRecord inputRecord = new InputRecord(), uint numberOfEventsRead = 0)
	{
		PInvoke.ReadConsoleInput(inputHandle, ref inputRecord, 1, ref numberOfEventsRead);

		switch (inputRecord.eventType)
		{
			case EventType.KEY_EVENT: //occurs when a key is pressed or let go
				KeyInput(inputRecord.eventRecord.keyEvent);
				break;
			case EventType.MOUSE_EVENT: //occurs when mouse is used
				MouseInput(inputRecord.eventRecord.mouseEvent);
				break;
			case EventType.WINDOW_BUFFER_SIZE_EVENT: //occurs when window buffer size is changed or set
				WindowsBufferSizeInput(inputRecord.eventRecord.windowBufferSizeEvent);
				break;
			case EventType.MENU_EVENT: //occurs when menu is used
				MenuInput(inputRecord.eventRecord.menuEvent);
				break;
			case EventType.FOCUS_EVENT: //occurs when window focus is changed
				FocusInput(inputRecord.eventRecord.focusEvent);
				break;
		}
	}
	static void MouseInput(MouseEventRecord mouseER)
	{
		switch (mouseER.dwEventFlags)
		{
			case MouseEventFlags.MOUSE_WHEELED:
				if (mouseER.dwButtonState > 0)
				{
					if (shift)
					{
						if (Program.color < (ConsoleColor)15)
							Program.color++;
						else
							Program.color = (ConsoleColor)0;


						circle = ConsoleRender.MakeCircle(Program.radius, mouseER.dwMousePosition, (TransparentConsoleColor)Program.color, mousePos, out renderCoord);
						ConsoleRender.Render(circle, renderCoord.X, renderCoord.Y);
					}
					else
					{

						Program.radius++;
						circle = ConsoleRender.MakeCircle(Program.radius, mouseER.dwMousePosition, (TransparentConsoleColor)Program.color, mousePos, out renderCoord);
						ConsoleRender.Render(circle, renderCoord.X, renderCoord.Y);
					}
				}
				else
				{
					if (shift)
					{
						if (Program.color > (ConsoleColor)0)
							Program.color--;
						else
							Program.color = (ConsoleColor)15;

						circle = ConsoleRender.MakeCircle(Program.radius, mouseER.dwMousePosition, (TransparentConsoleColor)Program.color, mousePos, out renderCoord);
						ConsoleRender.Render(circle, renderCoord.X, renderCoord.Y);

					}
					else
					{
						if (Program.radius > 1)
						{
							ConsoleRender.Render(ConsoleRender.MakeSquare(Program.radius, mouseER.dwMousePosition, (TransparentConsoleColor)(-1)), mouseER.dwMousePosition.X - 2 * Program.radius, mouseER.dwMousePosition.Y - Program.radius);


							Program.radius--;
							circle = ConsoleRender.MakeCircle(Program.radius, mouseER.dwMousePosition, (TransparentConsoleColor)Program.color, mousePos, out renderCoord);
							ConsoleRender.Render(circle, renderCoord.X, renderCoord.Y);
						}

					}
				}
				break;
			case MouseEventFlags.MOUSE_MOVED:
				
				circle = ConsoleRender.MakeCircle(Program.radius, mouseER.dwMousePosition, (TransparentConsoleColor)Program.color, mousePos, out renderCoord);
				ConsoleRender.Render(circle, renderCoord.X, renderCoord.Y);
				mousePos = new Coord(mouseER.dwMousePosition.X, mouseER.dwMousePosition.Y);
				if (writing) 
				{ 
					ConsoleRender.RasteriseCircle(Program.radius, new Coord((short)((mouseER.dwMousePosition.X - 2 * Program.radius) / 2), (short)(mouseER.dwMousePosition.Y - Program.radius)), Program.color, ConsoleRender.rasterisedScreen);
				}
				break;
			case MouseEventFlags.MOUSE_BUTTON:
				switch ((MouseButtonState)mouseER.dwButtonState)
				{
					case MouseButtonState.FROM_LEFT_1ST_BUTTON_PRESSED:
						undoList.Add(ConsoleRender.rasterisedScreen.Clone() as ConsoleColor[,]);
						ConsoleRender.RasteriseCircle(Program.radius, new Coord((short)((mouseER.dwMousePosition.X - 2 * Program.radius) / 2), (short)(mouseER.dwMousePosition.Y - Program.radius)), Program.color, ConsoleRender.rasterisedScreen);
						writing = true;
						break;

					case MouseButtonState.MOUSE_UNCLICKED:
						writing = false;
						break;
				}
				break;
			
		}
		if (mouseER.dwEventFlags == MouseEventFlags.MOUSE_MOVED)
		{
			
		}
		if (mouseER.dwEventFlags == MouseEventFlags.MOUSE_BUTTON)
		{
			
		}

	}

	static void KeyInput(KeyEventRecord keyER)
	{
		if (keyER.bKeyDown)
		{
			switch (keyER.wVirtualKeyCode)
			{
				case VirtualKeys.X:
					if (AreYouSure("exit", 'X'))
					{
						Program.drawing = false;
					}
					else
					{
						Program.drawing = false;
						Program.runStartScreen = false;
					}
					break;

				case VirtualKeys.Shift:
					shift = true;
					break;

				case VirtualKeys.Z:
					if (undoList.Count > 0)
					{
						ConsoleRender.rasterisedScreen = undoList[undoList.Count - 1].Clone() as ConsoleColor[,];
						undoList.RemoveAt(undoList.Count - 1);
						ConsoleRender.Render(ConsoleRender.nullScreen, 0, 0);
					}	
					break;

				case VirtualKeys.S:
					SaveFile();
					break;

				case VirtualKeys.R:

					if (AreYouSure("reset", 'R'))
					{
						undoList.Add(ConsoleRender.rasterisedScreen.Clone() as ConsoleColor[,]);
						ConsoleRender.rasterisedScreen = new ConsoleColor[ConsoleRender.rasterisedScreen.GetLength(0), ConsoleRender.rasterisedScreen.GetLength(1)];
						for (int x = 0; x < ConsoleRender.rasterisedScreen.GetLength(0); x++)
						{
							for (int y = 0; y < ConsoleRender.rasterisedScreen.GetLength(1); y++)
							{
								ConsoleRender.rasterisedScreen[x, y] = ConsoleColor.White;
							}
						}
						Program.drawing = false;
						Program.runStartScreen = false;
					}
					else
					{
						Program.drawing = false;
						Program.runStartScreen = false;
					}
					
					break;
			}
		}
		else
		{
			switch (keyER.wVirtualKeyCode)
			{
				case VirtualKeys.Shift:
					shift = false;
					break;
			}
		}
		
	}

	static void MenuInput(MenuEventRecord menuER)
	{
		//ignored
	}

	static void WindowsBufferSizeInput(WindowBufferSizeEventRecord windowER)
	{
		ConsoleRender.Render(ConsoleRender.nullScreen, 0, 0);
	}

	static void FocusInput(FocusEventRecord focusER)
	{
		//ignored
	}

	public static Coord GetCursorPos()
	{
		return mousePos;
	}
	#endregion

	#region Window Size
	//----------------------------------------------------------------------------
	//-----------------------------MANAGE WINDOW SIZE-----------------------------
	//----------------------------------------------------------------------------

	public static void SetFullscreen()
	{
		PInvoke.ShowWindow(PInvoke.GetConsoleWindow(), 3);
	}

	public static void SetWindowed()
	{
		PInvoke.ShowWindow(PInvoke.GetConsoleWindow(), 6);
	}
	#endregion

	#region Font
	//----------------------------------------------------------------------------
	//--------------------------------CHANGE FONT---------------------------------
	//----------------------------------------------------------------------------

	public static void SetCurrentFont(string font, short fontSize)
	{
		IntPtr h = PInvoke.GetStdHandle(-11);

		FontInfo set = new FontInfo
		{
			cbSize = Marshal.SizeOf<FontInfo>(),
			FontIndex = 0,
			FontFamily = 54,
			FontName = font,
			FontWeight = 400,
			FontSize = fontSize
		};

		PInvoke.SetCurrentConsoleFontEx(h, false, ref set);
	}
	#endregion

	public static void SaveFile()
	{
		using (Bitmap bmp = new Bitmap(ConsoleRender.rasterisedScreen.GetLength(0), ConsoleRender.rasterisedScreen.GetLength(1)))
		{
			Console.CursorVisible = true;
			for (int x = 0; x < bmp.Width; x++)
			{
				for (int y = 0; y < bmp.Height; y++)
				{
					bmp.SetPixel(x, y, ConsoleRender.GetColor(ConsoleRender.rasterisedScreen[x, y]));

				}
			}

			Console.BackgroundColor = ConsoleColor.Black;
			Console.Clear();
			SetCurrentFont("Consolas", 35);
			SetFullscreen();
			Console.WriteLine("File Name:");

			bool makingFile = true;
			while (Console.KeyAvailable)
				Console.ReadKey(true);
			string fileName = "";
			bool cancel = false;
			while (makingFile)
			{
				ConsoleKeyInfo key = Console.ReadKey(true);
				if (key.Key == ConsoleKey.Enter && fileName != null)
				{
					makingFile = false;
				}
				if (key.Key == ConsoleKey.Escape)
				{
					makingFile = false;
					cancel = true;
				}
				if (key.Key == ConsoleKey.Backspace && fileName.Length > 0)
				{
					fileName = fileName.Remove(fileName.Length - 1);
				}
				if (fileName.Length == 255)
					continue;
				if ((key.KeyChar > 63 && key.KeyChar < 124 && key.KeyChar != '[' && key.KeyChar != ']' && key.KeyChar != 92) || (key.KeyChar > 31 && key.KeyChar < 58 && key.KeyChar != '/' && key.KeyChar != '*' && key.KeyChar != '"' && key.KeyChar != '.') || key.KeyChar == '=')
				{
					fileName += key.KeyChar;
				}
				Console.SetCursorPosition(0, Console.CursorTop);
				Console.Write(new string(' ',fileName.Length+5));
				Console.SetCursorPosition(0, Console.CursorTop);
				Console.Write(fileName);

			}
			if (!cancel)
			{
				fileName.Replace('\\', ' ').Replace('/', ' ').Replace('.', ' ');
				Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Images");
				int i = 0;
				string initialFileName = fileName;
				while (File.Exists(Directory.GetCurrentDirectory() + @"\Images\" + $"{fileName.ToLower()}.png"))
				{
					fileName = initialFileName + i;
					i++;
				}

				bmp.Save(Directory.GetCurrentDirectory() + @"\Images\" + $"{fileName}.png", ImageFormat.Png);
				Console.WriteLine("\n\nFile saved. Press any key to return.");
				Console.ReadKey(true);

			}
			Program.drawing = false;
			Program.runStartScreen = false;
		}
	}

	static bool AreYouSure(string actionWord, char character)
	{
		Console.BackgroundColor = ConsoleColor.Black;
		Console.Clear();
		SetCurrentFont("Consolas", 35);
		SetFullscreen();
		while (Console.KeyAvailable)
			Console.ReadKey(true);

		Console.WriteLine($"Are you sure you want to {actionWord}?");
		Console.WriteLine($"(Press '{character}' again to {actionWord}, otherwise press 'Escape' to go back to canvas)");

		while (true)
		{
			ConsoleKeyInfo key = Console.ReadKey(true);
			if (key.Key == ConsoleKey.Escape)
			{
				return false;
			}
			else if (key.KeyChar.ToString().ToLower() == character.ToString().ToLower())
			{
				return true;
			}
			
		}
	}
}




