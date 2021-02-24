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
			//Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
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
			///
			//InputRecorder iR = new InputRecorder();
			//InputRecorder.ShowCursor(false);
			//while (true)
			{
				//iR.RestrictMouseToWindow();
				//iR.MouseInput();
				ConsoleMouse.Read();
			}
			InputRecorder.ShowCursor(true);
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
