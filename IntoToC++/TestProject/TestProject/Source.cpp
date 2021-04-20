#include <iostream>
#include <Windows.h>
#include "TicTacToe.h"

int main() 
{
	//reset color
	std::cout << "\033[0m";

	//0 is empty
	int board[3][3] = {0,0,0};

	const std::string x[6] = {"db    db", "`8b  d8'", " `8bd8' ", " .dPYb. ", ".8P  Y8.", "YP    YP" };

	const std::string y[6] = {" .d88b. ", ".8P  Y8.", "88    88", "88    88", "`8b  d8'", " `Y88P' "};
	const std::string none[6] = { "        ","        ","        ","        ","        ","        " };

	HANDLE inputHandle = GetStdHandle(-10);
	HANDLE outputHandle = GetStdHandle(-11);
	INPUT_RECORD buffer[128];
	DWORD numEvents = 0;

	//disable quick edit mode and enable mouse input
	SetConsoleMode(inputHandle, 0x0080);
	SetConsoleMode(inputHandle, 0x0008 | 0x0010);
	
	//Change Console Title
	SetConsoleTitle(TEXT("Gamer Game"));

	//Disable Console Cursor
	CONSOLE_CURSOR_INFO cursorInfo;
	GetConsoleCursorInfo(outputHandle, &cursorInfo);
	cursorInfo.bVisible = false;
	SetConsoleCursorInfo(outputHandle, &cursorInfo);
	
	TicTacToe grid(outputHandle, { 5,2 });
	//Draw Grid
	grid.DrawGrid();

	std::cout << "\033[1;91m";
	std::cout << "\033[0m";

	while (true) 
	{
		ReadConsoleInput(inputHandle, buffer, 128, &numEvents);
		COORD currentPos;
		COORD lastPos = { 0, 0 };

		for (unsigned int i = 0; i < numEvents; i++)
		{
			if (buffer[i].EventType == MOUSE_EVENT)
			{
				currentPos = buffer[i].Event.MouseEvent.dwMousePosition;
				if (!(currentPos.X == lastPos.X && currentPos.Y == lastPos.Y))
				{
					SetConsoleCursorPosition(outputHandle, currentPos);
					grid.HoverGridCoord(grid.GetGridCoord(currentPos));
					//std::cout << "\033[4;107;95mB";
				}
				if (buffer[i].Event.MouseEvent.dwButtonState == FROM_LEFT_1ST_BUTTON_PRESSED) 
				{
					if (grid.ConfirmSelect()) {
						if (grid.CheckIfCorrect())
							std::cout << "A PLAYER WINS!!!!";
					}
				}
			}
		}
	}
}

