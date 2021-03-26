using System;
using NCalc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Console Properties:
// Window size: 400x200
// Font size: 5
// Font: NSimSun (width is half of height so the window and graph will be square and have accurate angles)

class Program
{
	
	static int _size = 10;
	static float _yScale = 1,_xScale = 1;
	static void Main(string[] args)
	{
		ConsoleHelper.SetCurrentFont("Consolas", 16);
		Console.Clear();
		//this program draws a function on the console
		//scale with plus and minus or individually scale x and y axis with arrow keys
		//press R to restart

		//first while loop loops through asking for expression and rendering graph
		bool graphing = true;
		while (graphing)
		{
			
			ConsoleHelper.SetCurrentFont("Consolas", 16);
			Console.SetCursorPosition(0, 0);
			Console.Write(new string(' ', 50));
			Console.SetCursorPosition(0, 0);
			bool noErrors = true;
			Console.WriteLine("Graph a function!");
			Console.SetCursorPosition(0, 1);
			Console.Write("f(x) = ");
			Console.SetWindowSize(120, 30);
			string _input = Console.ReadLine();
			Expression _e = null;
			try
			{
				_e = new Expression(_input);
				_e.Parameters["x"] = 1;
				Console.SetCursorPosition(0, 1);
				Console.Write(new string(' ', 50));
				Console.SetCursorPosition(0, 0);
				_e.Evaluate();
			}
			catch
			{
				noErrors = false;
				Console.Write(new string(' ', 50));
				Console.SetCursorPosition(0, 2);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("Expression was not accepted");
				Console.ResetColor();
				Console.SetCursorPosition(0, 0);
			}
			Console.CursorVisible = false;
			while (noErrors)
			{
				ConsoleHelper.SetCurrentFont("NSimSun", 5);
				Console.SetCursorPosition(0, 0);
				Console.Write(new string(' ', 40));
				Console.SetCursorPosition(0, 2);
				Console.Write(new string(' ', 50));
				Console.WindowHeight = Console.LargestWindowHeight-20;
				Console.WindowWidth = Console.WindowHeight * 2;
				_size = Console.WindowHeight;
				Console.BackgroundColor = ConsoleColor.DarkGray;
				Console.SetCursorPosition(0, _size / 2);
				Console.Write(new string(' ', _size * 2));
				for (int y = 0; y < _size; y++)
				{
					Console.SetCursorPosition(_size, y);
					Console.Write("  ");
				}

				bool[,] _graph = new bool[_size, _size];

				Console.BackgroundColor = ConsoleColor.Black;


				for (int y = 0; y < _size; y++)
				{
					for (int x = 0; x < _size; x++)
					{
						_graph[x, y] = false;
					}
				}
				for (int x = -_size / 2; x < _size / 2; x++)
				{
					_e.Parameters["x"] = x* _xScale;
					int y = (int)(_yScale * float.Parse(_e.Evaluate().ToString()));
					if (y + _size / 2 < _size && y + _size / 2 > 0)
					{
						_graph[x + _size / 2, (int)(y + _size / 2)] = true;
					}
				}

				for (int y = 0; y < _size; y++)
				{
					for (int x = 0; x < _size; x++)
					{

						if (_graph[x, y])
						{
							Console.BackgroundColor = ConsoleColor.White;
							Console.SetCursorPosition(x * 2, _size - y);
							Console.Write("  ");
						}
					}
				}
				ConsoleKey key = Console.ReadKey(true).Key;
				if (key == ConsoleKey.R)
				{
					Console.ResetColor();
					Console.Clear();
					_xScale = 1;
					_yScale = 1;
					break;
				}
				else if (key == ConsoleKey.UpArrow)
				{
					Console.ResetColor();
					Console.Clear();
					_yScale += 0.20f;
				}
				else if (key == ConsoleKey.DownArrow)
				{
					Console.ResetColor();
					Console.Clear();
					_yScale -= 0.2f;
				}
				else if (key == ConsoleKey.LeftArrow)
				{
					Console.ResetColor();
					Console.Clear();
					_xScale += 0.1f;
				}
				else if (key == ConsoleKey.RightArrow)
				{
					Console.ResetColor();
					Console.Clear();
					_xScale -= 0.1f;
				}
				else if (key == ConsoleKey.OemPlus)
				{
					Console.ResetColor();
					Console.Clear();
					_xScale += 0.1f;
					_yScale += 0.1f;
				}
				else if (key == ConsoleKey.OemMinus)
				{
					Console.ResetColor();
					Console.Clear();
					_xScale -= 0.1f;
					_yScale -= 0.1f;
				}
				else
				{
					graphing = false;
					break;
				}
			}
			Console.CursorVisible = true;
		}
		
	}

}

