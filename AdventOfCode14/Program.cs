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

Console.WriteLine("Total load : " + totalLoad);

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