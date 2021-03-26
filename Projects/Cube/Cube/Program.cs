using System;

namespace Cube
{
    class Program
    {
        static void Main(string[] args)
        {
            bool doing = true;
            ConsoleHelper.SetCurrentFont("NSimSun",5);
            Console.Clear();
            Console.WindowHeight = Console.LargestWindowHeight - 10;
            Console.WindowWidth = Console.WindowHeight * 2;
            Console.CursorVisible = false;
            while (doing)
            {
                bool drawEdges = true;
                Vector3 camPos = new Vector3(0, 0, 0);
                Vector3 cubePos = new Vector3(0, 0, -5);
                float camMaxDist = 1000;
                float camAngleY = 0; //1D angle of camera;
                float camAngleX = 0;
                float camAngleMax = MathF.PI/2;
                float screenWidth = 2 * MathF.Tan(camAngleMax / 2) * camMaxDist;

                Vector3 testPoint = new Vector3(0, 0, 5);

                int screenX = Console.WindowWidth;
                int screenY = Console.WindowHeight;


                Vector3[] points = new Vector3[8] { new Vector3(-1, -1, -1), new Vector3(1, -1, -1), new Vector3(-1, 1, -1), new Vector3(-1, -1, 1), new Vector3(1, -1, 1), new Vector3(1, 1, -1), new Vector3(-1, 1, 1), new Vector3(1, 1, 1) };
                int[,] edges = new int[12, 2] { { 0, 3 }, { 3, 6 }, { 6, 2 }, { 2, 0 }, { 4, 1 }, { 1, 5 }, { 5, 7 }, { 7, 4 }, { 1, 0 }, { 4, 3 }, { 7, 6 }, { 5, 2 } };
                Vector2[] screenPoints = new Vector2[8];
                //find angle to point


                bool turning = true;
                while (turning)
                {
                    bool p2 = false;
                    int lastX, lastY;
                    Console.Clear();
                    for (int i = 0; i < points.Length; i++)
                    {
                        float x = 0;
                        float y = 0;
                        float xAngle = MathF.Atan(((points[i].x + cubePos.x) - camPos.x) / ((points[i].z + cubePos.z) - camPos.z)) + camAngleY;
                        float yAngle = MathF.Atan(((points[i].y + cubePos.y) - camPos.y) / ((points[i].z + cubePos.z) - camPos.z)) + camAngleX;
                        if ((xAngle < camAngleMax / 2 && xAngle > -camAngleMax / 2) && (yAngle < camAngleMax / 2 && yAngle > -camAngleMax / 2))
                        {
                            x = (screenX / 2) - (screenX / 2 * ((xAngle) / (camAngleMax / 2)));
                            y = (screenY / 2) - (screenY / 2 * ((yAngle) / (camAngleMax / 2)));
                            screenPoints[i] = new Vector2(x, y);

                        }
                        else
                        {
                            //find point that is closest to the right spot;
                            screenPoints[i] = null;
                        }




                    }
                    
                    if (drawEdges)
                    {
                        for (int i = 0; i < edges.Length / 2; i++)
                        {
                            MakeLine((int)screenPoints[edges[i, 0]].x, (int)screenPoints[edges[i, 0]].y, (int)screenPoints[edges[i, 1]].x, (int)screenPoints[edges[i, 1]].y);
                        }
                    }
                    else
                    {
                        foreach (Vector2 p in screenPoints)
                        {
                            if (p == null)
                                continue;
                            Console.SetCursorPosition((int)p.x, (int)p.y);
                            Console.Write("AA");
                        }
                    }
                    
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.LeftArrow:
                            camAngleY -= MathF.PI / 16;
                            camAngleY %= 2 * MathF.PI;
                            break;
                        case ConsoleKey.RightArrow:
                            camAngleY += MathF.PI / 16;
                            camAngleY %= 2 * MathF.PI;
                            break;
                        case ConsoleKey.UpArrow:
                            camAngleX -= MathF.PI / 16;
                            camAngleX %= 2 * MathF.PI;
                            break;
                        case ConsoleKey.DownArrow:
                            camAngleX += MathF.PI / 16;
                            camAngleX %= 2 * MathF.PI;
                            break;
                        case ConsoleKey.D:
                            camPos.z += .5f * MathF.Sin(camAngleY);
                            camPos.x += .5f * MathF.Cos(camAngleY);
                            break;
                        case ConsoleKey.A:
                            camPos.z -= .5f * MathF.Sin(camAngleY);
                            camPos.x -= .5f * MathF.Cos(camAngleY);
                            break;
                        case ConsoleKey.S:
                            camPos.x += .5f * MathF.Sin(camAngleY);
                            camPos.z += .5f * MathF.Cos(camAngleY);
                            break;
                        case ConsoleKey.W:
                            camPos.x -= .5f * MathF.Sin(camAngleY);
                            camPos.z -= .5f * MathF.Cos(camAngleY);
                            break;
                        case ConsoleKey.Escape:
                            turning = false;
                            doing = false;
                            break;
                        case ConsoleKey.R:
                            turning = false;
                            break;
                        case ConsoleKey.Z:
                            camPos.y += .5f;
                            break;
                        case ConsoleKey.X:
                            camPos.y -= .5f;
                            break;
                        case ConsoleKey.Spacebar:
                            drawEdges = !drawEdges;
                            break;
                    }
                }
            }
            Console.ReadKey();
        }
        static void MakeLine(int x0, int y0, int x1, int y1)
        {

            //my version of Bresenham's line algorithm was bugged so I yeeted this from rosettacode.com
            //get yoinked lol

            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;
            for (; ; )
            {
                Console.SetCursorPosition(x0, y0);
                Console.Write("X");
                if (x0 == x1 && y0 == y1) break;
                e2 = err;
                if (e2 > -dx) { err -= dy; x0 += sx; }
                if (e2 < dy) { err += dx; y0 += sy; }
            }
        }
    }
}
