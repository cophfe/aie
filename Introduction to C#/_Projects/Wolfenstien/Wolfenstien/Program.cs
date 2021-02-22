//using System;
//using System.Drawing;
//using System.Collections.Generic;
//using System.Numerics;
//using System.IO;

//namespace Wolfenstien
//{
//    struct CoordInt
//    {
//        public int X, Y;
//        public CoordInt(int X, int Y)
//        {
//            this.Y = Y;
//            this.X = X;
//        }
//    }
//    class Program
//    {

        
//        static string[] mapFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Images");
//        static ColouredCharacter[,] cScreen, lastCScreen;
//        static CoordInt topLeft, bottomRight;
//        static int widthMultiplier = 1; // 1 or 2. if it is 2 then the pixels will be square.
//        static void Main(string[] args)
//        {
//            //-----------------------------------------------------------
//            //-------------CONSOLEHELPER FROM STACKOVERFLOW--------------
//            //-----------------------------------------------------------
//            //just need it to change the console font
//            ConsoleHelper.SetCurrentFont("NSimSun", 5);
//            //-----------------------------------------------------------
//            //Console.BackgroundColor = ConsoleColor.DarkBlue;
//            //Console.Clear();
            
//            //Console.ReadKey(true);

//            Console.WindowHeight = 193; //FIXED FOR NOW
//            Console.WindowWidth = 629; //FIXED FOR NOW
//            //screen bounds (from each respective corner) (fixed for now)
//            topLeft = new CoordInt(15, 4);
//            bottomRight = new CoordInt(14, 43);
//            int screenX = Console.WindowWidth, screenY = Console.WindowHeight;
//            int canvasX = screenX - topLeft.X - bottomRight.X;
//            int canvasY = screenY - topLeft.Y - bottomRight.Y;
//            cScreen = new ColouredCharacter[canvasX, canvasY];
//            lastCScreen = new ColouredCharacter[canvasX, canvasY];

//            Texture t = new Texture(new Bitmap(Directory.GetCurrentDirectory() + @"\Images\" + "GUI.png"));
//            Write.Initiate();
//            Write.Render(t.pixelArray);
//            //Console.ReadKey(true);

//            Map map = new Map(new Bitmap(Directory.GetCurrentDirectory() + @"\Images\" + "map.png"));
//            CoordValue[,] worldMap = map.MapValues;

//            Console.CursorVisible = false;
//            Vector2 pPos = map.PlayerStart;
//            Vector2 pDir = new Vector2(-1, 0); //ALWAYS NEEDS TO BE NORMALISED!!
//            Vector2 canvasPos = new Vector2(0, 0.66f);
//            float time;
//            float lastTime;
//            float moveSpeed = .25f;
//            float rotSpeed = MathF.PI/32;
//            //Write.Initiate();
//            Random rand = new Random();



            
            

//            Console.ForegroundColor = ConsoleColor.Black;


            
//            const char shadeCharacter = 'X';
//            char character = shadeCharacter;

//            for (int y = 0; y < canvasY; y++)
//            {

//                if (y > canvasY / 2) 
//                {
//                    for (int x = 0; x < canvasX; x++)
//                    {
//                        cScreen[x, y] = new ColouredCharacter(ConsoleColor.DarkGray, ' ');
//                    }
//                    Console.BackgroundColor = ConsoleColor.DarkGray;
//                }
//                else
//                {
//                    for (int x = 0; x < canvasX; x++)
//                    {
//                        cScreen[x, y] = new ColouredCharacter(ConsoleColor.Black, ' ');
//                    }
//                    Console.BackgroundColor = ConsoleColor.Black;
//                }
//                Console.SetCursorPosition(topLeft.X, y + topLeft.Y);
//                Console.Write(new string(' ', canvasX));
//            }
//            lastCScreen = cScreen.Clone() as ColouredCharacter[,];
//            bool playing = true;
//            while (playing)
//            {
                
