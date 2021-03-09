using System;
using MathLibrary;

namespace testMath
{
	class Program
	{
		static float angle = (float)(Math.PI / 32);
		static void Main(string[] args)
		{
			Random rand = new Random();
			Vector2 v = new Vector2(0, 10);
			float rotation = (float)Math.PI * 2;
			while (true)
			{
				Console.SetCursorPosition(2*(int)Math.Round(v.x, 0, MidpointRounding.AwayFromZero) + 45, (int)Math.Round(v.y, 0, MidpointRounding.AwayFromZero) + 14);
				Console.Write(new string((char)rand.Next(30,90),2));
				v.Rotate(angle);
				//v = Vector2.Rotate(v, angle);
				angle %= rotation;

				
				Console.SetCursorPosition(0, 0);
				Console.Write(v.Magnitude());

				if (rand.NextDouble() > 0.2)
				System.Threading.Thread.Sleep(1);

			}
			

			
		}
	}
}
