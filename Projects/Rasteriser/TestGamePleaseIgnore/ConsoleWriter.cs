using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Drawing;

class ConsoleWriter
{
    [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool WriteConsoleOutput(
      SafeFileHandle hConsoleOutput,
      CharInfo[] lpBuffer,
      Coord dwBufferSize,
      Coord dwBufferCoord,
      ref SmallRect lpWriteRegion);

    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
        public short X;
        public short Y;

        public Coord(short X, short Y)
        {
            this.X = X;
            this.Y = Y;
        }
    };

    [StructLayout(LayoutKind.Explicit)]
    public struct CharUnion
    {
        [FieldOffset(0)] public char UnicodeChar;
        [FieldOffset(0)] public byte AsciiChar;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct CharInfo
    {
        [FieldOffset(0)] public CharUnion Char;
        [FieldOffset(2)] public short Attributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;
    }

    [STAThread]
    public static void Render(ConsoleColor[,] clr)
    {
        Console.WindowHeight = Console.LargestWindowHeight;
        Console.WindowWidth = Console.LargestWindowWidth;
        SafeFileHandle h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
        short sHeight = (short)clr.GetLength(1);
        short sWidth = (short)(clr.GetLength(0) * 2);
        Random rand = new Random();
        CharInfo[] buf = new CharInfo[clr.Length * 2];
        SmallRect rect = new SmallRect() { Left = 0, Top = 0, Right = sWidth, Bottom = sHeight };
        byte character = (byte)' ';
        short attribute = 15;
        if (!h.IsInvalid)
        {
            int i = 0;
            for (int y = 0; y < sHeight; y++)
            {
                for (int x = 0; x < sWidth -1;i++)
                {
                    
                    buf[x + y*sWidth].Attributes = (short)(attribute | ((int)clr[i, y] << 4));
                    buf[x + y * sWidth].Char.AsciiChar = (byte)(character);
                    buf[x +1+ y * sWidth].Attributes = (short)(attribute | ((int)clr[i, y] << 4));
                    buf[x +1+ y * sWidth].Char.AsciiChar = (byte)(character);
                    x += 2;
                }
                i = 0;
            }
            
            bool b = WriteConsoleOutput(h, buf,
                    new Coord() { X = sWidth, Y = sHeight },
                    new Coord() { X = 0, Y = 0 },
                    ref rect);
        }
        Console.ReadKey();
    }

    ConsoleColor GetClosestConsoleColour(Color color, bool transparancy = false)
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
            distance[x] = vector.Magnitude;
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

