string[] input = File.ReadAllLines("input.txt");

char[,] grid = new char[input.Length, input[0].Length];

// Transform string[] to char[,]
for (int i = 0; i < input.Length; i++)
{
	string currentString = input[i];
	for (int j = 0; j < input[i].Length; j++)
	{
		grid[i, j] = currentString[j];
	}
}

int rowCount = grid.GetLength(0);
int columnCount = grid.GetLength(1);

List<int> rowIndexes = [];

// Get rows to expand
for (int i = 0; i < rowCount; i++)
{
	if (!input[i].Contains('#'))
		rowIndexes.Add(i);
}

List<int> columnIndexes = [];

//Get columns to expand
for (int i = 0; i < columnCount; i++)
{
	List<char> column = [];
	for (int j = 0; j < rowCount; j++)
	{
		column.Add(grid[j, i]);
	}
	if (!column.Contains('#'))
		columnIndexes.Add(i);
}

rowIndexes.Reverse();
columnIndexes.Reverse();

char[,] newGrid;
// Insert rows
foreach (int rowIndex in rowIndexes)
{
	newGrid = InsertRow(grid, rowIndex);
	grid = new char[newGrid.GetLength(0), newGrid.GetLength(1)];
	Array.Copy(newGrid, grid, newGrid.Length);
}


foreach (int columnIndex in columnIndexes)
{
	newGrid = InsertColumn(grid, columnIndex);
	grid = new char[newGrid.GetLength(0), newGrid.GetLength(1)];
	Array.Copy(newGrid, grid, newGrid.Length);
}

List<(int Row, int Column)> galaxiesPositions = [];

for (int i = 0; i < grid.GetLength(0); i++)
{
	for (int j = 0; j < grid.GetLength(1); j++)
	{
		if (grid[i, j] == '#')
			galaxiesPositions.Add((i, j));
	}
}

List<((int Row, int Column), (int Row, int Column))> donePairs = [];
int distanceSum = 0;

for (int i = 0; i < galaxiesPositions.Count; i++)
{
	for (int j = 0; j < galaxiesPositions.Count; j++)
	{
		(int Row, int Column) galaxy1 = galaxiesPositions[i], galaxy2 = galaxiesPositions[j];

		if (galaxy1 == galaxy2) continue;

		if (donePairs.Contains((galaxy1, galaxy2)) || donePairs.Contains((galaxy2, galaxy1)))
			continue; 
		
		Console.WriteLine(galaxy1 + ", " + galaxy2);
		
		donePairs.Add((galaxy1, galaxy2));

		int distance = Math.Max(galaxy1.Row, galaxy2.Row) - Math.Min(galaxy1.Row, galaxy2.Row)
			+ Math.Max(galaxy1.Column, galaxy2.Column) - Math.Min(galaxy1.Column, galaxy2.Column);

		distanceSum += distance;
	}
}

Console.WriteLine($"Total sum of unioque pairs : {distanceSum}");

char[,] InsertRow(char[,] originalGrid, int rowIndex)
{
	int rows = originalGrid.GetLength(0);
	int cols = originalGrid.GetLength(1);

	char[,] newGrid = new char[rows + 1, cols];

	for (int i = 0; i < rowIndex; i++)
	{
		for (int j = 0; j < cols; j++)
		{
			newGrid[i, j] = originalGrid[i, j];
		}
	}

	for (int j = 0; j < cols; j++)
	{
		newGrid[rowIndex, j] = '.'; // You can set the default value as needed
	}

	for (int i = rowIndex + 1; i < rows + 1; i++)
	{
		for (int j = 0; j < cols; j++)
		{
			newGrid[i, j] = originalGrid[i - 1, j];
		}
	}

	return newGrid;
}

char[,] InsertColumn(char[,] originalGrid, int colIndex)
{
	int rows = originalGrid.GetLength(0);
	int cols = originalGrid.GetLength(1);

	char[,] newGrid = new char[rows, cols + 1];

	for (int i = 0; i < rows; i++)
	{
		for (int j = 0; j < colIndex; j++)
		{
			newGrid[i, j] = originalGrid[i, j];
		}

		newGrid[i, colIndex] = '.'; // You can set the default value as needed

		for (int j = colIndex + 1; j < cols + 1; j++)
		{
			newGrid[i, j] = originalGrid[i, j - 1];
		}
	}

	return newGrid;
}