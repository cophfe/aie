using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!\n");

            Matrix3 m3a = new Matrix3();
            m3a.SetRotateX(3.98f);

            Console.WriteLine("Matrix 3 Multiplication Result:");
            Console.WriteLine($"{m3a.m1}, {m3a.m2}, {m3a.m3}");
            Console.WriteLine($"{m3a.m4}, {m3a.m5}, {m3a.m6}");
            Console.WriteLine($"{m3a.m7}, {m3a.m8}, {m3a.m9}\n");

            Matrix3 m3c = new Matrix3();
            m3c.SetRotateZ(9.62f);

            Console.WriteLine("Matrix 3 Multiplication Result:");
            Console.WriteLine($"{m3c.m1}, {m3c.m2}, {m3c.m3}");
            Console.WriteLine($"{m3c.m4}, {m3c.m5}, {m3c.m6}");
            Console.WriteLine($"{m3c.m7}, {m3c.m8}, {m3c.m9}\n");

            Matrix3 m3d = m3c * m3a;

            Console.WriteLine("Matrix 3 Multiplication Result:");
            Console.WriteLine($"{m3d.m1}, {m3d.m2}, {m3d.m3}");
            Console.WriteLine($"{m3d.m4}, {m3d.m5}, {m3d.m6}");
            Console.WriteLine($"{m3d.m7}, {m3d.m8}, {m3d.m9}\n");
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
