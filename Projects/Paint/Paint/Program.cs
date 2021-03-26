using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
		
		string tab = "\t\n\t\n\t\n\t\n\t\n\t\n\t\n\t\n\t\n\t\n\t\n\t\n\t\n\t\n\t\n\t\n\t";


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
		Console.WriteLine("\n	Paint For Console works best when opening png files created with Paint For Console.\n");

		Console.WriteLine("\n	When on Canvas:\n\n	Press 'S' to save image. Press 'R' twice to clear. Press 'Z' to undo.");
		Console.WriteLine("\n	Change brush radius with the scroll wheel, hold 'Shift' and scroll to change color.");
		Console.WriteLine("\n	Press 'X' twice to go back to menu (canvas is not saved). ");
		Console.SetCursorPosition(0, newStartCoord.Y);
		Console.BackgroundColor = ConsoleColor.Black;
		Console.Write(tab);
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

							Console.SetCursorPosition(0, newStartCoord.Y);
							Console.BackgroundColor = ConsoleColor.Black;
							Console.Write(tab);
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

							Console.SetCursorPosition(0, newStartCoord.Y);
							Console.BackgroundColor = ConsoleColor.Black;
							Console.Write(tab);
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

							Console.SetCursorPosition(0, newStartCoord.Y);
							Console.BackgroundColor = ConsoleColor.Black;
							Console.Write(tab);
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

						Console.SetCursorPosition(0, newStartCoord.Y);
						Console.BackgroundColor = ConsoleColor.Black;
						Console.Write(tab);
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
							runStartScreen = false;

							Console.BackgroundColor = ConsoleColor.Black;
							Console.Clear();
							ConsoleControl.SetCurrentFont("Consolas", 30);

							ConsoleControl.SetWindowed(); //its bugged and sometimes does not realise the console changed size (due to font change) unless I window it temporarily

							ConsoleControl.SetFullscreen();
							Bitmap bmp;
							Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Images");
							string[] filesInDirectory = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Images\");
							if (filesInDirectory.Length == 0)
							{
								Console.CursorVisible = false;
								Console.WriteLine("\nThere are no files in the Image folder. Press 'X' to return to the menu or press 'N' to make a new blank image.");
								while (true)
								{
									ConsoleKey key = Console.ReadKey(true).Key;
									if (key == ConsoleKey.N)
									{
										runStartScreen = true;
										drawing = true;
										break;
									}
									else if (key == ConsoleKey.X)
									{
										runStartScreen = true;
										drawing = false;
										break;
									}
								}
							}
							if (runStartScreen)
								break;
							List<string> validFiles = new List<string>(filesInDirectory.Length);


							for (int i = 0; i < filesInDirectory.Length; i++)
							{

								if (filesInDirectory[i].Contains(".png"))
								{
									string[] parts = filesInDirectory[i].Split(char.Parse(@"\"));
									validFiles.Add(parts[parts.Length - 1]);


								}
							}

							List<Rect> buttonBounds = new List<Rect>(validFiles.Count);
							int top, left, bottom, right;

							Coord size = new Coord(5, 5);
							

							Console.SetCursorPosition(0, Console.WindowHeight - 1);
							Console.ForegroundColor = ConsoleColor.Blue;
							Console.WriteLine("Press 'X' to return to menu.");
							Console.SetCursorPosition(0, 0);
							Console.ForegroundColor = ConsoleColor.White;
							Console.WriteLine("\nSelect a file from to open: \n");
							

							for (int i = 0; i < validFiles.Count; i++)
							{
								Console.BackgroundColor = ConsoleColor.DarkGray;
								Console.ForegroundColor = ConsoleColor.White;

								left = Console.CursorLeft;
								top = Console.CursorTop;
								Console.Write($" {validFiles[i]} ");
								right = Console.CursorLeft;
								bottom = Console.CursorTop;
								buttonBounds.Add(new Rect(left, top, right, bottom));

								Console.BackgroundColor = ConsoleColor.Black;
								Console.Write("    ");

								if ((i + 1) % size.X == 0)
								{
									Console.Write("\n\n");
								}

								if (i == size.X * size.Y)
								{
									validFiles.RemoveRange(i + 1, validFiles.Count - i);
									break;

								}
							}
							bool choosingImage = true;
							selected = 0;
							int amountToSelect = validFiles.Count;

							Console.BackgroundColor = ConsoleColor.DarkGray;
							if (Console.KeyAvailable)
								Console.ReadKey(true);

							
							Console.CursorVisible = false;
							while (choosingImage)
							{
								PInvoke.ReadConsoleInput(inputHandle, ref inputRecord, 1, ref num);

								
								if (inputRecord.eventType == EventType.KEY_EVENT)
								{
									if (inputRecord.eventRecord.keyEvent.wVirtualKeyCode == VirtualKeys.X)
									{
										choosingImage = false;
										runStartScreen = true;
										drawing = false;
										continue;
									}
								}
								if (inputRecord.eventType == EventType.MOUSE_EVENT)
								{
									if (inputRecord.eventRecord.mouseEvent.dwEventFlags == MouseEventFlags.MOUSE_MOVED)
									{
										Coord position;
										
										for (int i = 0; i < validFiles.Count; i++)
										{
											position = inputRecord.eventRecord.mouseEvent.dwMousePosition;
											if (position.X >= buttonBounds[i].Left && position.X <= buttonBounds[i].Right && position.Y >= buttonBounds[i].Top && position.Y <= buttonBounds[i].Bottom)
											{
												if (selected != i + 1)
												{
													selected = i + 1;
													Console.SetCursorPosition(buttonBounds[0].Left, buttonBounds[0].Top);
													for (int x = 0; x < validFiles.Count; x++)
													{
														if (x == i)
														{
															Console.BackgroundColor = ConsoleColor.Gray;
															Console.ForegroundColor = ConsoleColor.Black;
														}
														else
														{
															Console.BackgroundColor = ConsoleColor.DarkGray;
															Console.ForegroundColor = ConsoleColor.White;
														}
														Console.Write($" {validFiles[x]} ");
														Console.BackgroundColor = ConsoleColor.Black;
														Console.Write("    ");

														if ((x + 1) % size.X == 0)
														{
															Console.Write("\n\n");
														}
													}

												}
												
												break;
											}
											else if (i == validFiles.Count - 1 && selected != 0)
											{
												selected = 0;
												Console.SetCursorPosition(buttonBounds[0].Left, buttonBounds[0].Top);
												for (int x = 0; x < validFiles.Count; x++)
												{
													Console.BackgroundColor = ConsoleColor.DarkGray;
													Console.ForegroundColor = ConsoleColor.White;
													
													Console.Write($" {validFiles[x]} ");
													Console.BackgroundColor = ConsoleColor.Black;
													Console.Write("    ");

													if ((x + 1) % size.X == 0)
													{
														Console.Write("\n\n");
													}
												}
											}
										}

									}
									else if (inputRecord.eventRecord.mouseEvent.dwEventFlags == MouseEventFlags.MOUSE_BUTTON)
									{
										Console.ForegroundColor = ConsoleColor.Black;
										if (selected != 0)
											choosingImage = false;

									}
								}
								
							}
							if (!runStartScreen) //(will be true when the user cancels the file making
							try
							{

								bmp = new Bitmap(Directory.GetCurrentDirectory() + @"\Images\" + validFiles[selected - 1]);
								

								//sets console font
								ConsoleControl.SetCurrentFont("NSimSun", 5);
								//makes the console fullscreen
								ConsoleControl.SetWindowed(); //its bugged and sometimes does not realise the console changed size (due to font change) unless I window it temporarily
								ConsoleControl.SetFullscreen();
								//removes the scroll bar
								Console.BufferHeight = Console.WindowHeight;


								int w = Console.LargestWindowWidth / 2, h = Console.LargestWindowHeight;
								ConsoleRender.rasterisedScreen = new ConsoleColor[w, h];
								for (int x = 0; x < bmp.Width; x++)
								{
									for (int y = 0; y < bmp.Height; y++)
									{
										ConsoleRender.rasterisedScreen[x, y] = GetClosestConsoleColor(bmp.GetPixel(x, y));
									}
								}
							}
							catch
							{
								Console.WriteLine("An error occurred. Press anything to continue.");
								Console.ReadKey(true);
							}
							break;
					}
				}
			}
		}
		
	}

	static ConsoleColor GetClosestConsoleColor(Color color)
	{

		Color[] cCs =
		{
			Color.Black,						//Black
			Color.FromArgb(255,0,0,255),		//Dark Blue
			Color.FromArgb(255,0,128,0),		//Dark Green
			Color.FromArgb(255,0,128,128),		//Dark Cyan
			Color.FromArgb(255,128,0,0),		//Dark Red
			Color.FromArgb(255,128,0,128),		//Dark Magenta
			Color.FromArgb(255,128,128,0),		//Dark Yellow
			Color.FromArgb(255,192,192,192),	//Grey
			Color.FromArgb(255,128,128,128),	//Dark Grey
			Color.LightBlue,					//Blue
			Color.Green,						//Green
			Color.Cyan,							//Cyan
			Color.Red,							//Red
			Color.Magenta,						//Magenta
			Color.Yellow,						//Yellow
			Color.White							//White
			};

		float[] distance = new float[cCs.Length];
		for (int x = 0; x < cCs.Length; x++)
		{
			Vector3 vector = new Vector3(cCs[x].R - color.R, cCs[x].G - color.G, cCs[x].B - color.B);
			distance[x] = vector.Length();
		}
		float big = 999999;
		int number = 0;

		int i = 0;

		for (; i < distance.Length; i++)
		{
			if (distance[i] < big)
			{
				big = distance[i];
				number = i;
			}
		}

		return (ConsoleColor)number;
	}
}
	
