using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ReadingWriting
{
	class Canvas
	{
		int w, h;
		ConsoleColor[,] pixelArray;
		public Canvas(int width, int height)
		{
			w = width;
			h = height;
			pixelArray = new ConsoleColor[w, h];
		}

		public static void RenderCanvas()
		{
			
		}

		public static void RenderUI()
		{
			const ConsoleColor background = ConsoleColor.DarkGray;
			
			int w = Console.WindowWidth/2, h= Console.WindowHeight;
			Vector2 border = new Vector2(w /20, h/20);
			ConsoleColor[,] UI = new ConsoleColor[w,h];
			
			for (int y = 0; y < h; y++)
			{
				for (int x = 0; x < w; x++)
				{
					if ((y < border.Y || y > h - border.Y) || (x < border.X || x > w - border.X))
					{
						UI[x, y] = background;
						continue;
					}
					UI[x, y] = ConsoleColor.Black;
				}
			}
			for (int y = 0; y < h; y++)
			{
				for (int x = 0; x < w; x++)
				{
					Console.BackgroundColor = UI[x, y];
					Console.Write("  ");

				}
			}
		}
	}
}
