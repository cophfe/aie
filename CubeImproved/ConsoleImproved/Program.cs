using System;
using System.Numerics;
using System.Threading;

namespace ConsoleImproved
{
    class Program
    {
        static string screen;
        static int xSize;
        static int ySize;
        static int planeWidth = 2;
        static int planeHeight = 2;
        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                running = true;
                
                //-----------------------------------------------------------
                //-------------CONSOLEHELPER FROM STACKOVERFLOW--------------
                //-----------------------------------------------------------
                //just need it to change the console font
                ConsoleHelper.SetCurrentFont("NSimSun", 5);
                //-----------------------------------------------------------

                Console.Clear();
                
               
                    int height = 200;
                    int width = 400;
                    
                        Console.WindowWidth = width; // works on release build, not on debug
                   
                                       
                    Console.WindowHeight = height + 1;
                
                

                //create a string that holds the value of each pixel on the screen
                int size = height * width;
                xSize = width/ 2; // (resolution)
                ySize = height;

                //controls
                float turnAmount = MathF.PI / 32;
                float moveAmount = 0.5f;

                //camera
                Vector3 camPos = new Vector3(0, 0, 0);
                Vector3 camRot = new Vector3(0, 0, 0);
                float fov = MathF.PI / 3;

                //cube points (relative to objPos) and euler rotation
                Vector3 objPos = new Vector3(0, 0, -5f);
                Vector3 objRot = new Vector3(MathF.PI / 4, 0, MathF.PI / 4);
                float scale = 1; // unused right now
                Vector3[] objPoints = new Vector3[] { new Vector3(-1, -1, -1), new Vector3(1, -1, -1), new Vector3(-1, 1, -1), new Vector3(-1, -1, 1), new Vector3(1, -1, 1), new Vector3(1, 1, -1), new Vector3(-1, 1, 1), new Vector3(1, 1, 1) };
                int[,] edges = new int[12, 2] { { 0, 3 }, { 3, 6 }, { 6, 2 }, { 2, 0 }, { 4, 1 }, { 1, 5 }, { 5, 7 }, { 7, 4 }, { 1, 0 }, { 4, 3 }, { 7, 6 }, { 5, 2 } };
                RenderObject cube = new RenderObject(objPoints, objRot, objPos, scale, edges);
                
                bool rendering = true;
                int[] onScreenX = new int[8];
                int[] onScreenY = new int[8];

                //-----------------------------------------------------------
                //---------------THREAD CODE FROM STACKOVERFLOW--------------
                //-----------------------------------------------------------
                //just need it to use a second thread
                var thread = new Thread(() =>
                {
                    while (rendering)
                    {
                        var input = Console.ReadKey(true);

                        switch (input.Key)
                        {
                            case ConsoleKey.LeftArrow:




                                camRot.Y += turnAmount;

                                break;
                            case ConsoleKey.RightArrow:
                                camRot.Y -= turnAmount;

                                break;
                            case ConsoleKey.UpArrow:
                                camRot.X -= turnAmount;

                                break;
                            case ConsoleKey.DownArrow:
                                camRot.X += turnAmount;

                                break;
                            case ConsoleKey.W:
                                camPos.Z -= moveAmount;

                                break;
                            case ConsoleKey.S:
                                camPos.Z += moveAmount;

                                break;
                            case ConsoleKey.A:
                                camPos.X -= moveAmount;

                                break;
                            case ConsoleKey.D:
                                camPos.X += moveAmount;

                                break;
                            case ConsoleKey.Z:
                                camPos.Y -= moveAmount;
                                break;
                            case ConsoleKey.X:
                                camPos.Y += moveAmount;
                                break;
                            case ConsoleKey.R:
                                rendering = false;
                                break;
                        }
                    }
                });
                thread.IsBackground = true;
                thread.Start();

