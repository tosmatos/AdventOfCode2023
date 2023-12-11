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

//char[,] newGrid;
//// Insert rows
//foreach (int rowIndex in rowIndexes)
//{
//    newGrid = InsertRow(grid, rowIndex);
//    grid = new char[newGrid.GetLength(0), newGrid.GetLength(1)];
//    Array.Copy(newGrid, grid, newGrid.Length);
//}


//foreach (int columnIndex in columnIndexes)
//{
//    newGrid = InsertColumn(grid, columnIndex);
//    grid = new char[newGrid.GetLength(0), newGrid.GetLength(1)];
//    Array.Copy(newGrid, grid, newGrid.Length);
//}

List<(long Row, long Column)> galaxiesPositions = [];

for (int i = 0; i < grid.GetLength(0); i++)
{
    for (int j = 0; j < grid.GetLength(1); j++)
    {
        if (grid[i, j] == '#')
            galaxiesPositions.Add((i, j));
    }
}

List<((long Row, long Column), (long Row, long Column))> donePairs = [];
long distanceSum = 0;
long distanceSumPart2 = 0;

for (int i = 0; i < galaxiesPositions.Count; i++)
{
    for (int j = 0; j < galaxiesPositions.Count; j++)
    {
        (long Row, long Column) galaxy1 = galaxiesPositions[i], galaxy2 = galaxiesPositions[j];

        if (galaxy1 == galaxy2) continue;

        if (donePairs.Contains((galaxy1, galaxy2)) || donePairs.Contains((galaxy2, galaxy1)))
            continue;

        long millionGalaxy1Rows = rowIndexes.Where(index => index < galaxy1.Row).Count();
        long millionGalaxy1Columns = columnIndexes.Where(index => index < galaxy1.Column).Count();
        long millionGalaxy2Rows = rowIndexes.Where(index => index < galaxy2.Row).Count();
        long millionGalaxy2Columns = columnIndexes.Where(index => index < galaxy2.Column).Count();

        (long Row, long Column) oldGalaxy1 = (millionGalaxy1Rows * 999999 + galaxy1.Row, millionGalaxy1Columns * 999999 + galaxy1.Column);
        (long Row, long Column) oldGalaxy2 = (millionGalaxy2Rows * 999999 + galaxy2.Row, millionGalaxy2Columns * 999999 + galaxy2.Column);

        donePairs.Add((galaxy1, galaxy2));

        long distance = Math.Max(galaxy1.Row, galaxy2.Row) - Math.Min(galaxy1.Row, galaxy2.Row)
            + Math.Max(galaxy1.Column, galaxy2.Column) - Math.Min(galaxy1.Column, galaxy2.Column);

        long oldDistance = Math.Max(oldGalaxy1.Row, oldGalaxy2.Row) - Math.Min(oldGalaxy1.Row, oldGalaxy2.Row)
            + Math.Max(oldGalaxy1.Column, oldGalaxy2.Column) - Math.Min(oldGalaxy1.Column, oldGalaxy2.Column);

        distanceSum += distance;
        distanceSumPart2 += oldDistance;
    }
}

Console.WriteLine($"Total sum of unique pairs : {distanceSum}");
Console.WriteLine($"Total sum of unique pairs (accounting for million years): {distanceSumPart2}");

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