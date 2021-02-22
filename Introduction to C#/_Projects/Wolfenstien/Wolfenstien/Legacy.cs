//using System;
//using System.Drawing;
//using System.Collections.Generic;
//using System.Numerics;

//namespace Wolfenstien
//{
//    class Program
//    {
//        static ConsoleColor[,] screen, lastFrame;
//        static void Main(string[] args)
//        {
//            //-----------------------------------------------------------
//            //-------------CONSOLEHELPER FROM STACKOVERFLOW--------------
//            //-----------------------------------------------------------
//            //just need it to change the console font
//            ConsoleHelper.SetCurrentFont("NSimSun", 5);
//            //-----------------------------------------------------------
//            Console.CursorVisible = false;
//            Vector2 pPos = new Vector2(22, 12);
//            Vector2 pDir = new Vector2(-1, 0);
//            Vector2 canvasPos = new Vector2(0, 0.66f);
//            float time;
//            float lastTime;
//            float moveSpeed = .5f;
//            float rotSpeed = MathF.PI / 16;
//            Write.Initiate();
//            Console.WindowHeight = Console.LargestWindowHeight - 10;
//            Console.WindowWidth = (Console.LargestWindowWidth - 10);
//            int screenX = Console.WindowWidth / 2, screenY = Console.WindowHeight;
//            int maxHeight = screenY;

//            screen = new ConsoleColor[screenX, screenY];

//            int mapWidth = 24, mapHeight = 24;
//            int[,] worldMap =
//            {
//              {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
//              {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
//            };

//            while (true)
//            {
//                //Console.SetCursorPosition(0, 0);
//                //Console.Write(new string(' ', screenX * screenY));
//                for (int x = 0; x < screenX; x++)
//                {
//                    for (int y = 0; y < screenY; y++)
//                    {
//                        screen[x, y] = ConsoleColor.Black;
//                    }

//                }
//                for (int x = 0; x < screenX; x++)
//                {
//                    //calculate ray position and direction
//                    float cameraX = 2 * x / (float)screenX - 1; //x-coordinate in camera space
//                    float rayDirX = pDir.X + canvasPos.X * cameraX;
//                    float rayDirY = pDir.Y + canvasPos.Y * cameraX;
//                    //which box of the map we're in
//                    int mapX = (int)(pPos.X);
//                    int mapY = (int)(pPos.Y);

//                    //length of ray from current position to next x or y-side
//                    float sideDistX;
//                    float sideDistY;

//                    //length of ray from one x or y-side to next x or y-side
//                    float deltaDistX = Math.Abs(1 / rayDirX);
//                    float deltaDistY = Math.Abs(1 / rayDirY);
//                    float perpWallDist;

//                    //what direction to step in x or y-direction (either +1 or -1)
//                    int stepX;
//                    int stepY;

//                    int hit = 0; //was there a wall hit?
//                    int side = 0; //was a NS or a EW wall hit?
//                                  //calculate step and initial sideDist
//                    if (rayDirX < 0)
//                    {
//                        stepX = -1;
//                        sideDistX = (pPos.X - mapX) * deltaDistX;
//                    }
//                    else
//                    {
//                        stepX = 1;
//                        sideDistX = (mapX + 1 - pPos.X) * deltaDistX;
//                    }
//                    if (rayDirY < 0)
//                    {
//                        stepY = -1;
//                        sideDistY = (pPos.Y - mapY) * deltaDistY;
//                    }
//                    else
//                    {
//                        stepY = 1;
//                        sideDistY = (mapY + 1 - pPos.Y) * deltaDistY;
//                    }
//                    //perform DDA
//                    while (hit == 0)
//                    {
//                        //jump to next map square, OR in x-direction, OR in y-direction
//                        if (sideDistX < sideDistY)
//                        {
//                            sideDistX += deltaDistX;
//                            mapX += stepX;
//                            side = 0;
//                        }
//                        else
//                        {
//                            sideDistY += deltaDistY;
//                            mapY += stepY;
//                            side = 1;
//                        }
//                        //Check if ray has hit a wall
//                        if (worldMap[mapX, mapY] > 0) hit = 1;
//                    }
//                    //Calculate distance projected on camera direction (Euclidean distance will give fisheye effect!)
//                    if (side == 0)
//                        perpWallDist = (mapX - pPos.X + (1 - stepX) / 2) / rayDirX;
//                    else
//                        perpWallDist = (mapY - pPos.Y + (1 - stepY) / 2) / rayDirY;

//                    //Calculate height of line to draw on screen
//                    int lineHeight = (int)MathF.Max((screenY / perpWallDist), 2);

//                    //calculate lowest and highest pixel to fill in current stripe
//                    int drawStart = -lineHeight / 2 + screenY / 2;
//                    if (drawStart < 0) drawStart = 0;
//                    int drawEnd = lineHeight / 2 + screenY / 2;
//                    if (drawEnd >= screenY) drawEnd = screenY - 1;

