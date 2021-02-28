using System;
using System.IO;
using System.Drawing;
using System.Numerics;
// Used for p/invoke!
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

// SOLUTION IS NOT MINE. THIS CODE IS BASED OFF THE ANSWERS TO THIS QUESTION FROM STACKOVERFLOW
// --------------------------------------------------------------------------------------------
// https://stackoverflow.com/questions/2754518/how-can-i-write-fast-colored-output-to-console#comment67160243_2754674
// --------------------------------------------------------------------------------------------
// I am not using this program for any gain, I just want to see if I can work this out
// --------------------------------------------------------------------------------------------
// I still did a lot of research
// However it functions in exactly the same way as stackoverflow so if this was for an assignment it 
// would be plagurism lol
// --------------------------------------------------------------------------------------------
// Thank you stackoverflow user Chris Taylor!
// --------------------------------------------------------------------------------------------
// Why am I writing this when I am the only one that will read it?


//This class turns a 2D array of ConsoleColor into the screen buffer. 
//It also accepts a different character for each pixel

// I keep namespace because it is easiest
namespace Wolfenstien
{
    class Write
    {
        
        static SafeFileHandle h;
        //This writes to the console in color super fast by accessing some !unmanaged! functions from a library

        // This Attribute tells the runtime to load this !unmanaged! DLL
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern SafeFileHandle CreateFile( 
        string FileName,
        [MarshalAs(UnmanagedType.U4)] uint fileAccess,
        [MarshalAs(UnmanagedType.U4)] uint fileShare,
        IntPtr securityAttributes,
        [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
        [MarshalAs(UnmanagedType.U4)] int flags,
        IntPtr template);


        //Obviously this external method writes console output, including c o l o u r
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutput(
        SafeFileHandle hConsoleOutput,
        CharInfo[] lpBuffer,
        Coord dwBufferSize,
        Coord dwBufferCoord,
        ref SmallRect lpWriteRegion);

        //Memory is kinda unmanaged now so these attributes are important
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
        public static void Initiate()
        {
            h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
            //does not help :(
        }
        
        [STAThread]
        public static void Render(ConsoleColor[,] clr, int startX = 0, int startY = 0)
        {
            h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
            short sHeight = (short)clr.GetLength(1);
            short sWidth = (short)clr.GetLength(0);
            CharInfo[] buf = new CharInfo[clr.Length];
            SmallRect rect = new SmallRect() { Left = (short)startX, Top = (short)startY, Right = (short)(sWidth + startX), Bottom = (short)(sHeight + startY)};
            short attribute = 0; // colour of foreground from 0 to 15
            byte character = (byte)' ';
            
                for (int y = 0; y < sHeight; y++)
                {
                    for (int x = 0; x < sWidth; x++)
                    {

                        buf[x + y * sWidth].Attributes = (short)(attribute | ((ushort)clr[x, y] << 4));
                        buf[x + y * sWidth].Char.AsciiChar = (byte)character;
                    }
                }

                bool b = WriteConsoleOutput(h, buf,
                        new Coord() { X = sWidth, Y = sHeight },
                        new Coord() { X = 0, Y = 0 },
                        ref rect);
            
        }

        public static ConsoleColor GetClosestConsoleColour(Color color)
        {

            Color[] cCs =
            {
            Color.Black,
            Color.DarkBlue,
            Color.DarkGreen,
            Color.DarkCyan,
            Color.DarkRed,
            Color.DarkMagenta,
            Color.Brown, //What Colour is dark yellow??
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
                Vector3 vector = new Vector3(cCs[i].R - color.R, cCs[i].G - color.G, cCs[i].B - color.B);
                distance[i] = vector.Length();
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



}
