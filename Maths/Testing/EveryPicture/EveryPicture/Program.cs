using System;
using System.Drawing.Imaging;
using System.Drawing;

namespace EveryPicture
{
	class Program
	{
		const int h = 2, w = 2;
		static Bitmap bmp;
		const int maxColour = (int)ConsoleColor.White;
		static void Main(string[] args)
		{
			ConsoleHelper.SetCurrentFont("Consolas", 16);
			//Console.WindowHeight = 20;
			//Console.WindowWidth = 40;
			//Console.BufferWidth = 40;
			//Console.BufferHeight = 20;
			Console.BackgroundColor = ConsoleColor.Black;
			Console.Clear();
			Console.CursorVisible = false;
			int[] pixel = new int[w * h];
			for (int k = 0; k < pixel.Length; k++)
			{
				pixel[k] = 0;
			}

			bmp = new Bitmap(w, h);
			int i = 0;
			
			while (pixel[pixel.Length - 1] != maxColour)
			{
				DrawPixel((ConsoleColor)pixel[i], i);
				SaveFile(pixel);
				
				if (pixel[i] == maxColour )
				{
					i++;
					while (pixel[i - 1] >= maxColour && i < pixel.Length)
					{
						pixel[i - 1] = 0;
						pixel[i] ++;
						SaveFile(pixel);
						DrawPixel((ConsoleColor)pixel[i], i);
						i++;
						
					}
					i = 0;
				}
				pixel[i] += 1;
				
				//System.Threading.Thread.Sleep(1);
				
			}

			Console.BackgroundColor = ConsoleColor.Black;
			
			Console.ReadKey();
		}

		static void DrawPixel(ConsoleColor colour, int index)
		{
			int hIndex = 0;
			while ((index + 1) - h * hIndex > w)
			{
				hIndex++;
			}
			Console.SetCursorPosition((index - h * hIndex) * 2, hIndex);
			Console.BackgroundColor = colour;
			Console.Write("  ");
		}

		static int index = 0;

		static void SaveFile(int[] array)
		{
			for (int y = 0; y < bmp.Height; y++)
			{
				for (int x = 0; x < bmp.Width; x++)
				{
					bmp.SetPixel(x, y, GetColor(array[x + y * h]));
				}
			} //DONT USE THIS ON SSD
			//IT WILL EAT THROUGH ITS USAGES
			//bmp.Save($"Images/2x2_{index}.jpg");
			index++;
			
		}

		public static Color GetColor(int consoleColor)
		{
			return colors[consoleColor];
		}

		static Color[] colors =
			{
			Color.Black,                        //Black
			Color.FromArgb(255,0,0,255),	    //Dark Blue
			Color.FromArgb(255,0,128,0),		//Dark Green
			Color.FromArgb(255,0,128,128),		//Dark Cyan
			Color.FromArgb(255,128,0,0),		//Dark Red
			Color.FromArgb(255,128,0,128),		//Dark Magenta
			Color.FromArgb(255,128,128,0),		//Dark Yellow
			Color.FromArgb(255,192,192,192),	//Grey
			Color.FromArgb(255,128,128,128),	//Dark Grey
			Color.LightBlue,				    //Blue
			Color.Green,					    //Green
			Color.Cyan,						    //Cyan
			Color.Red,						    //Red
			Color.Magenta,					    //Magenta
			Color.Yellow,					    //Yellow
			Color.White						    //White
			};
	}
}
