using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ReadingWriting
{
	
	class Cursor
	{
		enum CursorType
		{
			Circle,
			Square
		}

		Vector2 pos = new Vector2(0, 0);
		const int moveAmount = 1, sizeAmount = 1;
		int size = 5;
		CursorType cT = CursorType.Circle;
		ConsoleColor colour = ConsoleColor.White;
		

		public void MoveCursor(ConsoleKey key, out bool keepGoing)
		{
			switch (key)
			{
				case ConsoleKey.UpArrow:
					pos.Y += moveAmount;
					break;
				case ConsoleKey.DownArrow:
					pos.Y -= moveAmount;
					break;
				case ConsoleKey.RightArrow:
					pos.X += moveAmount;
					break;
				case ConsoleKey.LeftArrow:
					pos.X -= moveAmount;
					break;
				case ConsoleKey.OemPlus:
					size += sizeAmount;
					break;
				case ConsoleKey.OemMinus:
					if (size > sizeAmount)
						size -= sizeAmount;
					break;
				case ConsoleKey.Oem4: //[
					if (colour == ConsoleColor.Black)
					{
						colour = ConsoleColor.White;
						break;
					}
					colour--;
					break;
				case ConsoleKey.Oem6: //]
					if (colour == ConsoleColor.White)
					{
						colour = ConsoleColor.Black;
						break;
					}
					colour++;
					break;
				case ConsoleKey.Escape:
					keepGoing = false;
					return;
			}
			keepGoing = true;
		}

		public void WriteCursor()
		{
			switch (cT)
			{
				case CursorType.Circle:

					break;

				case CursorType.Square:

					break;
			}
		}
	}
}
