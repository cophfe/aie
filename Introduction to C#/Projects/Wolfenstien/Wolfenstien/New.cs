using System;
using System.Drawing;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;


namespace Wolfenstien
{
    struct CoordInt
    {
        public int X, Y;
        public CoordInt(int X, int Y)
        {
            this.Y = Y;
            this.X = X;
        }
    }
    class Program
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


        static string[] mapFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Images");
        static ColouredCharacter[,] cScreen, lastCScreen;
        static CoordInt topLeft, bottomRight;
        static int widthMultiplier = 1; // 1 or 2. if it is 2 then the pixels will be square.
        [STAThread]
        static void Main(string[] args)
        {
            //-----------------------------------------------------------
            //-------------CONSOLEHELPER FROM STACKOVERFLOW--------------
            //-----------------------------------------------------------
            //just need it to change the console font
            ConsoleHelper.SetCurrentFont("NSimSun", 5);
            //-----------------------------------------------------------
            //Console.BackgroundColor = ConsoleColor.DarkBlue;
            //Console.Clear();

            //Console.ReadKey(true);

            Console.WindowHeight = 193; //FIXED FOR NOW
            Console.WindowWidth = 629; //FIXED FOR NOW
            //screen bounds (canvas for rendering stuff) (fixed for now)
            topLeft = new CoordInt(15, 4);
            bottomRight = new CoordInt(14, 43);
            int screenX = Console.WindowWidth, screenY = Console.WindowHeight;
            int canvasX = screenX - topLeft.X - bottomRight.X;
            int canvasY = screenY - topLeft.Y - bottomRight.Y;
            cScreen = new ColouredCharacter[canvasX, canvasY];
            lastCScreen = new ColouredCharacter[canvasX, canvasY];

            Texture GUI = new Texture(new Bitmap(Directory.GetCurrentDirectory() + @"\Images\" + "GUI.png"));
            Write.Initiate();
            Write.Render(GUI.pixelArray);
            //Console.ReadKey(true);
            Texture[] wallTextures = new Texture[(int)CoordValue.Length - 1];

            for (int i = 0; i < wallTextures.Length; i++)
            {
                wallTextures[i] = new Texture(new Bitmap(@$"{Directory.GetCurrentDirectory()}\Images\{i + 1}.png"));
            }
            int texWidth = wallTextures[0].pixelArray.GetLength(0);
            int texHeight = wallTextures[0].pixelArray.GetLength(1);

            Map map = new Map(new Bitmap(Directory.GetCurrentDirectory() + @"\Images\" + "map.png"));
            List<Sprite> spriteList = map.GetSprites();
            CoordValue[,] worldMap = map.MapValues;

            Console.CursorVisible = false;
            Vector2 pPos = map.PlayerStart;
            Vector2 pDir = new Vector2(-1, 0); //ALWAYS NEEDS TO BE NORMALISED!!
            Vector2 canvasPos = new Vector2(0, 0.66f);
            float time;
            float lastTime;
            float moveSpeed = .25f;
            float rotSpeed = MathF.PI / 32;
            //Write.Initiate();
            Random rand = new Random();


            bool fastRendering = true;
            bool singleColor = false;



            Console.ForegroundColor = ConsoleColor.Black;



            const char shadeCharacter = 'X';
            char character = shadeCharacter;

            for (int y = 0; y < canvasY; y++)
            {

                if (y > canvasY / 2)
                {
                    for (int x = 0; x < canvasX; x++)
                    {
                        cScreen[x, y] = new ColouredCharacter(ConsoleColor.DarkGray, ' ');
                    }
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                }
                else
                {
                    for (int x = 0; x < canvasX; x++)
                    {
                        cScreen[x, y] = new ColouredCharacter(ConsoleColor.Black, ' ');
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition(topLeft.X, y + topLeft.Y);
                Console.Write(new string(' ', canvasX));
            }
            lastCScreen = cScreen.Clone() as ColouredCharacter[,];
            bool playing = true;

            //raycasting
            bool hit, side;
            int mapX, mapY, stepX, stepY, lineHeight, drawStart, drawEnd;
            float cameraX, rayDirX, rayDirY, sideDistX, sideDistY, deltaDistX, deltaDistY, perpWallDist;
            char c;
            //texture stuff
            CoordValue texture;
            int texX, texY;
            float wallX, step, texPos;
            //sprite stuff
            bool sprHit;
            float[] distArray = new float[canvasX];

            while (playing)
            {

                for (int x = 0; x < canvasX; x++)
                {
                    for (int y = 0; y < canvasY; y++)
                    {
                        if (y > canvasY / 2)
                            cScreen[x, y] = new ColouredCharacter(ConsoleColor.DarkGray, ' ');
                        else
                            cScreen[x, y] = new ColouredCharacter(ConsoleColor.Black, ' ');


                    }

                }
                for (int x = 0; x < canvasX; x++)
                {

                }
                //lastFrame = screen;
                for (int x = 0; x < canvasX; x++)
                {
                    //calculate ray position and direction
                    cameraX = 2 * x / (float)(canvasX) - 1; //x-coordinate in camera space
                    rayDirX = pDir.X + canvasPos.X * cameraX;
                    rayDirY = pDir.Y + canvasPos.Y * cameraX;
                    //which box of the map we're in
                    mapX = (int)(pPos.X);
                    mapY = (int)(pPos.Y);



                    //length of ray from one x or y-side to next x or y-side
                    deltaDistX = Math.Abs(1 / rayDirX);
                    deltaDistY = Math.Abs(1 / rayDirY);


                    hit = false;
                    side = false;
                    sprHit = false;
                    //calculate step and initial sideDist
                    if (rayDirX < 0)
                    {
                        stepX = -1;
                        sideDistX = (pPos.X - mapX) * deltaDistX;
                    }
                    else
                    {
                        stepX = 1;
                        sideDistX = (mapX + 1 - pPos.X) * deltaDistX;
                    }
                    if (rayDirY < 0)
                    {
                        stepY = -1;
                        sideDistY = (pPos.Y - mapY) * deltaDistY;
                    }
                    else
                    {
                        stepY = 1;
                        sideDistY = (mapY + 1 - pPos.Y) * deltaDistY;
                    }
                    //perform DDA (line making)
                    while (!hit)
                    {
                        //jump to next map square, OR in x-direction, OR in y-direction
                        if (sideDistX < sideDistY)
                        {
                            sideDistX += deltaDistX;
                            mapX += stepX;
                            side = false;
                        }
                        else
                        {
                            sideDistY += deltaDistY;
                            mapY += stepY;
                            side = true;
                        }
                        //Check if ray has hit a wall
                        if (worldMap[mapX, mapY] > 0)
                            hit = true;
                    }
                    //Calculate distance projected on camera direction 
                    //for sprites distance should stay the same for every line
                    if (!side)
                    {
                        c = ' ';
                        perpWallDist = (mapX - pPos.X + (1 - stepX) / 2) / rayDirX;
                    }
                    else
                    {
                        perpWallDist = (mapY - pPos.Y + (1 - stepY) / 2) / rayDirY;
                        c = shadeCharacter;
                    }


                    //Calculate height of line to draw on screen
                    lineHeight = (int)MathF.Max((canvasY / perpWallDist), 2);

                    //calculate lowest and highest pixel to fill in current stripe
                    drawStart = -lineHeight / 2 + canvasY / 2;
                    if (drawStart < 0) drawStart = 0;
                    drawEnd = lineHeight / 2 + canvasY / 2;
                    if (drawEnd >= canvasY) drawEnd = canvasY - 1;

                     
                        
                    

                    if (!singleColor)
                    {
                        //--------------TEXTURES N STUFF--------------
                        //Find texture
                        texture = worldMap[mapX, mapY];

                        //find point on wall hit

                        if (!side)
                            wallX = pPos.Y + perpWallDist * rayDirY;
                        else
                            wallX = pPos.X + perpWallDist * rayDirX;
                        wallX -= (int)wallX;

                        //find point on texture hit
                        texX = (int)(wallX * texWidth);
                        if (!side && rayDirX > 0)
                            texX = texWidth - texX - 1;
                        if (side && rayDirY < 0)
                            texX = texWidth - texX - 1;

                        // How much to increase the texture coordinate per screen pixel
                        step = texHeight / (float)lineHeight;

                        // Starting texture coordinate
                        texPos = (drawStart - canvasY / 2 + lineHeight / 2) * step;
                        for (int y = drawStart; y < drawEnd; y++)
                        {
                            // Cast the texture coordinate to integer, and mask with (texHeight - 1) in case of overflow
                            texY = (int)texPos & (texHeight - 1);
                            texPos += step;
                            ConsoleColor color = wallTextures[(int)texture - 1].pixelArray[texX, texY];
                            cScreen[x, y] = new ColouredCharacter(color, c);
                        }
                        //--------------------------------------------------
                        //----------------SPRITES N STUFF-------------------


                    }
                    else
                    {
                        //choose wall color

                        ConsoleColor color;
                        switch (worldMap[mapX, mapY])
                        {
                            case CoordValue.StoneWall: color = ConsoleColor.Gray; break;
                            case CoordValue.WoodenWall: color = ConsoleColor.DarkYellow; break;
                            case CoordValue.BlueWall: color = ConsoleColor.Blue; break;
                            case CoordValue.DefaultWall: color = ConsoleColor.Green; break;
                            default: color = ConsoleColor.Yellow; break;
                        }

                        WriteX(x, drawStart, drawEnd, new ColouredCharacter(color, c));
                    }


                }
                //Write.Render(screen);

                if (!cScreen.Equals(lastCScreen))
                {
                    if (fastRendering)
                    {
                        Render(); //takes between 10 and 100 ms depending on how much needs to be changed
                    }
                    else
                    {
                        DrawScene(); //takes like 6 weeks depending on how much needs to be changed
                    }
                }






                lastCScreen = cScreen.Clone() as ColouredCharacter[,];
                float oldDirX, oldPlaneX;
                float oldDirY, oldPlaneY;
                //if (Console.KeyAvailable)
                switch (Console.ReadKey(true).Key)
                {

                    case ConsoleKey.W:
                        if (worldMap[(int)(pPos.X + pDir.X * moveSpeed), (int)(pPos.Y)] <= 0)
                            pPos.X += pDir.X * moveSpeed;
                        if (worldMap[(int)(pPos.X), (int)(pPos.Y + pDir.X * moveSpeed)] <= 0)
                            pPos.Y += pDir.Y * moveSpeed;
                        break;
                    case ConsoleKey.S:
                        if (worldMap[(int)(pPos.X - pDir.X * moveSpeed), (int)(pPos.Y)] <= 0)
                            pPos.X -= pDir.X * (float)moveSpeed;
                        if (worldMap[(int)(pPos.X), (int)(pPos.Y - pDir.X * moveSpeed)] <= 0)
                            pPos.Y -= pDir.Y * (float)moveSpeed;
                        break;
                    case ConsoleKey.A:
                        oldDirX = pDir.X;
                        pDir.X = pDir.X * MathF.Cos(rotSpeed) - pDir.Y * MathF.Sin(rotSpeed);
                        pDir.Y = oldDirX * MathF.Sin(rotSpeed) + pDir.Y * MathF.Cos(rotSpeed);
                        oldPlaneX = canvasPos.X;
                        canvasPos.X = canvasPos.X * MathF.Cos(rotSpeed) - canvasPos.Y * MathF.Sin(rotSpeed);
                        canvasPos.Y = oldPlaneX * MathF.Sin(rotSpeed) + canvasPos.Y * MathF.Cos(rotSpeed);
                        break;
                    case ConsoleKey.D:
                        oldDirX = pDir.X;
                        pDir.X = pDir.X * MathF.Cos(-rotSpeed) - pDir.Y * MathF.Sin(-rotSpeed);
                        pDir.Y = oldDirX * MathF.Sin(-rotSpeed) + pDir.Y * MathF.Cos(-rotSpeed);
                        oldPlaneX = canvasPos.X;
                        canvasPos.X = canvasPos.X * MathF.Cos(-rotSpeed) - canvasPos.Y * MathF.Sin(-rotSpeed);
                        canvasPos.Y = oldPlaneX * MathF.Sin(-rotSpeed) + canvasPos.Y * MathF.Cos(-rotSpeed);
                        break;
                    case ConsoleKey.Q:
                        fastRendering = !fastRendering;
                        break;
                    case ConsoleKey.E:
                        singleColor = !singleColor;
                        break;

                }
                while (Console.KeyAvailable)
                    Console.ReadKey(true); // skips previous input chars

                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        static void Render()
        {
            SafeFileHandle h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
            short sHeight = (short)cScreen.GetLength(1);
            short sWidth = (short)cScreen.GetLength(0);
            CharInfo[] buf = new CharInfo[cScreen.Length];
            SmallRect rect = new SmallRect() { Left = (short)topLeft.X, Top = (short)topLeft.Y, Right = (short)(sWidth + topLeft.X), Bottom = (short)(sHeight + topLeft.Y) };
            short attribute = 0; // colour of foreground from 0 to 15

            for (int y = 0; y < sHeight; y++)
            {
                for (int x = 0; x < sWidth; x++)
                {

                    buf[x + y * sWidth].Attributes = (short)(attribute | ((ushort)cScreen[x, y].color << 4));
                    buf[x + y * sWidth].Char.AsciiChar = (byte)cScreen[x, y].character;
                }
            }

            WriteConsoleOutput(h, buf,
                new Coord() { X = sWidth, Y = sHeight },
                new Coord() { X = 0, Y = 0 },
                ref rect);
        }

        static void DrawScene()
        {


            int xSize = cScreen.GetLength(0);
            int ySize = cScreen.GetLength(1);
            bool newColor = false;
            Console.SetCursorPosition(0, 0);
            int start;
            ColouredCharacter last = cScreen[0, 0];

            for (int y = 0; y < ySize; y++)
            {
                start = 0;
                for (int x = 0; x < xSize; x++)
                {
                    if (newColor)
                    {
                        if (!cScreen[x, y].Equals(last))
                        {
                            newColor = false;
                            Console.SetCursorPosition(start * widthMultiplier + topLeft.X, y + topLeft.Y);

                            Console.BackgroundColor = last.color;
                            Console.Write(new String(cScreen[start, y].character, (x - start) * widthMultiplier));
                        }
                        if (x == xSize - 1)
                        {
                            newColor = false;
                            Console.SetCursorPosition(start * widthMultiplier + topLeft.X, y + topLeft.Y);
                            Console.BackgroundColor = last.color;
                            Console.Write(new String(cScreen[start, y].character, (x - start + 1) * widthMultiplier));
                            continue;
                        }
                        if (cScreen[x, y].Equals(lastCScreen[x, y]))
                        {
                            newColor = false;
                            Console.SetCursorPosition(start * widthMultiplier + topLeft.X, y + topLeft.Y);
                            Console.BackgroundColor = last.color;
                            Console.Write(new String(cScreen[start, y].character, (x - start) * widthMultiplier));
                        }



                    }
                    if (!cScreen[x, y].Equals(lastCScreen[x, y]) && !newColor)
                    {

                        newColor = true;
                        last = cScreen[x, y];
                        start = x;
                    }




                }

            }


        }
        static void WriteX(int x, int drawStart, int drawEnd, ColouredCharacter cChar)
        {
            for (int y = drawStart; y < drawEnd; y++)
            {
                cScreen[x, y] = cChar;
                //Console.SetCursorPosition(x, y);
                //Console.BackgroundColor = color;
                //Console.Write(' ');
            }

        }
    }
}
