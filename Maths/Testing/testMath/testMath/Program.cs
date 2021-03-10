using System;
using Mlib;

namespace testMath
{
	class Program
	{
		static float angle = (float)(Math.PI / 32);
		
		static void Main(string[] args)
		{
			//Matrix3 m3a = Matrix3.Identity;
			//m3a.SetRotateX(3.98f);

			//Matrix3 m3c = Matrix3.Identity;
			////m3c.SetRotateZ(9.62f);

			//Matrix3 m3d = m3a * m3c;

			//Console.WriteLine("Matrix 3 Multiplication Result:");
			//Console.WriteLine($"{m3d.m00}, {m3d.m01}, {m3d.m02}");
			//Console.WriteLine($"{m3d.m10}, {m3d.m11}, {m3d.m12}");
			//Console.WriteLine($"{m3d.m20}, {m3d.m21}, {m3d.m22}\n");


			//Matrix4 m4a = new Matrix4(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
			Matrix4 m4a = new Matrix4();
			m4a.SetRotateX(4.5f);

			//Matrix4 m4c = new Matrix4(10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160);
			Matrix4 m4c = Matrix4.Identity;

			Matrix4 m4d = m4a * m4c;

			Console.WriteLine("Matrix 4 Multiplication Result:");
			Console.WriteLine($"{m4d.m00}, {m4d.m01}, {m4d.m02}, {m4d.m03}");
			Console.WriteLine($"{m4d.m10}, {m4d.m11}, {m4d.m12}, {m4d.m13}");
			Console.WriteLine($"{m4d.m20}, {m4d.m21}, {m4d.m22}, {m4d.m23}");
			Console.WriteLine($"{m4d.m30}, {m4d.m31}, {m4d.m32}, {m4d.m33}");

			Console.ReadKey();
		}
	}
}
