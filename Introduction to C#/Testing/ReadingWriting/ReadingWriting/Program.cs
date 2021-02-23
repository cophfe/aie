using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ReadingWriting
{
	class Program
	{
		
		//delegate void d();
		static void Main(string[] args)
		{
			////d dd = new d();
			////dd += MoveCursor;
			//Cursor c = new Cursor();
			//bool drawing = true;
			//while (drawing)
			//{
			//	c.WriteCursor(); //writes to canvas array
			//	c.MoveCursor(Console.ReadKey(true).Key, out drawing);
			//}
			////either use "using" or ".Dispose()" to avoid memory leaks
			////writer.Dispose();
			ConsoleMouse.Read();
		}

		

		void Write()
		{
			using (StreamWriter writer = new StreamWriter("Files//test.txt"))
			{
				writer.WriteLine("yo");

				writer.Close();
			}
		}
	}
}