//                    //choose wall color
//                    ConsoleColor color;
//                    switch (worldMap[mapX, mapY] + 4 * side)
//                    {
//                        case 1: color = ConsoleColor.Red; break; //red
//                        case 2: color = ConsoleColor.Green; break; //green
//                        case 3: color = ConsoleColor.Blue; break; //blue
//                        case 4: color = ConsoleColor.White; break; //white
//                        case 5: color = ConsoleColor.DarkRed; break; //red
//                        case 6: color = ConsoleColor.DarkGreen; break; //green
//                        case 7: color = ConsoleColor.DarkBlue; break; //blue
//                        case 8: color = ConsoleColor.Gray; break; //white
//                        default: color = ConsoleColor.Yellow; break; //yellow
//                    }

//                    //give x and y sides different brightness
//                    //if (side == 1) { color = color / 2; }

//                    DrawX(x, drawStart, drawEnd, color);

//                }
//                //Write.Render(screen);
//                DrawScene(screenX, screenY, screen);
//                float oldDirX, oldPlaneX;
//                switch (Console.ReadKey(true).Key)
//                {
//                    case ConsoleKey.W:
//                        if (worldMap[(int)(pPos.X + pDir.X * moveSpeed), (int)(pPos.Y)] == 0)
//                            pPos.X += pDir.X * moveSpeed;
//                        if (worldMap[(int)(pPos.X), (int)(pPos.Y + pDir.X * moveSpeed)] == 0)
//                            pPos.Y += pDir.X * moveSpeed;
//                        break;
//                    case ConsoleKey.S:
//                        if (worldMap[(int)(pPos.X - pDir.X * moveSpeed), (int)(pPos.Y)] == 0)
//                            pPos.X -= pDir.X * (float)moveSpeed;
//                        if (worldMap[(int)(pPos.X), (int)(pPos.Y - pDir.X * moveSpeed)] == 0)
//                            pPos.Y -= pDir.X * (float)moveSpeed;
//                        break;
//                    case ConsoleKey.A:
//                        oldDirX = pDir.X;
//                        pDir.X = pDir.X * MathF.Cos(rotSpeed) - pDir.X * MathF.Sin(rotSpeed);
//                        pDir.X = oldDirX * MathF.Sin(rotSpeed) + pDir.X * MathF.Cos(rotSpeed);
//                        oldPlaneX = canvasPos.X;
//                        canvasPos.X = canvasPos.X * MathF.Cos(rotSpeed) - canvasPos.Y * MathF.Sin(rotSpeed);
//                        canvasPos.Y = oldPlaneX * MathF.Sin(rotSpeed) + canvasPos.Y * MathF.Cos(rotSpeed);
//                        break;
//                    case ConsoleKey.D:
//                        oldDirX = pDir.X;
//                        pDir.X = pDir.X * MathF.Cos(-rotSpeed) - pDir.X * MathF.Sin(-rotSpeed);
//                        pDir.X = oldDirX * MathF.Sin(-rotSpeed) + pDir.X * MathF.Cos(-rotSpeed);
//                        oldPlaneX = canvasPos.X;
//                        canvasPos.X = canvasPos.X * MathF.Cos(-rotSpeed) - canvasPos.Y * MathF.Sin(-rotSpeed);
//                        canvasPos.Y = oldPlaneX * MathF.Sin(-rotSpeed) + canvasPos.Y * MathF.Cos(-rotSpeed);
//                        break;
//                }

//                Console.BackgroundColor = ConsoleColor.Black;

//            }
//        }

//        static void DrawScene(int xSize, int ySize, ConsoleColor[,] screen)
//        {
//            Console.SetCursorPosition(0, 0);
//            int start = 0;
//            ConsoleColor last = screen[0, 0];
//            List<ColouredString> strings = new List<ColouredString>(50);
//            for (int y = 0; y < ySize; y++)
//            {
//                start = 0;
//                last = screen[0, y];
//                for (int x = 0; x < xSize; x++)
//                {
//                    if (screen[x, y] != last)
//                    {
//                        strings.Add(new ColouredString(last, 'X', 2 * (x - start)));
//                        Console.BackgroundColor = last;
//                        Console.Write(new String('X', 2 * (x - start)));

//                        start = x;


//                    }
//                    if (x == xSize - 1)
//                    {
//                        strings.Add(new ColouredString(last, 'X', 2 * (xSize - 1)));
//                        Console.BackgroundColor = last;
//                        Console.Write(new String('Y', 2 * (xSize - 1)));
//                    }
//                    last = screen[x, y];

//                }
//                // Console.Write('\n');
//            }
//            for (int i = 0; i < strings.Count; i++)
//            {
//                strings[i].Write();

//            }
//        }
//        static void DrawX(int x, int drawStart, int drawEnd, ConsoleColor color)
//        {
//            for (int y = drawStart; y < drawEnd; y++)
//            {
//                screen[x, y] = color;
//                //Console.SetCursorPosition(x, y);
//                //Console.BackgroundColor = color;
//                //Console.Write(' ');
//            }
//            Console.BackgroundColor = ConsoleColor.Black;

//        }
//    }
//}
