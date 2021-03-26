using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class Program
{
	static Random rand = new Random();
	static int groundHeight = 5, borderThickness = 2;
	static int PhysicsIterationAmount = 60; //iterations per second
	static int worldSizeX = Console.WindowWidth, worldSizeY = Console.WindowHeight;
	static int characterPosX, characterPosY, characterSize=2, move = 0;
	static bool[,] world;
	static bool jumping = false, startJump = false, worldReset = false;
	static int jumpPower, jumpPowerMax = 6,airTime, airTimeMax = 2;
	static long deltaTime,physicsTimer = 0;
	static void Main(string[] args)
	{
		world = new bool[worldSizeX, worldSizeY];
		// could define values by doing {{val,val,val},{val,val,val},{val,val,val},{val,val,val}}
		Console.CursorVisible = false;

		WorldGen();

		//read inputs
		bool playing = true;
		Console.ResetColor();
		long timeInitial = 0;
		long timeFinal =0;
		
		long hourGlass=0;
		var thread = new Thread(() => {
			while (playing)
			{
				var input = Console.ReadKey(true);

				switch (input.Key)
				{
					case ConsoleKey.LeftArrow:
						move = 1;

						break;
					case ConsoleKey.RightArrow:
						move = 2;

						break;
					case ConsoleKey.Spacebar:
						bool floor = false;
						for (int i = 0; i < characterSize; i++)
						{
							if (world[characterPosX + i, characterPosY +1])
							{
								floor = true;
									
							}
						}
						if (floor)
						{

							jumping = true;
							jumpPower = jumpPowerMax;
						}
						break;
					case ConsoleKey.R:
						worldReset = true; //bool that main thread checks for cuz it messes up if main thread is running at the same time;
						
						break;
				}
			}
		});
		thread.IsBackground = true;
		thread.Start();

		while (playing)
		{
			timeFinal = DateTime.Now.Ticks;
			deltaTime = timeFinal - timeInitial;
			timeInitial = timeFinal;
			hourGlass += deltaTime;
			Console.SetCursorPosition(0, 0);
			if ( hourGlass> 10000000/PhysicsIterationAmount)
			{

				Console.Write(timeFinal / 10000000);
				if (move == 1)
				{
					MovePlayer(false);
					move = 0;
				}
				else if (move == 2)
				{
					MovePlayer(true);
					move = 0;
				}
				Physics();
				if (worldReset)
				{
					WorldGen();
				}
				hourGlass = 0;

			}
			
			
			
		}
	}

	private static void WorldGen()
	{
		Console.Clear();
		//make world array
		Console.WriteLine("Crafting world...");
		//create basic world with ground, also a border around the sides
		for (int y = 0; y < worldSizeY; ++y)
		{
			for (int x = 0; x < worldSizeX; ++x)
			{
				if (y < borderThickness)
				{
					world[x, y] = true;
				}
				else if (x < borderThickness || x > worldSizeX - borderThickness)
				{
					world[x, y] = true;
				}
				else if (y > worldSizeY - groundHeight)
				{

					world[x, y] = true;
				}
				else
				{

					world[x, y] = false;
				}
			}
		}
		//create random platforms
		int PlatformAmount = 20, platformSize = 7;
		for (int platform = 0; platform < PlatformAmount; platform++)
		{
			int xPos = rand.Next(borderThickness + 1, worldSizeX - (borderThickness + platformSize));
			int yPos = rand.Next(0, worldSizeY - groundHeight);
			for (int i = 0; i < platformSize; i++)
			{
				world[xPos + i, yPos] = true;
			}
		}
		Console.Clear();
		//render world array. if coord is true, is white, if not, is black.
		for (int y = 0; y < worldSizeY; ++y)
		{
			for (int x = 0; x < worldSizeX; ++x)
			{
				if (world[x, y])
				{
					Console.BackgroundColor = ConsoleColor.White;
					Console.Write(' ');
				}
				else
				{
					Console.BackgroundColor = ConsoleColor.Black;
					Console.Write(' ');
				}
			}

		}
		Console.SetCursorPosition(0, 0);

		//Create character;

		characterPosX = worldSizeX / 2;
		characterPosY = 6;

		RenderPlayer();
		worldReset = false;
	}
	private static void Physics()
	{
		//v = u + a*t
		//v = u + gravity * 1000 / PhysicsIterationAmount;

		//if (!world[characterPosX, characterPosY + 1] && physicsTimer > 500000)
		//{
		//	physicsTimer = 0;

		//}
		physicsTimer += deltaTime / 1000;
		if (jumping)
		{
			//check if any blocks above the characters head are white
			for (int i = 0; i < characterSize; i++)
			{
				if (world[characterPosX + i, characterPosY - characterSize])
				{
					jumping = false;
					physicsTimer = 0;
				}
			}
				
			if (physicsTimer > 10)
			{



				jumpPower--;
				physicsTimer = 0;
				if (jumpPower > 0)
				{
					RenderPlayer(true);
					characterPosY--; 
					RenderPlayer();
				}

			}
			if (jumpPower < -airTimeMax)
			{

				jumping = false;
				physicsTimer = 0;
			}

		}
		if (physicsTimer > 10)
		{
			for (int i = 0; i < characterSize; i++)
			{
				if (!world[characterPosX + i, characterPosY + 1])
				{
					if (airTime > 0)
					{
						airTime--;
						break;
					}
					else
					{
						RenderPlayer(true);
						characterPosY++;
						RenderPlayer();

						physicsTimer = 0;

					}
				}
				else
				{
					airTime = airTimeMax;
					physicsTimer = 0;
				}
			}
		}
		



		//Console.Write("GHADJADH");
	}
	
	static void MovePlayer(bool right = true)
	{
		
		//Console.SetCursorPosition(characterPosX, characterPosY);
		//Console.BackgroundColor = ConsoleColor.Black;
		//Console.Write(" ");
		if (right)
		{
			for (int i = 0; i < characterSize; i++)
			{
				if (world[characterPosX + characterSize, characterPosY - i])
				{
					return;
				}
			}
			RenderPlayer(true);
			characterPosX++;


		}
		else
		{
			for (int i = 0; i < characterSize; i++)
			{
				if (world[characterPosX - 1, characterPosY - i])
				{
					return;
				}
			}
			RenderPlayer(true);
			characterPosX--;
		}
		RenderPlayer();
	}
	static void RenderPlayer(bool remove = false)
	{
		for (int y = 0; y < characterSize; ++y)
		{
			for(int x = 0; x< characterSize; ++x)
			{
				Console.SetCursorPosition(characterPosX + x, characterPosY - y);
				if (remove)
				{
					Console.BackgroundColor = ConsoleColor.Black;
				}
				else
				{
					Console.BackgroundColor = ConsoleColor.DarkBlue;
				}
				Console.Write(' ');

			}
		}

		Console.ResetColor();
	}
}


