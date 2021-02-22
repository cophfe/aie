using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
	static Random rand = new Random();
	static Timer tim;
	static string[] letters;
	static int[] fC;
	static int[] bC;
	static int firstLine = 0, finalLine = Console.WindowHeight;
	static int scrollPosition = 0;
	static void Main(string[] args)
	{
		Console.CursorVisible = false;
		Console.WriteLine("Break the cycle by rolling a 20.");
		int[] numbers = new int[20];
		int amount = 1;
		Console.Write("You rolled ");
		while (true)
		{
			int lol = Die(20);
			if (lol == 20)
			{
				Console.WriteLine("and a 20");
				
				Console.WriteLine(new string('_', Console.WindowWidth));
				firstLine = Console.CursorTop +1;
				Console.ForegroundColor = ConsoleColor.Green;
				Center("Number of rolls: " + amount);
				Console.ResetColor();
				for (int i = 1; i< 21; i++)
				{
					if(i % 2 == 0)
					{
						Console.ForegroundColor = ConsoleColor.DarkGray; 
					}
					else
					{
						Console.ResetColor();
					}
					
					Center("Number of " +i +"s: " + numbers[i-1]);
				}
				Console.ResetColor();
				finalLine = Console.CursorTop;

				Console.Write(new string('_', Console.WindowWidth));
				
				break;
			}
			//if()
			if (lol == 8 || lol == 11 || lol == 18)
				Console.Write("an " + lol + ", ");
			else
				Console.Write("a " +lol +", ");
			++numbers[lol - 1];
			amount++;
		}
		Console.ForegroundColor = ConsoleColor.Blue;
		Console.WriteLine("\nPress anything to exit");
		DecorateSides(5, 10, firstLine, finalLine);
		Console.ReadKey();
	}

	static void Center(string line, bool newLine = true)
	{
		Console.Write(new string(' ', Console.WindowWidth / 2 - (line.Length)/2) + line);
		if (newLine)
			Console.Write("\n");
	}
	static void DecorateSides(int width, int scrollSpeed, int startLine, int endLine)
	{

		int charNum = width;
		int lineNum = endLine-startLine + 1;
		fC = new int[lineNum * width];
		bC = new int[lineNum * width];
		letters = new string[lineNum * width];

		for (int i = 0; i < letters.Length; i++)
		{
			letters[i] = letters[i] + "$% @*+-{}[];~&#!".ElementAt<char>(rand.Next(0, 15));
			fC[i] = rand.Next(1, 15);
			bC[i] = rand.Next(1, 15);

		}
		Console.SetCursorPosition(0, firstLine);
		for (int i = 0; i < letters.Length; i++)
		{
			
			Console.ForegroundColor = (ConsoleColor)fC[i];
			Console.BackgroundColor = (ConsoleColor)bC[i];
			Console.Write(letters[i]);
			Console.ResetColor();
			if ((i+1) % 5 == 0)
			{
				Console.SetCursorPosition(0, firstLine + i/5);

			}
			
		}
		tim = new Timer(1000/scrollSpeed);
		tim.Elapsed += (sender, e) => { ScrollSides(lineNum); };
			tim.AutoReset = true;
		tim.Enabled = true;
		Console.ResetColor();
		Console.SetCursorPosition(0, finalLine -1);

	}


	static void ScrollSides(int lineAmount, bool left = true)
	{

		for (int i = 0; i < letters.Length; i++)
		{

			
			Console.ResetColor();
			if ((i + 1) % 5 == 0)
			{
				int line = firstLine + i / 5 + scrollPosition;
				if (line > finalLine -1)
					line -= lineAmount;
				Console.SetCursorPosition(0, line);

			}
			Console.ForegroundColor = (ConsoleColor)fC[i];
			Console.BackgroundColor = (ConsoleColor)bC[i];
			Console.Write(letters[i]);

		}
		scrollPosition++;
		if (scrollPosition == lineAmount + 1)
		{
			scrollPosition = 0;
		}
		
	}
	static int Die(int sides)
	{
		int num = (int)(sides * rand.NextDouble());

		return (num + 1);
	}
}

