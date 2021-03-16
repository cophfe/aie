using System;
using Mlib;

namespace testMath
{
	class Program
	{
		static float angle = (float)(Math.PI / 32);
		
		static void Main(string[] args)
		{



			Matrix4 m4c = new Matrix4(1, 1, 1, -1, 1, 1, -1, 1, 1, -1, 1, 1, -1, 1, 1, 1);
			//m4c.SetRotateZ(4);
			//Matrix4 m4d = m4c.Inverse();

			

			Console.WriteLine("Matrix 3 Multiplication Result:\n");
			for (int i = 0; i < m4c.m.Length; i++)
			{
				Console.Write($"{m4c.m[i]},\t");
				if (i == 3 || i == 7 || i == 11 || i == 15)
					Console.Write('\n');
			}

					Console.Write('\n');
			Console.Write('\n');
			for (int i = 0; i < m4d.m.Length; i++)
			{
				Console.Write($"{m4d.m[i]},\t");
				if (i == 3 || i == 7 || i == 11 || i == 15)
					Console.Write('\n');
			}
			Console.Write('\n');

			m4d = m4d * m4c;
			for (int i = 0; i < m4d.m.Length; i++)
			{
				Console.Write($"{m4d.m[i]},\t");
				if (i == 3 || i == 7 || i == 11 || i == 15)
					Console.Write('\n');
			}
			Console.ReadKey();
		}
	}
}