//                for (int x = 0; x < canvasX; x++)
//                {
//                    for (int y =0; y < canvasY; y++)
//                    {
//                        if (y > canvasY / 2)
//                            cScreen[x, y] = new ColouredCharacter(ConsoleColor.DarkGray, ' ');
//                        else
//                            cScreen[x, y] = new ColouredCharacter(ConsoleColor.Black, ' ');


//                    }

//                }

//                //lastFrame = screen;
//                for (int x = 0; x < canvasX/ widthMultiplier; x++)
//                {
//                    //calculate ray position and direction
//                    float cameraX = 2 * x / (float)(canvasX/ widthMultiplier) - 1; //x-coordinate in camera space
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
//                              //calculate step and initial sideDist
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
//                        if (worldMap[mapX,mapY] > 0) hit = 1;
//                    }
//                    //Calculate distance projected on camera direction (Euclidean distance will give fisheye effect!)
//                    if (side == 0) 
//                        perpWallDist = (mapX - pPos.X + (1 - stepX) / 2) / rayDirX;
//                    else 
//                        perpWallDist = (mapY - pPos.Y + (1 - stepY) / 2) / rayDirY;

//                    //Calculate height of line to draw on screen
//                    int lineHeight = (int)MathF.Max((canvasY / perpWallDist),2);

//                    //calculate lowest and highest pixel to fill in current stripe
//                    int drawStart = -lineHeight / 2 + canvasY / 2;
//                    if (drawStart < 0) drawStart = 0;
//                    int drawEnd = lineHeight / 2 + canvasY / 2;
//                    if (drawEnd >= canvasY) drawEnd = canvasY - 1;

//                    //choose wall color
//                    char c = ' ';
//                    ConsoleColor color;
//                    switch (worldMap[mapX,mapY])
//                    {
//                        case CoordValue.StoneWall: color = ConsoleColor.Gray; break;
//                        case CoordValue.WoodenWall: color = ConsoleColor.DarkYellow; break; 
//                        case CoordValue.BlueWall: color = ConsoleColor.Blue; break;
//                        case CoordValue.DefaultWall: color = ConsoleColor.Green; break;
//                        default: color = ConsoleColor.Yellow; break; 
//                    }
//                    if (side == 1)
//                    {
//                        c = character;
//                    }
//                    WriteX(x, drawStart, drawEnd, new ColouredCharacter(color, c));
//                }
//                //Write.Render(screen);
//                while (Console.KeyAvailable)
//                    Console.ReadKey(true); // skips previous input chars