                while (rendering)
                {
                    Console.CursorVisible = false;
                    screen = new string(' ', size);
                    //setting up a matrix that converts objPoints from the objects local space to world space.
                    Matrix4x4 objectToWorldMatrix = GenerateMatrix(objPos, objRot);
                    //converting object points to world space with the matrix
                    Vector3[] objPointsWorld = TransformVectorArray(objPoints, objectToWorldMatrix);
                    

                    //convert from world to camera space to render.
                    Matrix4x4 worldToCameraMatrix = GenerateMatrix(-camPos, -camRot);

                    Vector3[] camPoints = TransformVectorArray(objPointsWorld, worldToCameraMatrix);
                    Vector2[] screenPoints = new Vector2[objPoints.Length];
                    //with a canvas at z = 1 in local camera space, finding the position of each point on that canvas
                    for (int i = 0; i < screenPoints.Length; i++)
                    {
                        //super simplified calculations to get what x would be on a triangle with the same ratio between x and z if z was one (camera space)
                        float temp = 1 / -camPoints[i].Z;
                        screenPoints[i].X = camPoints[i].X * temp;
                        screenPoints[i].Y = camPoints[i].Y * temp;

                        //normalize screenPoint so it is within the range 0 and 1 when on the canvas
                        screenPoints[i].X = (screenPoints[i].X + planeWidth / 2) / planeWidth;
                        screenPoints[i].Y = (screenPoints[i].Y + planeHeight / 2) / planeHeight;

                        // turn it into screen coordinates
                        
                        onScreenX[i] = (int)(screenPoints[i].X * xSize);
                        onScreenY[i] = (int)(screenPoints[i].Y * ySize);
                        if (temp < 0)
                        {
                            onScreenX[i] *= -1;
                            onScreenY[i] *= -1;
                        }
                        //try to make lines between points, only try to render if, when the new x divided by xSize is less than canvasX divided by 2 (same for y)
                        //int val = (int)((x * xSize) * 2 + (y * ySize) * xSize * 2);

                        //if (Math.Abs(screenPoints[i].X) < canvasX / 2 && Math.Abs(screenPoints[i].Y) < canvasY / 2)
                        //{
                        //    int val = onScreenX[i] * 2 + onScreenY[i] * xSize * 2;
                        //    screen = screen.Remove(val, 2).Insert(val, "AA");
                        //    //AddPointToScreen(screenPoints[i].X, screenPoints[i].Y);
                        //}
                    }
                    for (int i = 0; i < edges.Length / 2; i++)
                    {
                        // only make a line if at least one of the points is on screen
                        int x0 = onScreenX[edges[i, 0]], y0 = onScreenY[edges[i, 0]], x1 = onScreenX[edges[i, 1]], y1 = onScreenY[edges[i, 1]];
                        if ((( x0 >= 0 && x0 < xSize) && (y0 >= 0 && y0 < ySize)) || ((x1 >= 0 && x1 < xSize)&&(y1 >= 0 && y1 < ySize)))
                            MakeLine(x0, y0, x1, y1);
                        
                    }

                    objRot.Z += 0.02f;
                    objRot.X += 0.02f;
                    Console.SetCursorPosition(0, 0);

                    Console.Write(screen);
                    //System.Threading.Thread.Sleep(10);
                    //Console.Clear();
                }
            }
            
        }

        static void MakeLine(int x0, int y0, int x1, int y1)
        {

            //my version of Bresenham's line algorithm was bugged so I copied this from rosettacode.com
            //get yoinked lololol


            int dx = Math.Abs(x1 - x0);
            // if x0 < x1 sx = 1, else sx = -1
            int sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0);
            int sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2;
            int e2;

            for (; ; )
            {
                if (x0 > 0 && x0 < xSize && y0 > 0 && y0< ySize)
                {
                    int val = x0 * 2 + y0 * xSize * 2;
                    screen = screen.Remove(val, 2).Insert(val, "**");
                }
                
                if (x0 == x1 && y0 == y1) break;
                e2 = err;
                if (e2 > -dx) { err -= dy; x0 += sx; }
                if (e2 < dy) { err += dx; y0 += sy; }
            }
        }

        static void AddPointToScreen(float x, float y)
        {
            int onScreenX = (int)(x * xSize);
            int onScreenY = (int)(y * ySize);
            //Console.SetCursorPosition(onScreenX, onScreenY);
            //Console.Write("A");
            
        }

        protected static Matrix4x4 GenerateMatrix(Vector3 position, Vector3 rotation)
        {
            
            Matrix4x4 mX = new Matrix4x4(1, 0, 0, 0,
                                            0, MathF.Cos(rotation.X), MathF.Sin(rotation.X), 0,
                                            0, -MathF.Sin(rotation.X), MathF.Cos(rotation.X), 0,
                                            0, 0, 0, 1);

            Matrix4x4 mY = new Matrix4x4(MathF.Cos(rotation.Y), 0, -MathF.Sin(rotation.Y), 0,
                                            0, 1, 0, 0,
                                            MathF.Sin(rotation.Y), 0, MathF.Cos(rotation.Y), 0,
                                            0, 0, 0, 1);

            Matrix4x4 mZ = new Matrix4x4(MathF.Cos(rotation.Z), MathF.Sin(rotation.Z), 0, 0,
                                            -MathF.Sin(rotation.Z), MathF.Cos(rotation.Z), 0, 0,
                                            0, 0, 1, 0,
                                            0, 0, 0, 1);
            var mR = mX * mZ * mY;
            mR.M41 = position.X; mR.M42 = position.Y; mR.M43 = position.Z;
            return mR;
        }

        protected static Vector3[] TransformVectorArray(Vector3[] points, Matrix4x4 transformationMatrix)
        {
            var p = new Vector3[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                p[i] = TransformVector(points[i], transformationMatrix);
            }
            return p;
        }

        protected static Vector3 TransformVector(Vector3 point, Matrix4x4 transformationMatrix)
        {
                Vector3 p = new Vector3();
            
                p = new Vector3(point.X * transformationMatrix.M11 + point.Y * transformationMatrix.M21 + point.Z * transformationMatrix.M31 + transformationMatrix.M41,
                                              point.X * transformationMatrix.M12 + point.Y * transformationMatrix.M22 + point.Z * transformationMatrix.M32 + transformationMatrix.M42,
                                              point.X * transformationMatrix.M13 + point.Y * transformationMatrix.M23 + point.Z * transformationMatrix.M33 + transformationMatrix.M43);
                //Console.WriteLine($"point: {points[i].X}, {points[i].Y}, {points[i].Z}\npoint Transformed: {pointMoved.X}, {pointMoved.Y}, {pointMoved.Z}");
            
            return p;
        }
    }
}



