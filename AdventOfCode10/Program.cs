string[] input = File.ReadAllLines("input.txt");

char[] pipes = {'|', '-', 'L', 'J', '7', 'F'};

char[] pipesNorth = { '|', 'F', '7', 'S'};

char[,] grid = new char[input.Length, input[0].Length];

(int Row, int Column) startPosition = (0, 0);

// Transform string[] to char[,]
for (int i = 0; i < input.Length; i++)
{
	string currentString = input[i];
	for (int j = 0; j < input[i].Length; j++)
	{
		grid[i, j] = currentString[j];
		if (grid[i, j] == 'S')
			startPosition = (i, j);
	}
}

Console.WindowWidth = grid.GetLength(0) + 1;
//Console.WindowHeight = grid.GetLength(1) + 1;

List<(int Row, int Column)> starterPipes = GetFirstPipesPositions(startPosition.Row, startPosition.Column);
bool backToStart = false;
(int Row, int Column) previousPosition = startPosition, currentPosition = starterPipes[0], nextPosition = (0,0);
int steps = 1;

List<(int Row, int Column)> pipeLoop = [];
pipeLoop.Add(startPosition);

Console.ForegroundColor = ConsoleColor.White;
DisplayGrid();

Console.ForegroundColor = ConsoleColor.Red;
while (!backToStart)
{
	steps++;
	pipeLoop.Add(currentPosition);
	Console.SetCursorPosition(currentPosition.Column, currentPosition.Row);
	Console.Write(grid[currentPosition.Row, currentPosition.Column]);
    nextPosition = GetNextDirection(previousPosition.Row, previousPosition.Column, currentPosition.Row, currentPosition.Column);
	previousPosition = currentPosition;
	currentPosition = nextPosition;
	if (currentPosition == startPosition)
		backToStart = true;
}

int tilesInsideLoop = 0;

Console.ForegroundColor = ConsoleColor.Blue;

for (int i = 0; i < grid.GetLength(0); i++) // rows
{
    bool counting = false;
    for (int j = 0; j < grid.GetLength(1); j++) // columns
	{
		if (pipeLoop.Contains((i, j)) && pipesNorth.Contains(grid[i,j]))
		{
            counting = !counting;
			continue;
        }
		if (counting && !pipeLoop.Contains((i,j)))
		{
            Console.SetCursorPosition(j, i);
            Console.Write(grid[i, j]);
			Thread.Sleep(100);
            tilesInsideLoop++;
		}
	}
}

Console.ForegroundColor = ConsoleColor.Green;
Console.SetCursorPosition(0, grid.GetLength(0));
Console.WriteLine($"Number of steps to go back to beginning : {steps}");
Console.WriteLine($"Furthest distance from start : {steps / 2}");
Console.WriteLine($"Tiles inside the loop : {tilesInsideLoop}");

(int Row, int Column) GetNextDirection(int previousRow, int previousColumn, int currentRow, int currentColumn)
{
	char tile = grid[currentRow, currentColumn];
	switch (tile)
	{
		case '|':
			if (previousRow < currentRow)
				return (currentRow + 1, currentColumn);
			return (currentRow - 1, currentColumn);
		case '-':
			if (previousColumn < currentColumn)
				return (currentRow, currentColumn + 1);
			return (currentRow, currentColumn - 1);
		case 'L':
			if (previousRow < currentRow)
				return (currentRow, currentColumn + 1);
			return (currentRow - 1, currentColumn);
		case 'J':
			if (previousRow < currentRow)
				return (currentRow, currentColumn - 1);
			return (currentRow - 1, currentColumn);
		case '7':
			if (previousRow > currentRow)
				return (currentRow, currentColumn - 1);
			return (currentRow + 1, currentColumn);
		case 'F':
			if (previousRow > currentRow)
				return (currentRow, currentColumn + 1);
			return (currentRow + 1, currentColumn);
	}

	return (0, 0);
}

List<(int Row, int Column)> GetFirstPipesPositions(int row, int column)
{
	List<(int Row, int Column)> firstPipes = [];
	if (row > 0 && column > 0)
	{
		if (pipes.Contains(grid[row - 1, column])){
			if (grid[row - 1, column] == '|' || grid[row - 1, column] == 'F' || grid[row - 1, column] == '7')
			{
				firstPipes.Add((row - 1, column));
			}
		}
		if (pipes.Contains(grid[row + 1, column]))
		{
			if (grid[row + 1, column] == '|' || grid[row + 1, column] == 'J' || grid[row + 1, column] == 'L')
			{
				firstPipes.Add((row + 1, column));
			}
		}
		if (pipes.Contains(grid[row, column - 1]))
			if (grid[row, column - 1] == '-' || grid[row, column - 1] == 'F' || grid[row, column - 1] == 'L')
			{
				firstPipes.Add((row, column - 1));
			}
		if (pipes.Contains(grid[row, column + 1]))
		{
			if (grid[row, column + 1] == '-' || grid[row, column + 1] == 'J' || grid[row, column + 1] == '7')
			{
				firstPipes.Add((row, column + 1));
			}
		}
	}
	return firstPipes;
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