//                DrawScene(); //takes between 90 and 150 ms depending on how much needs to be changed
//                lastCScreen = cScreen.Clone() as ColouredCharacter[,];
//                float oldDirX, oldPlaneX;
//                float oldDirY, oldPlaneY;
//                //if (Console.KeyAvailable)
//                    switch (Console.ReadKey(true).Key)
//                    {
//                        case ConsoleKey.W:
//                            if (worldMap[(int)(pPos.X +pDir.X * moveSpeed),(int)(pPos.Y)] <= 0) 
//                                pPos.X +=pDir.X * moveSpeed;
//                            if (worldMap[(int)(pPos.X),(int)(pPos.Y +pDir.X * moveSpeed)] <= 0) 
//                                pPos.Y +=pDir.Y * moveSpeed;
//                            break;
//                        case ConsoleKey.S:
//                            if (worldMap[(int)(pPos.X -pDir.X * moveSpeed), (int)(pPos.Y)] <= 0) 
//                                pPos.X -=pDir.X * (float)moveSpeed;
//                            if (worldMap[(int)(pPos.X), (int)(pPos.Y -pDir.X * moveSpeed)] <= 0) 
//                                pPos.Y -=pDir.Y * (float)moveSpeed;
//                            break;
//                        case ConsoleKey.A:
//                            oldDirX = pDir.X;
//                            pDir.X = pDir.X * MathF.Cos(rotSpeed) - pDir.Y * MathF.Sin(rotSpeed);
//                            pDir.Y = oldDirX * MathF.Sin(rotSpeed) + pDir.Y * MathF.Cos(rotSpeed);
//                            oldPlaneX = canvasPos.X;
//                            canvasPos.X = canvasPos.X * MathF.Cos(rotSpeed) - canvasPos.Y * MathF.Sin(rotSpeed);
//                            canvasPos.Y = oldPlaneX * MathF.Sin(rotSpeed) + canvasPos.Y * MathF.Cos(rotSpeed);
//                            break;
//                        case ConsoleKey.D:
//                            oldDirX =pDir.X;
//                            pDir.X =pDir.X * MathF.Cos(-rotSpeed) -pDir.Y * MathF.Sin(-rotSpeed);
//                            pDir.Y = oldDirX * MathF.Sin(-rotSpeed) + pDir.Y * MathF.Cos(-rotSpeed);
//                            oldPlaneX = canvasPos.X;
//                            canvasPos.X = canvasPos.X * MathF.Cos(-rotSpeed) - canvasPos.Y * MathF.Sin(-rotSpeed);
//                            canvasPos.Y = oldPlaneX * MathF.Sin(-rotSpeed) + canvasPos.Y * MathF.Cos(-rotSpeed);
//                            break;
//                        case ConsoleKey.Spacebar:
//                            if (character == ' ')
//                                character = shadeCharacter;
//                            else
//                                character = ' ';
//                            break;

                        
//                    }
                
//                Console.BackgroundColor = ConsoleColor.Black;
//            }
//        }

//        static void DrawScene()
//        {
//            if (cScreen.Equals(lastCScreen))
//            {
//                return;
//            }

//            int xSize = cScreen.GetLength(0);
//            int ySize = cScreen.GetLength(1);
//            bool newColor = false;
//            Console.SetCursorPosition(0, 0);
//            int start;
//            ColouredCharacter last = cScreen[0,0];
            
//            for (int y = 0; y < ySize; y++)
//            {
//                start = 0;
//                for (int x = 0; x < xSize; x++)
//                {
//                    if (newColor)
//                    {
//                        if (!cScreen[x, y].Equals(last))
//                        {
//                            newColor = false;
//                            Console.SetCursorPosition(start* widthMultiplier + topLeft.X, y + topLeft.Y);
                            
//                            Console.BackgroundColor = last.color;
//                            Console.Write(new String(cScreen[start, y].character, (x - start)* widthMultiplier));
//                        }
//                        if (x == xSize - 1)
//                        {
//                            newColor = false;
//                            Console.SetCursorPosition(start* widthMultiplier + topLeft.X, y + topLeft.Y);
//                            Console.BackgroundColor = last.color;
//                            Console.Write(new String(cScreen[start, y].character, (x - start + 1)* widthMultiplier));
//                            continue;
//                        }
//                        if (cScreen[x, y].Equals(lastCScreen[x, y]))
//                        {
//                            newColor = false;
//                            Console.SetCursorPosition(start* widthMultiplier + topLeft.X, y + topLeft.Y);
//                            Console.BackgroundColor = last.color;
//                            Console.Write(new String(cScreen[start, y].character, (x - start)*widthMultiplier));
//                        }



//                    }
//                    if (!cScreen[x, y].Equals(lastCScreen[x,y]) && !newColor)
//                    {
                        
//                        newColor = true;
//                        last = cScreen[x, y];
//                        start = x;
//                    }




//                }
                
//            }
            

//        }
//        static void WriteX(int x, int drawStart, int drawEnd, ColouredCharacter cChar)
//        {
//            for (int y = drawStart; y < drawEnd; y++)
//            {
//                cScreen[x, y] = cChar;
//                //Console.SetCursorPosition(x, y);
//                //Console.BackgroundColor = color;
//                //Console.Write(' ');
//            }
            
//        }
//    }
//}