//Matrix4x4 m = new Matrix4x4();
//float angle = 0;

//int x = Console.WindowWidth - 10;
//int y = Console.WindowHeight - 10;
//int r = y / 2;
//Vector3 p = new Vector3(1, 0, 0);

//while (true)
//{
//    float[,] m3x3 = new float[3, 3] { { MathF.Cos(angle), MathF.Sin(angle), 0 },
//                                              { -MathF.Sin(angle), MathF.Cos(angle), 0 },
//                                              { 0, 0, 0 } };
//    p = new Vector3(p.X * m3x3[0, 0] + p.Y * m3x3[0, 1] + p.Z * m3x3[0, 2], p.X * m3x3[1, 0] + p.Y * m3x3[1, 1] + p.Z * m3x3[1, 2], p.X * m3x3[2, 0] + p.Y * m3x3[2, 1] + p.Z * m3x3[2, 2]);

//    Console.SetCursorPosition(120 + (int)(p.X * r * 2), 60 + (int)(p.Y * r));
//    Console.Write("AA");
//    Console.ReadKey(true);
//    angle -= MathF.PI / 180;
//}

//Vector3 pTransformed = new Vector3(p.X * m.M11 + p.Y * m.M21 + p.Z * m.M31,
//                                    p.X * m.M12 + p.Y * m.M22 + p.Z * m.M32,
//                                    p.X * m.M13 + p.Y * m.M23 + p.Z * m.M33);

