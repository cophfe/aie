using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowTrash
{
	class Program
	{
		static void Main(string[] args)
		{
			//InitiateScreenWrite();

			int[,] screen = new int[Console.WindowWidth, Console.WindowHeight];
			Vector3 cameraPos = new Vector3(0, 0, 10);
			Vector3 planePos = new Vector3(0,0,-9);
			Vector3 cubePos = new Vector3(0,0,-3);
			
			Vector3[] vertices = new Vector3[8] { new Vector3(-1, -1, -1), new Vector3(1, -1, -1), new Vector3(-1, 1, -1), new Vector3(-1, -1, 1), new Vector3(1, -1, 1), new Vector3(1, 1, -1), new Vector3(-1, 1, 1), new Vector3(1, 1, 1) };
			int[,] edges = new int[12, 2] { { 0, 3 }, { 3, 6 }, { 6, 2 }, { 2, 0 }, { 4, 1 }, { 1, 5 }, { 5, 7 }, { 7, 4 }, { 1, 0 }, { 4, 3 }, { 7, 6 }, { 5, 2 } };

			for (int i = 0; i < vertices.Length; i++)
			{
				float dX = (vertices[i].x + cubePos.x) - cameraPos.x;
				float dY = (vertices[i].y + cubePos.y) - cameraPos.y;
				float dZ = (vertices[i].z + cubePos.z) - cameraPos.z;
				float distXZ = (float)Math.Sqrt((double)(dX * dX + dZ * dZ));
				float distYZ = (float)Math.Sqrt((double)(dY * dY + dZ * dZ));
				Vector2 screenPos = new Vector2(dX*(dZ/planePos.z), dY*(dZ/planePos.z));
				//Console.WriteLine($"Position on screen: {screenPos.x}, {screenPos.y}");
				Console.SetCursorPosition((int)distYZ, (int)distXZ);
				Console.Write("%");
				//angles are not needed, just need the ratio between the sides

			}


			Console.ReadLine();
		}
		
		static void InitiateScreenWrite()
		{
			Vector2 pos = new Vector2(0, 0);
			Vector2 point1 = new Vector2(0, 0);
			bool pDone = false;
			bool mL = true;
			while (true)
			{
				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.DownArrow:
						if (pos.y != Console.WindowHeight)
							pos.y++;
						break;
					case ConsoleKey.UpArrow:
						if (pos.y != 0)
							pos.y--;
						break;
					case ConsoleKey.LeftArrow:
						if (pos.x != 0)
							pos.x--;
						break;
					case ConsoleKey.RightArrow:
						if (pos.x != Console.WindowWidth - 1)
							pos.x++;
						break;
					case ConsoleKey.Enter:
						if (pDone)
						{
							MakeLine((int)point1.x, (int)point1.y, (int)pos.x, (int)pos.y);
							pDone = false;
						}
						else
						{
							point1.x = pos.x;
							point1.y = pos.y;
							Console.Write('@');
							pDone = true;
						}
						break;
					case ConsoleKey.Escape:
						Console.Clear();
						break;
				}
				Console.SetCursorPosition((int)pos.x, (int)pos.y);
			}
		}
		static void MakeLine (int x0, int y0, int x1, int y1)
		{

			//my version of Bresenham's line algorithm was bugged so I yeeted this from rosettacode.com
			//get yoinked lol

			int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
			int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
			int err = (dx > dy ? dx : -dy) / 2, e2;
			for (; ; )
			{
				Console.SetCursorPosition(x0, y0);
				Console.Write('#');
				if (x0 == x1 && y0 == y1) break;
				e2 = err;
				if (e2 > -dx) { err -= dy; x0 += sx; }
				if (e2 < dy) { err += dx; y0 += sy; }
			}
		}
	}
	struct Vector3
	{
		public float x;
		public float y;
		public float z;
		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
	}
	struct Vector2
	{
		public float x;
		public float y;
		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
	}
}
