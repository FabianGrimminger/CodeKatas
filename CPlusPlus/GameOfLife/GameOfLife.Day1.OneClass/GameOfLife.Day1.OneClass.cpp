#include "stdafx.h"
#include <iostream>
#include <string>
#include <random>
#include <algorithm>
#include <Windows.h>

#define width 10
#define height 10

class GameOfLife {

private:
	bool gamefield[width][height];
	std::mt19937 random;	
	bool glider = false;

	void CreateGameField() {
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				gamefield[i][j] = false;
			}
		}
		if (glider) {
			gamefield[0][1] = true;
			gamefield[1][2] = true;
			gamefield[2][0] = true;
			gamefield[2][1] = true;
			gamefield[2][2] = true;
			return;
		}

		int counter = 0;
		std::uniform_int_distribution<std::mt19937::result_type> dist10(0, 10);
		while (counter <= 20) {
			int x = dist10(random);
			int y = dist10(random);

			if (!gamefield[x][y]) {
				counter++;
				gamefield[x][y] = true;
			}
		}
	}

	void PrintGameField() {
		HANDLE hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
		int RED_DARKBLUE = 28;
		int WHITE_DARKBLUE = 31;
		
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				if (gamefield[i][j]) {
					SetConsoleTextAttribute(hConsole, RED_DARKBLUE);
					std::cout << " x";
				}
				else {
					SetConsoleTextAttribute(hConsole, WHITE_DARKBLUE);
					std::cout << " o";
				}
			}
			std::cout << std::endl;
		}
		std::cout << std::endl;
	}

	bool IsAlive(int neighbours,bool self) {
		if (self && neighbours == 2) {
			return true;
		}
		if (neighbours == 3) {
			return true;
		}
		return false;
	}

	int CountNeighbours(int x, int y) {
		int counter = 0;
		for (int i = -1; i <= 1; i++) {
			for (int j = -1; j <= 1; j++) {
				int tempX = (x + i) % width;
				int tempY = (y + j) % height;

				if (tempX < 0) {
					tempX = width + tempX;
				}
				if (tempY < 0) {
					tempY = height + tempY;
				}

				if (gamefield[tempX][tempY]) {
					counter++;
				}
			}
		}
		if (gamefield[x][y]) {
			counter--;
		}
		return counter;
	}

	void EnvolveLife() {
		bool tempField[width][height];

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				int n = CountNeighbours(i, j);
				tempField[i][j] = IsAlive(n, gamefield[i][j]);
			}
		}
		memcpy(gamefield, tempField, sizeof(gamefield));
	}

public:
	GameOfLife() {
		random.seed(std::random_device()());
	}
	GameOfLife(char c){
		glider = true;
	}
	GameOfLife(int seed) {
		random.seed(seed);
	}

	void Run() {
		CreateGameField();
		
		for (int i = 0; i < 10; i++) {
			PrintGameField();
			EnvolveLife();
		}
		std::cout << "----------Finish----------" << std::endl;
	}


};

//Console Window will close if not using Ctrl+F5 instead of F5
int main(int argc, char*argv[])
{
	if (argc == 1) {
		GameOfLife().Run();
		return 0;
	}

	std::string firstParam = argv[1];
	if (firstParam == "g") {
		GameOfLife('g').Run();
		return 0;
	}
	
	try {
		int seed = std::stoi(firstParam);
		GameOfLife(seed).Run();
		return 0;
	}
	catch (const std::invalid_argument& ia) {

	}

	std::cout << "Wrong params: use none, a number or g";

	return 0;
}

