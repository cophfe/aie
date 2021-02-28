using System;
using System.Numerics;

class Program
{
	public static bool running = true;
	public static bool drawing = true;
	public static bool runStartScreen = true;

	public static int radius = 10;
	public static ConsoleColor color = ConsoleColor.Black;
	static void Main(string[] args)
	{
		while(running)
		{
			radius = 10;
			color = ConsoleColor.Black;

			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;

			//gets standard input handle
			IntPtr inputHandle = PInvoke.GetStdHandle(-10);
			//disables quick edit mode
			PInvoke.SetConsoleMode(inputHandle, 0x0080);
			//enables input without console.read and enables mouse input
			PInvoke.SetConsoleMode(inputHandle, 0x0008 | 0x0010);
			Console.CursorVisible = false;
			
			drawing = true;

			if (runStartScreen)
			{
				StartScreen();
				ConsoleControl.undoList.Clear();
				ConsoleControl.undoList.TrimExcess();

			}

			

			Console.BackgroundColor = ConsoleColor.White;
			Console.Clear();
			Console.CursorVisible = false;

			//sets console font
			ConsoleControl.SetCurrentFont("NSimSun", 5);
			//makes the console fullscreen
			ConsoleControl.SetWindowed(); //its bugged and sometimes does not realise the console changed size (due to font change) unless I window it temporarily
			ConsoleControl.SetFullscreen();
			//removes the scroll bar
			Console.BufferHeight = Console.WindowHeight;

			
			int w = Console.LargestWindowWidth / 2, h = Console.LargestWindowHeight;
			ConsoleRender.nullScreen = new TransparentConsoleColor[w, h];
			if (runStartScreen)
				ConsoleRender.rasterisedScreen = new ConsoleColor[w, h];

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					ConsoleRender.nullScreen[x, y] = TransparentConsoleColor.Null;
					if (runStartScreen)
						ConsoleRender.rasterisedScreen[x, y] = ConsoleColor.White;
				}
			}
			if (!runStartScreen)
				ConsoleRender.Render(ConsoleRender.nullScreen, 0, 0);
			runStartScreen = true;
			while (drawing)
			{

				ConsoleControl.ReadInput(inputHandle);

			}
		}
		

	}

	public static void StartScreen()
	{

		Console.CursorVisible = false;
		string title =
			"\n\n\n\n" +
			"	PPPPPPPPPPPPPPPPP                        iiii                              tttt          \n"+
			"	P::::::::::::::::P                      i::::i                          ttt:::t          \n" +
			"	P::::::PPPPPP:::::P                      iiii                           t:::::t          \n" +
			"	PP:::::P     P:::::P                                                    t:::::t          \n" +
			"	  P::::P     P:::::P  aaaaaaaaaaaaa    iiiiiii  nnnn  nnnnnnnn     ttttttt:::::ttttttt    \n" +
			"	  P::::P     P:::::P  a::::::::::::a   i:::::i  n:::nn::::::::nn   t:::::::::::::::::t    \n" +
			"	  P::::PPPPPP:::::P   aaaaaaaaa:::::a   i::::i  n::::::::::::::nn  t:::::::::::::::::t    \n" +
			"	  P:::::::::::::PP             a::::a   i::::i  nn:::::::::::::::n tttttt:::::::tttttt    \n" +
			"	  P::::PPPPPPPPP        aaaaaaa:::::a   i::::i    n:::::nnnn:::::n       t:::::t          \n" +
			"	  P::::P              aa::::::::::::a   i::::i    n::::n    n::::n       t:::::t          \n" +
			"	  P::::P             a::::aaaa::::::a   i::::i    n::::n    n::::n       t:::::t          \n" +
			"	  P::::P            a::::a    a:::::a   i::::i    n::::n    n::::n       t:::::t    tttttt\n" +
			"	PP::::::PP          a::::a    a:::::a  i::::::i   n::::n    n::::n       t::::::tttt:::::t\n" +
			"	P::::::::P          a:::::aaaa::::::a  i::::::i   n::::n    n::::n       tt::::::::::::::t\n" +
			"	P::::::::P           a::::::::::aa:::a i::::::i   n::::n    n::::n         tt:::::::::::tt\n" +
			"	PPPPPPPPPP            aaaaaaaaaa  aaaa iiiiiiii   nnnnnn    nnnnnn           ttttttttttt  \n\n";

		
		string subTitle = 
		"	███████╗ ██████╗ ██████╗      ██████╗ ██████╗ ███╗   ██╗███████╗ ██████╗ ██╗     ███████╗\n" +
		"	██╔════╝██╔═══██╗██╔══██╗    ██╔════╝██╔═══██╗████╗  ██║██╔════╝██╔═══██╗██║     ██╔════╝\n" +
		"	█████╗  ██║   ██║██████╔╝    ██║     ██║   ██║██╔██╗ ██║███████╗██║   ██║██║     █████╗  \n" +
		"	██╔══╝  ██║   ██║██╔══██╗    ██║     ██║   ██║██║╚██╗██║╚════██║██║   ██║██║     ██╔══╝  \n" +
		"	██║     ╚██████╔╝██║  ██║    ╚██████╗╚██████╔╝██║ ╚████║███████║╚██████╔╝███████╗███████╗\n" +
		"	╚═╝      ╚═════╝ ╚═╝  ╚═╝     ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚══════╝╚══════╝\n";

		
		
		string newFile =
			"\t" + @"  _  _ _____      __  ___ ___ _    ___  " + "\n"+
			"\t" + @" | \| | __\ \    / / | __|_ _| |  | __| " + "\n"+
			"\t" + @" |  \ | _| \ \/\/ /  | _| | || |__| _|  " + "\n"+
			"\t" + @" |_|\_|___| \_/\_/   |_| |___|____|___| " + "\n"+
			"\t" + @"                                        ";

		
		string openFile =
			"\t" + @"   ___  ___ ___ _  _   ___ ___ _    ___  " + "\n"+
			"\t" + @"  / _ \| _ \ __| \| | | __|_ _| |  | __| " + "\n"+
			"\t" + @" | (_) |  _/ _|| .` | | _| | || |__| _|  " + "\n"+
			"\t" + @"  \___/|_| |___|_|\_| |_| |___|____|___| " + "\n"+
			"\t" + @"                                         ";

		string exit =

			"\t" + @"  _____  _____ _____  " + "\n" +
			"\t" + @" | __\ \/ /_ _|_   _| " + "\n" +
			"\t" + @" | _| >  < | |  | |   " + "\n" +
			"\t" + @" |___/_/\_\___| |_|   " + "\n" +
			"\t" + @"                      ";



		Console.BackgroundColor = ConsoleColor.Black;
		Console.ForegroundColor = ConsoleColor.White;
		Console.Clear();
		ConsoleControl.SetCurrentFont("Consolas", 15);
		Console.WriteLine(title);
		ConsoleControl.SetFullscreen();

		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine(subTitle);

		Console.BackgroundColor = ConsoleColor.DarkGray;
		Console.ForegroundColor = ConsoleColor.White;
		Console.WriteLine('\n');
		Coord newStartCoord = new Coord(0, Console.CursorTop);
		Console.Write(newFile);
		Coord newEndCoord = new Coord(Console.CursorLeft, Console.CursorTop);
		Console.WriteLine('\n');

		Console.BackgroundColor = ConsoleColor.DarkGray;
		Console.ForegroundColor = ConsoleColor.White;
		Coord openStartCoord = new Coord(0, Console.CursorTop);
		Console.Write(openFile);
		Coord openEndCoord = new Coord(Console.CursorLeft, Console.CursorTop);
		Console.WriteLine('\n');

		Console.BackgroundColor = ConsoleColor.DarkGray;
		Console.ForegroundColor = ConsoleColor.White;
		Coord exitStartCoord = new Coord(0, Console.CursorTop);
		Console.Write(exit);
		Coord exitEndCoord = new Coord(Console.CursorLeft, Console.CursorTop);
		Console.WriteLine('\n');

		Console.BackgroundColor = ConsoleColor.Black;
		Console.ForegroundColor = ConsoleColor.Blue;
		Console.WriteLine("\n	Paint For Console can only open .png files that were created with Paint For Console");
		Console.WriteLine("\n	Select option with mouse.");

		Console.WriteLine("\n	When on Canvas press 'S' to save image. Press 'R' twice to clear. Press 'Z' to undo.");
		Console.WriteLine("\n	Change brush radius with the scroll wheel, hold 'Shift' and scroll to change color.");
		Console.WriteLine("\n	Press 'X' twice to go back to menu ( ! Will Delete Your File ! ). ");

		bool selecting = true;
		int selected = 0; //0 is neither open nor new, 1 is new, 2 is open, 3 is exit
		
		IntPtr inputHandle = PInvoke.GetStdHandle(-10);
		InputRecord inputRecord = new InputRecord();
		uint num = 0;
		Console.CursorVisible = false;

		while (selecting)
		{
			PInvoke.ReadConsoleInput(inputHandle, ref inputRecord, 1, ref num);
			
			if (inputRecord.eventType == EventType.MOUSE_EVENT)
			{
				if (inputRecord.eventRecord.mouseEvent.dwEventFlags == MouseEventFlags.MOUSE_MOVED)
				{
					Coord position = inputRecord.eventRecord.mouseEvent.dwMousePosition;
					if (position.X >= newStartCoord.X + 8 && position.X <= newEndCoord.X - 1 && position.Y >= newStartCoord.Y && position.Y <= newEndCoord.Y) // selected new file
					{
						if (selected != 1)
						{
							selected = 1;

							Console.SetCursorPosition(0, newStartCoord.Y);
							Console.BackgroundColor = ConsoleColor.Gray;
							Console.ForegroundColor = ConsoleColor.Black;
							Console.Write(newFile);
							Console.WriteLine('\n');

							Console.BackgroundColor = ConsoleColor.DarkGray;
							Console.ForegroundColor = ConsoleColor.White;
							Console.Write(openFile);
							Console.WriteLine('\n');

							Console.BackgroundColor = ConsoleColor.DarkGray;
							Console.ForegroundColor = ConsoleColor.White;
							Console.Write(exit);
							Console.WriteLine('\n');
						}
						
					}
					else if (position.X >= openStartCoord.X + 8 && position.X <= openEndCoord.X - 1 && position.Y >= openStartCoord.Y && position.Y <= openEndCoord.Y) // selected open file
					{
						if(selected != 2)
						{
							selected = 2;

							Console.SetCursorPosition(0, newStartCoord.Y);
							Console.BackgroundColor = ConsoleColor.DarkGray;
							Console.ForegroundColor = ConsoleColor.White;
							Console.Write(newFile);
							Console.WriteLine('\n');

							Console.BackgroundColor = ConsoleColor.Gray;
							Console.ForegroundColor = ConsoleColor.Black;
							Console.Write(openFile);
							Console.WriteLine('\n');

							Console.BackgroundColor = ConsoleColor.DarkGray;
							Console.ForegroundColor = ConsoleColor.White;
							Console.Write(exit);
							Console.WriteLine('\n');
						}
						
					}
					else if (position.X >= exitStartCoord.X + 8 && position.X <= exitEndCoord.X - 1 && position.Y >= exitStartCoord.Y && position.Y <= exitEndCoord.Y) // selected exit
					{
						if (selected != 3)
						{
							selected = 3;

							Console.SetCursorPosition(0, newStartCoord.Y);
							Console.BackgroundColor = ConsoleColor.DarkGray;
							Console.ForegroundColor = ConsoleColor.White;
							Console.Write(newFile);
							Console.WriteLine('\n');

							Console.BackgroundColor = ConsoleColor.DarkGray;
							Console.ForegroundColor = ConsoleColor.White;
							Console.Write(openFile);
							Console.WriteLine('\n');

							Console.BackgroundColor = ConsoleColor.Gray;
							Console.ForegroundColor = ConsoleColor.Black;
							Console.Write(exit);
							Console.WriteLine('\n');
						}

					}
					else if (selected != 0) // selected none
					{
						selected = 0;

						Console.SetCursorPosition(0, newStartCoord.Y);
						Console.BackgroundColor = ConsoleColor.DarkGray;
						Console.ForegroundColor = ConsoleColor.White;
						Console.Write(newFile);
						Console.WriteLine('\n');

						Console.BackgroundColor = ConsoleColor.DarkGray;
						Console.ForegroundColor = ConsoleColor.White;
						Console.Write(openFile);
						Console.WriteLine('\n');

						Console.BackgroundColor = ConsoleColor.DarkGray;
						Console.ForegroundColor = ConsoleColor.White;
						Console.Write(exit);
						Console.WriteLine('\n');
					}
				}
				else if (inputRecord.eventRecord.mouseEvent.dwEventFlags == MouseEventFlags.MOUSE_BUTTON)
				{
					switch (selected)
					{
						case 1:
							selecting = false;
							break;

						

						case 3:
							selecting = false;
							running = false;
							drawing = false;
							
							break;

						case 2:
							selecting = false;
							Console.BackgroundColor = ConsoleColor.Black;
							Console.ForegroundColor = ConsoleColor.White;
							Console.Clear();
							ConsoleControl.SetCurrentFont("Consolas", 20);
							Console.WriteLine("Images: ");
							ConsoleControl.SetFullscreen();
							while (true)
							{

							}
							break;
					}
				}
			}
		}
		
	}
}
	
