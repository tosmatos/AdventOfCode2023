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

int totalEnergizedTiles = CountEnergizedTiles();

DisplayGrid(energizedGrid);

Console.WriteLine($"Total energized tiles (Part 1): {totalEnergizedTiles}");

ResetEnergizedGrid();

// Part 2

List<int> energizedTiles = [];

// Search top row beaming down
for (int i = 0; i < grid.GetLength(1) - 1; i++)
{
    TravelInGrid(Direction.Down, 0, i);
    energizedTiles.Add(CountEnergizedTiles());
    ResetEnergizedGrid();
}

// Search bottom row beaming up
for (int i = grid.GetLength(1) - 1; i >= 0; i--)
{
	TravelInGrid(Direction.Up, 0, i);
	energizedTiles.Add(CountEnergizedTiles());
	ResetEnergizedGrid();
}

// Search left column beaming right
for (int i = 0; i < grid.GetLength(0) - 1; i++)
{
	TravelInGrid(Direction.Right, i, 0);
	energizedTiles.Add(CountEnergizedTiles());
	ResetEnergizedGrid();
}

// Search right column beaming left
for (int i = grid.GetLength(0) - 1; i >= 0; i--)
{
	TravelInGrid(Direction.Left, 0, i);
	energizedTiles.Add(CountEnergizedTiles());
	ResetEnergizedGrid();
}

Console.WriteLine($"Best total energized tiles (part 2) : {energizedTiles.Max()}");

void ResetEnergizedGrid()
{
	for (int i = 0; i < input.Length; i++)
	{
		for (int j = 0; j < input[i].Length; j++)
		{
			energizedGrid[i, j] = '.';
		}
	}
    visitedPositions = []; // Reset this aswell else every subsequent search if flawed
}

int CountEnergizedTiles()
{
    int energizedTiles = 0;
	for (int i = 0; i < input.Length; i++)
	{
		for (int j = 0; j < input[i].Length; j++)
		{
			if (energizedGrid[i, j] == '#')
				energizedTiles++;
		}
	}
    return energizedTiles;
}

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