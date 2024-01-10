Console.SetWindowSize(150, 150);

string[] input = File.ReadAllLines("input.txt");

char[,] grid = new char[input.Length, input[0].Length];

int totalLoad = 0;

// Transform string[] to char[,]
for (int i = 0; i < input.Length; i++)
{
	string currentString = input[i];
	for (int j = 0; j < input[i].Length; j++)
	{
		grid[i, j] = currentString[j];
	}
}

TiltGridNorth();
DisplayGrid();
ComputeLoad();

Console.WriteLine("Total load with only north tilt : " + totalLoad);
totalLoad = 0;

for (int i = 0; i < 1000000000; i++)
{
	TiltGridNorth();
	TiltGridWest();
	TiltGridSouth();
	TiltGridEast();
}

DisplayGrid();
ComputeLoad();

Console.WriteLine("Total load after 1000000000 cycles : " + totalLoad);

//Console.WriteLine(grid[grid.GetLength(0) - 1, grid.GetLength(1) - 1]);

void ComputeLoad()
{
	int weight = grid.GetLength(0);
	int oCount = 0;
	for (int row = 0; row < grid.GetLength(0); row++)
	{
		for (int column = 0; column < grid.GetLength(1); column++)
		{
			if (grid[row, column] == 'O')
				oCount++;
		}
		totalLoad += weight * oCount;
		weight--;
		oCount = 0;
	}
}

void TiltGridNorth()
{
	bool movedRocks = true;
	while (movedRocks)
	{
		movedRocks = false;
		for (int row = 0; row < grid.GetLength(0); row++)
		{
			int rowUnder = row + 1;
			for (int column = 0; column < grid.GetLength(1); column++)
			{
				if (grid[row, column] == '.')
				{
					if (rowUnder < grid.GetLength(1) && grid[rowUnder, column] == 'O')
					{
						grid[row, column] = 'O';
						grid[rowUnder, column] = '.';
						movedRocks = true;
					}
				}
			}
		}
	}
}

void TiltGridSouth()
{
	bool movedRocks = true;
	while (movedRocks)
	{
		movedRocks = false;
		for (int row = grid.GetLength(0) - 1; row >= 0; row--)
		{
			int rowAbove = row - 1;
			for (int column = 0; column < grid.GetLength(1); column++)
			{
				if (grid[row, column] == '.')
				{
					if (rowAbove >= 0 && grid[rowAbove, column] == 'O')
					{
						grid[row, column] = 'O';
						grid[rowAbove, column] = '.';
						movedRocks = true;
					}
				}
			}
		}
	}
}

void TiltGridEast()
{
	bool movedRocks = true;
	while (movedRocks)
	{
		movedRocks = false;
		for (int row = 0; row < grid.GetLength(0); row++)
		{
			for (int column = 0; column < grid.GetLength(1); column++)
			{
				int columnBefore = column - 1;
				if (grid[row, column] == '.')
				{
					if (columnBefore >= 0 && grid[row, columnBefore] == 'O')
					{
						grid[row, column] = 'O';
						grid[row, columnBefore] = '.';
						movedRocks = true;
					}
				}
			}
		}
	}
}

void TiltGridWest()
{
	bool movedRocks = true;
	while (movedRocks)
	{
		movedRocks = false;
		for (int row = 0; row < grid.GetLength(0); row++)
		{
			for (int column = grid.GetLength(1) - 1; column >= 0; column--)
			{
				int columnBefore = column + 1;
				if (grid[row, column] == '.')
				{
					if (columnBefore < grid.GetLength(1) && grid[row, columnBefore] == 'O')
					{
						grid[row, column] = 'O';
						grid[row, columnBefore] = '.';
						movedRocks = true;
					}
				}
			}
		}
	}
}

void DisplayGrid()
{
	for (int row = 0; row < grid.GetLength(0); row++)
	{
		for (int column = 0; column < grid.GetLength(1); column++)
		{
			Console.SetCursorPosition(column, row);
			Console.Write(grid[row, column]);
		}
		Console.WriteLine();
	}
}