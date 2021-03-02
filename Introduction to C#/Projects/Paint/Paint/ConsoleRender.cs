using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Numerics;
using System.Drawing;

class ConsoleRender
{
    public static ConsoleColor[,] rasterisedScreen;
    public static TransparentConsoleColor[,] nullScreen;

	static IntPtr h;

    public static void Render(TransparentConsoleColor[,] clr, int startX = 0, int startY = 0)
    {
        h = PInvoke.CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
        int backgroundHeight = rasterisedScreen.GetLength(1), backgroundWidth = rasterisedScreen.GetLength(0);
        short sHeight = (short)clr.GetLength(1);
        short sWidth = (short)(2*clr.GetLength(0));
        CharInfo[] buf = new CharInfo[2*clr.Length];
        SmallRect rect = new SmallRect() { Left = (short)startX, Top = (short)startY, Right = (short)(startX + sWidth), Bottom = (short)(sHeight + startY) };
        short attribute = 2; // colour of foreground from 0 to 15
        Random rand = new Random();
        byte character = (byte)' '; 
        

        int i = 0;
        for (int y = 0; y < sHeight; y++)
        {
            for (int x = 0; x < sWidth - 1; i++)
            {
                if (clr[i, y] == TransparentConsoleColor.Null)
				{
                    if (inBounds(i + startX/2, y + startY, backgroundWidth, backgroundHeight))
					{
                        buf[x + y * sWidth].Attributes = (short)(attribute | ((int)rasterisedScreen[i+startX/2, y+ startY] << 4));
                        buf[x + y * sWidth].Char.asciiChar = (byte)(character);
                        buf[x + 1 + y * sWidth].Attributes = (short)(attribute | ((int)rasterisedScreen[i+startX/2, y+ startY] << 4));
                        buf[x + 1 + y * sWidth].Char.asciiChar = (byte)(character);
                        x += 2;
					}
					else
					{
                        buf[x + y * sWidth].Attributes = (short)(attribute | ((int)2 << 4));
                        buf[x + y * sWidth].Char.asciiChar = (byte)(character);
                        buf[x + 1 + y * sWidth].Attributes = (short)(attribute | ((int)2 << 4));
                        buf[x + 1 + y * sWidth].Char.asciiChar = (byte)(character);
                        x += 2;
					}
                }
				else
				{
                    buf[x + y * sWidth].Attributes = (short)(attribute | ((int)clr[i, y] << 4));
                    buf[x + y * sWidth].Char.asciiChar = (byte)(character);
                    buf[x + 1 + y * sWidth].Attributes = (short)(attribute | ((int)clr[i, y] << 4));
                    buf[x + 1 + y * sWidth].Char.asciiChar = (byte)(character);
                    x += 2;
				}
            }
            i = 0;
        }


        bool b = PInvoke.WriteConsoleOutput(h, buf,
                new Coord() { X = (short)(sWidth), Y = sHeight },
                new Coord() { X = 0, Y = 0 },
                ref rect);

    }

    public static TransparentConsoleColor[,] MakeCircle(int radius, Coord position, TransparentConsoleColor color, Coord lastPosition, out Coord renderCoord) 
    {
        Coord deltaPos = new Coord((short)((lastPosition.X - position.X)/2), (short)( lastPosition.Y - position.Y));
        int iY = deltaPos.Y < 0 ? -1 : 0; // if deltaPox.Y is more than zero iY will equal 1, else it will equal 0
        int iX = deltaPos.X < 0 ? -1 : 0;

        TransparentConsoleColor[,] circle = new TransparentConsoleColor[radius * 2 + Math.Abs(deltaPos.X) + 2, radius * 2 + Math.Abs(deltaPos.Y)];
		for (int x = 0; x < circle.GetLength(0); x++)
		{
			for (int y = 0; y < circle.GetLength(1); y++)
			{
                circle[x, y] = TransparentConsoleColor.Null;
			}
		}
        for (int x = 0; x < radius; x++)
		{
			for (int y = 0; y < radius; y++)
			{
                if (Math.Abs(x * x + y * y) < (radius * radius))
                {
                    circle[radius - x + deltaPos.X * iX, radius -1- y + deltaPos.Y * iY] = color;
                    circle[x + radius + deltaPos.X * iX, y + radius + deltaPos.Y * iY] = color;
                    circle[x + radius + deltaPos.X * iX, radius - 1 - y + deltaPos.Y * iY] = color;
                    circle[radius - x + deltaPos.X * iX, y + radius + deltaPos.Y * iY] = color;

                }

            }
		}
        
        renderCoord = new Coord(position.X - position.X % 2 - (2*deltaPos.X * iX + 2*radius), position.Y - radius - deltaPos.Y * iY);
        return circle;
	}

    public static void RasteriseCircle(int radius, Coord position, ConsoleColor color, ConsoleColor[,] background)
	{
        
        for (int x = 0; x < radius; x++)
        {
            for (int y = 0; y < radius; y++)
            {
                if (Math.Abs(x * x + y * y) < (radius * radius))
                {
                    if(inBounds(x + radius + position.X, y + radius + position.Y, background.GetLength(0), background.GetLength(1)))
                    {
                        background[x + radius + position.X, y + radius + position.Y] = color;
                    }
                    if (inBounds(x + radius + position.X, radius - 1 - y + position.Y, background.GetLength(0), background.GetLength(1)))
                    {
                        background[x + radius + position.X, radius - 1 - y + position.Y] = color;
                    }
                    if (inBounds(radius - x + position.X, y + radius + position.Y, background.GetLength(0), background.GetLength(1)))
                    {
                        background[radius - x + position.X, y + radius + position.Y] = color;
                    }
                    if (inBounds(radius - x + position.X, radius - 1 - y + position.Y, background.GetLength(0), background.GetLength(1)))
                    {
                        background[radius - x + position.X, radius - 1 - y + position.Y] = color;
                    }
                }

            }
        }
    }

    public static TransparentConsoleColor[,] MakeSquare(int radius, Coord position, TransparentConsoleColor color)
	{
        TransparentConsoleColor[,] square = new TransparentConsoleColor[radius * 2, radius * 2];

        for (int x = 0; x < 2 * radius; x++)
        {
            for (int y = 0; y < 2 * radius; y++)
            {
                square[x, y] = (TransparentConsoleColor)color;
            }
        }
       
        return square;
    }

    

    static bool inBounds(int indexX, int indexY, int lengthX, int lengthY)
    {
        return (indexX >= 0) && (indexX < lengthX) && (indexY >= 0) && (indexY < lengthY);
    }

    public static Color GetColor(ConsoleColor consoleColor)
	{
        Color[] colors =
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


        return colors[(int)consoleColor];
    }

}
    public enum TransparentConsoleColor
	{
        Null = -1,
        Black,
        DarkBlue,
        DarkGreen,
        DarkCyan,
        DarkRed,
        DarkMagenta,
        DarkYellow,
        DarkGray,
        Gray,
        Blue,
        Green,
        Cyan,
        Red,
        Magenta,
        Yellow,
        White
	}

