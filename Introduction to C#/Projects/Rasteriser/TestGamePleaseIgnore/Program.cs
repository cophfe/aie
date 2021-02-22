using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
	static void Main(string[] args)
	{
		
		Console.CursorVisible = false;
		int _scaleAmt = 1;
		bool working = true, happy = true, autoScale = true;
		while (working)
		{
			Console.SetCursorPosition(0, 0);
			Console.WindowHeight = 30;
			Console.WindowWidth = 120;
			ConsoleHelper.SetCurrentFont("Consolas", 16);
			Console.SetCursorPosition(0, 0);
			
			//Console.Write(new string(' ', 40));
			Console.SetCursorPosition(0, 0);
			happy = true;
			autoScale = true;
			bool scaling = true;
			//source bitmap image
			Bitmap _bmp;
			string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Images\");
			for (int i = 0; i < files.Length; i++)
			{
				string[] parts = files[i].Split(char.Parse(@"\"));
				files[i] = parts[parts.Length - 1];
			}
			try
			{
				
				_bmp = new Bitmap(Directory.GetCurrentDirectory() + @"\Images\" + Selector.Select(files));
				Console.Clear();
			}
			catch
			{
				_bmp = new Bitmap(@"C:\Users\s210575\Documents\CoPHFe Files\Introduction to C#\TestGamePleaseIgnore\Images\obama.jpg");
			}
		while (happy)
			{
				scaling = true;
				Console.SetCursorPosition(0, 0);
				ConsoleHelper.SetCurrentFont("NSimSun", 5);
				
				Console.SetCursorPosition(0, 0);
				//Console.Write(new string(' ', 40));
				int x, y;
				while (scaling)
				{
					if (_bmp.Height / (_scaleAmt + 1) < Console.LargestWindowHeight)
					{
						Console.WindowHeight = _bmp.Height / (_scaleAmt + 1);
						scaling = false;
					}
					else if (autoScale)
					{
						_scaleAmt++;
					}
					else
					{
						Console.WindowHeight = Console.LargestWindowHeight;
						scaling = false;
					}
					if (_bmp.Width * 2 / (_scaleAmt + 1) +10< Console.LargestWindowWidth-10)
					{
						Console.WindowWidth = _bmp.Width * 2 / (_scaleAmt + 1) + 10;
						scaling = false;
					}
					else if (autoScale)
					{
						_scaleAmt++;
					}
					else
					{
						Console.WindowWidth = Console.LargestWindowWidth;
						scaling = false;
					}
				}
				Console.SetCursorPosition(0, 0);

				int w = _bmp.Width;// -_scaleAmt;
				int h = _bmp.Height;// -_scaleAmt;
				ConsoleColor[,] _img = new ConsoleColor[w,h]; ;
				
				for (y = 0; y < h; y++)
				{
					for (x = 0; x < w; x++)
					{
						
						Color _pC = _bmp.GetPixel(x, y);
						_img[x , y] = GetClosestConsoleColour(_pC);
						_img[x, y] = GetClosestConsoleColour(_pC);
						//Console.BackgroundColor = GetClosestConsoleColour(_pC);
						//Console.Write("  ");
						//x += _scaleAmt;
						
					}
					Console.Write('\n');
					//y += _scaleAmt;
					
				}
				ConsoleWriter.Render(_img, w, h, _scaleAmt);
				Console.BackgroundColor = 0;
				ConsoleKey key = Console.ReadKey(true).Key;
				switch (key)
				{
					case ConsoleKey.R:
						happy = false;
						Console.Clear();
						break;
					case ConsoleKey.RightArrow:
						if (_scaleAmt != 0)
							_scaleAmt--;
						autoScale = false;
						Console.Clear();
						break;
					case ConsoleKey.LeftArrow:
						
							_scaleAmt++;
						autoScale = false;
						Console.Clear();
						break;
					default:
						working = false;
						happy = false;
						break;
				}
			}
		}
		
		
		
	}

	static ConsoleColor GetClosestConsoleColour(Color color)
	{

		Color[] cCs =
		{
			Color.Black,
			Color.DarkBlue,
			Color.DarkGreen,
			Color.DarkCyan,
			Color.DarkRed,
			Color.DarkMagenta,
			Color.Brown,
			Color.DarkGray,
			Color.Gray,
			Color.Blue,
			Color.Green,
			Color.Cyan,
			Color.Red,
			Color.Magenta,
			Color.Yellow,
			Color.White
		};
		float[] distance = new float[cCs.Length];
		for (int i = 0; i < cCs.Length; i++)
		{
			Vector3 vector = new Vector3(cCs[i].R- color.R, cCs[i].G -color.G, cCs[i].B- color.B );
			distance[i] = vector.Magnitude;
		}
		float big = 999999;
		int number = 0;
		for (int i = 0; i < distance.Length; i++)
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
struct Vector3
{
	public float _x, _y, _z;
	public Vector3(float x, float y, float z)
	{
		_x = x;
		_y = y;
		_z = z;
	}
	public float Magnitude
	{
		get
		{
			return (float)Math.Sqrt(_x * _x + _y * _y + _z * _z);
		}
	}
	
}
