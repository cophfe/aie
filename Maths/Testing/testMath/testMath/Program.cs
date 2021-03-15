using System;
using Mlib;

namespace testMath
{
	class Program
	{
		static float angle = (float)(Math.PI / 32);
		
		static void Main(string[] args)
		{

			

			Matrix3 m3c = new Matrix3(0, -3, -2, 1, -4, -2, -3, 4, 1);
			m3c.SetRotateZ(4);
			Matrix3 m3d = m3c.Inverse();



			Console.WriteLine("Matrix 3 Multiplication Result:\n");
			for (int i = 0; i < m3c.m.Length; i++)
			{
				Console.Write($"{m3c.m[i]},\t");
				if (i == 2 || i == 5 || i == 8)
					Console.Write('\n');
			}
			Console.Write('\n');
			for (int i = 0; i < m3d.m.Length; i++)
			{
				Console.Write($"{m3d.m[i]},\t");
				if (i == 2 || i == 5 || i == 8)
					Console.Write('\n');
			}
			Console.Write('\n');

			m3d = m3d * m3c;
			for (int i = 0; i < m3d.m.Length; i++)
			{
				Console.Write($"{m3d.m[i]},\t");
				if (i == 2 || i == 5 || i == 8)
					Console.Write('\n');
			}
			Console.ReadKey();
		}
	}
}
