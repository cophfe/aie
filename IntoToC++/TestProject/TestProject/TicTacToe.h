#pragma once
#include <iostream>
#include <Windows.h>

class TicTacToe
{
public:
	TicTacToe(HANDLE outputHandle, COORD topLeft);
	void DrawGrid();
	void DrawLetter(COORD startPos, char characterChar);
	void HoverGridCoord(COORD coord);
	bool ConfirmSelect();
	COORD GetGridCoord(COORD cursorPos);
	void SwitchPlayer();
	bool CheckIfCorrect();
	char GetPlayerCharacter();
private:
	void SetHoverPrint();
	void SetPlayer1Print();
	void SetPlayer2Print();
	void UpdatePrint();
	void SetCurrentPlayerPrint();
	bool player1 = true;
	char colorSetString[8] = "\033[1;90m";
	const char hoverColorModifier = '0';
	const char player1ColorModifier = '1';
	const char player2ColorModifier = '4';
	char grid[3][3] = { {'n', 'n', 'n'}, {'n', 'n', 'n'}, {'n', 'n', 'n'} };
	HANDLE outputHandle;
	COORD topLeft;
	COORD selected = { 0, 0 };
	const char* x = "db    db\0`8b  d8'\0 `8bd8' \0 .dPYb. \0.8P  Y8.\0YP    YP";
	const char* o = " .d88b. \0.8P  Y8.\088    88\088    88\0`8b  d8'\0 `Y88P' ";
	const char* none = "        \0        \0        \0        \0        \0        ";
	
};
