#include <iostream>
#include <cmath>
#include <Windows.h>
#include "TicTacToe.h"

TicTacToe::TicTacToe(HANDLE outputHandle, COORD topLeft)
{
	this->outputHandle = outputHandle;
	this->topLeft = topLeft;
}

void TicTacToe::DrawGrid()
{
	//Attribute rules found here: https://misc.flogisoft.com/bash/tip_colors_and_formatting
	//not 100% accurate for this console but close to it

	//x and o dimensions: 8x6 
	//line is 1 thick, with spacing of 10 spaces before character on x and 4 on y (this makes it square)
	//so total size is (8 * 3 + 10 * 6 + 1 * 4)x(6 * 3 + 4 * 6 + 1 * 4)
	//88x46

	char line[89];
	for (int i = 0; i < 88; i++)
	{
		line[i] = ' ';
	}
	line[88] = 0;

	//grid line points are:
	//horizontal: 0, 15, 30, 46 
	//vertical: 0, 21, 42, 64
	std::cout << "\033[47m";
	COORD c = { 0,0 };
	for (int i = 1; i < 46; i++)
	{
		c.Y = i + topLeft.Y;
		c.X = topLeft.X;
		SetConsoleCursorPosition(outputHandle, c);
		std::cout << ' ';
		c.X = topLeft.X + 29;
		SetConsoleCursorPosition(outputHandle, c);
		std::cout << ' ';
		c.X = topLeft.X + 58;
		SetConsoleCursorPosition(outputHandle, c);
		std::cout << ' ';
		c.X = topLeft.X + 87;
		SetConsoleCursorPosition(outputHandle, c);
		std::cout << ' ';
	}
	c.X = topLeft.X;
	c.Y = topLeft.Y;
	SetConsoleCursorPosition(outputHandle, c);
	std::cout << line;
	c.Y = topLeft.Y + 15;
	SetConsoleCursorPosition(outputHandle, c);
	std::cout << line;
	c.Y = topLeft.Y + 30;
	SetConsoleCursorPosition(outputHandle, c);
	std::cout << line;
	c.Y = topLeft.Y + 45;
	SetConsoleCursorPosition(outputHandle, c);
	std::cout << line;

	std::cout << "\033[0m";
}

void TicTacToe::DrawLetter(COORD position, char characterChar = 'n')
{
	const char* string = NULL;

	switch (characterChar)
	{
	case 'x':
		string = x;
		break;
	case 'o':
		string = o;
		break;
	default:
		string = none;
		break;
	}
	position.X = position.X * 29 + 11 + topLeft.X;
	position.Y = position.Y * 15 + 5 + topLeft.Y;
	for (int i = 0; i < 6; i++)
	{
		SetConsoleCursorPosition(outputHandle, position);
		std::cout << &(string[i * 9]);
		position.Y++;
	}
}

void TicTacToe::HoverGridCoord(COORD coord)
{
	if (!(coord.X == selected.X && coord.Y == selected.Y) && grid[coord.X][coord.Y] == 'n') {
		UpdatePrint();
		DrawLetter(selected, grid[selected.X][selected.Y]);
		selected = coord;
		SetHoverPrint();
		std::cout << "\033[1;90m";

		DrawLetter(selected, GetPlayerCharacter());
	}
}

bool TicTacToe::ConfirmSelect() 
{
	if (grid[selected.X][selected.Y] == 'n') 
	{
		SetCurrentPlayerPrint();
		UpdatePrint();
		char c = GetPlayerCharacter();
		grid[selected.X][selected.Y] = c;
		DrawLetter(selected, c);
		SwitchPlayer();
		return true;
	}
	return false;
}

COORD TicTacToe::GetGridCoord(COORD cursorPos)
{
	cursorPos.X = (cursorPos.X - topLeft.X) / 29;
	if (cursorPos.X > 2) 
	{
		cursorPos.X = 2;
	}
	else if (cursorPos.X < 0) 
	{
		cursorPos.X = 0;
	}
	cursorPos.Y = (cursorPos.Y - topLeft.Y) / 15;
	if (cursorPos.Y > 2)
	{
		cursorPos.Y = 2;
	}
	else if (cursorPos.Y < 0)
	{
		cursorPos.Y = 0;
	}
	return cursorPos;
}

void TicTacToe::SetHoverPrint() 
{
	colorSetString[5] = hoverColorModifier;
}

void TicTacToe::SetPlayer1Print()
{
	colorSetString[5] = player1ColorModifier;
}

void TicTacToe::SetPlayer2Print()
{
	colorSetString[5] = player2ColorModifier;
}

void TicTacToe::SetCurrentPlayerPrint() 
{
	if (player1) {
		SetPlayer1Print();
	}
	else {
		SetPlayer2Print();
	}
}

void TicTacToe::UpdatePrint() 
{
	std::cout << colorSetString;
}

void TicTacToe::SwitchPlayer() {
	player1 = !player1;
}

char TicTacToe::GetPlayerCharacter() {
	return player1 ? 'x' : 'o';
}

bool TicTacToe::CheckIfCorrect() 
{
	char positionChar;
	char positionX;
	char positionY;
	for (char i = 0; i < 3; i++)
	{
		positionX = i;
		for (char j = 0; j < 3; j++)
		{
			positionChar = grid[positionX][i];
			if (positionChar == 'n')
				continue;
			positionY = i;
			//first check horizontal
			char indexX = 0;
			char indexY = positionY;
			char correctAmountInARow = 0;

			while (grid[indexX][indexY] == positionChar)
			{
				correctAmountInARow++;
				indexX++;
				if (correctAmountInARow == 3) 
				{
					return true;
				}
			}
			//then check verticle
			correctAmountInARow = 0;
			indexX = positionX;
			indexY = 0;
			
			while (grid[indexX][indexY] == positionChar)
			{
				correctAmountInARow++;
				indexY++;
				if (correctAmountInARow == 3)
				{
					return true;
				}
			}
			//then check diagonal
			//if the sum of the coords is odd, then it does not have a diagonal
			if (positionX + positionY == 3 || (positionX + positionY == 5))
				return false;
			if (grid[1][1] == positionChar)
			{
				if (grid[0][0] == positionChar && grid[2][2] == positionChar)
					return true;
				if (grid[2][0] == positionChar && grid[0][2] == positionChar)
					return true;
			}
			else
				return false;
		}
	}
	return false;
}
