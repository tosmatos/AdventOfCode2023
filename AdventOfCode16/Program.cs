Console.SetWindowSize(200, 200);

string[] input = File.ReadAllLines("input.txt");

char[,] grid = new char[input.Length, input[0].Length];
char[,] energizedGrid = new char[input.Length, input[0].Length];

// Transform string[] to char[,] and fill energyGrid with .
for (int i = 0; i < input.Length; i++)
{
    string currentString = input[i];
    for (int j = 0; j < input[i].Length; j++)
    {
        grid[i, j] = currentString[j];
        energizedGrid[i, j] = '.';
    }
}
HashSet<(int Row, int Column, Direction direction)> visitedPositions = new();

TravelInGrid(Direction.Right, 0, 0);

int totalEnergizedTiles = 0;

for (int i = 0; i < input.Length; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        if (energizedGrid[i, j] == '#')
            totalEnergizedTiles++;
    }
}

DisplayGrid(energizedGrid);

Console.WriteLine($"Total energized tiles : {totalEnergizedTiles}");

void TravelInGrid(Direction direction, int row, int column)
{
    //DisplayGrid(energizedGrid);

	var currentState = (row, column, direction);

	if (!visitedPositions.Add(currentState)) //already visited with same direction
        return;

	energizedGrid[row, column] = '#';

    List<Direction> nextDirection = ComputeBeamDirection(direction, grid[row, column]);

    foreach(Direction newDirection in  nextDirection)
    {
        int newRow = 0, newColumn = 0;
        switch(newDirection)
        {
            case Direction.Left:
                newRow = row;
                newColumn = column - 1;
                break;
            case Direction.Right:
                newRow = row;
                newColumn = column + 1;
                break;
            case Direction.Up:
                newRow = row - 1;
                newColumn = column;
                break;
            case Direction.Down:
                newRow = row + 1;
                newColumn = column;
                break;
        }
        if (newRow >= 0 && newColumn >= 0 && newRow < grid.GetLength(0) && newColumn < grid.GetLength(1)) // If next position is in grid
            TravelInGrid(newDirection, newRow, newColumn);
    }
}

List<Direction> ComputeBeamDirection(Direction direction, char tile)
{
    List<Direction> newDirections = [];
    switch(direction)
    {
        case Direction.Left:
            if (tile == '-')
                newDirections.Add(Direction.Left);
            else if (tile == '|')
            {
                newDirections.Add(Direction.Up);
                newDirections.Add(Direction.Down);
            }
            else if (tile == '/')
                newDirections.Add(Direction.Down);
            else if (tile == '\\')
                newDirections.Add(Direction.Up);
            else if (tile == '.')
                newDirections.Add(direction);
            break;
        case Direction.Right:
            if (tile == '-')
                newDirections.Add(Direction.Right);
            else if (tile == '|')
            {
                newDirections.Add(Direction.Up);
                newDirections.Add(Direction.Down);
            }
            else if (tile == '/')
                newDirections.Add(Direction.Up);
            else if (tile == '\\')
                newDirections.Add(Direction.Down);
			else if (tile == '.')
				newDirections.Add(direction);
			break;
        case Direction.Up:
            if (tile == '-')
            {
                newDirections.Add(Direction.Left);
                newDirections.Add(Direction.Right);
            }
            else if (tile == '|')
                newDirections.Add(Direction.Up);
            else if (tile == '/')
                newDirections.Add(Direction.Right);
            else if (tile == '\\')
                newDirections.Add(Direction.Left);
			else if (tile == '.')
				newDirections.Add(direction);
			break;
        case Direction.Down:
            if (tile == '-')
            {
                newDirections.Add(Direction.Left);
                newDirections.Add(Direction.Right);
            }
            else if (tile == '|')
                newDirections.Add(Direction.Down);
            else if (tile == '/')
                newDirections.Add(Direction.Left);
            else if (tile == '\\')
                newDirections.Add(Direction.Right);
			else if (tile == '.')
				newDirections.Add(direction);
			break;
    }

    return newDirections;
}

void DisplayGrid(char[,] grid)
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

enum Direction
{
    Left,
    Down,
    Right,
    Up,
}