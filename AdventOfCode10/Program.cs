string[] input = File.ReadAllLines("input.txt");

char[] pipes = new char[6] {'|', '-', 'L', 'J', '7', 'F'};

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

List<(int Row, int Column)> starterPipes = GetFirstPipesPositions(startPosition.Row, startPosition.Column);
bool backToStart = false;
(int Row, int Column) previousPosition = startPosition, currentPosition = starterPipes[0], nextPosition = (0,0);
int steps = 1;

while (!backToStart)
{
	steps++;
	nextPosition = GetNextDirection(previousPosition.Row, previousPosition.Column, currentPosition.Row, currentPosition.Column);
	previousPosition = currentPosition;
	currentPosition = nextPosition;
	if (currentPosition == startPosition)
		backToStart = true;
}

Console.WriteLine($"Number of steps to go back to beginning : {steps}");
Console.WriteLine($"Furthest distance from start : {steps / 2}");

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