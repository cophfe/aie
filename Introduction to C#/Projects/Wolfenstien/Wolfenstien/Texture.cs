using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Numerics;

//Net core: you have to add system.drawing.common through nuget

namespace Wolfenstien
{
    class Texture
    {
		public ConsoleColor[,] pixelArray;
		
		public Texture(Bitmap bmp, bool transparancy = false)
        {
			pixelArray = new ConsoleColor[bmp.Width, bmp.Height];
			for (int x = 0; x < bmp.Width; x++)
			{
				for (int y = 0; y < bmp.Height; y++)
				{
					pixelArray[x, y] = GetClosestConsoleColour(bmp.GetPixel(x,y), transparancy);
				}
			}
           
		}


		ConsoleColor GetClosestConsoleColour(Color color, bool transparancy = false)
		{

			Color[] cCs =
			{
			Color.Black, //Black
			Color.FromArgb(255,0,0,255),	//Dark Blue
			Color.FromArgb(255,0,128,0),		//Dark Green
			Color.FromArgb(255,0,128,128),		//Dark Cyan
			Color.FromArgb(255,128,0,0),		//Dark Red
			Color.FromArgb(255,128,0,128),		//Dark Magenta
			Color.FromArgb(255,128,128,0),		//Dark Yellow
			Color.FromArgb(255,192,192,192),		//Grey
			Color.FromArgb(255,128,128,128),		//Dark Grey
			Color.LightBlue,				//Blue
			Color.Green,					//Green
			Color.Cyan,						//Cyan
			Color.Red,						//Red
			Color.Magenta,					//Magenta
			Color.Yellow,					//Yellow
			Color.White						//White
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
			if (transparancy)
			{
				if (color.A < 255 || color == Color.Black)
				{
					return ConsoleColor.Black;
				}

				i = 1; // Dont let colors that aren't EXACTLY black turn to black, because black is used to indicate transparency in this case

			}
			
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
}
