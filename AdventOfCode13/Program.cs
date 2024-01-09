string[] input = File.ReadAllLines("input.txt");

List<List<string>> grids = [];
List<string> grid = [];
int total = 0;

foreach (string line in input)
{
	if (string.IsNullOrEmpty(line))
	{
		grids.Add(grid);
		grid = [];
	} else
	{
		grid.Add(line.Trim());
	}
}
grids.Add(grid); // Add last grid not taken since last line is part of grid

foreach (List<string> field in grids)
{
	int linesAbove = ComputeMirror(field);

	if (linesAbove > 0)
	{
		total += linesAbove * 100;
		continue;
	}
	PrintField(field);
	Console.WriteLine($"Lines above : {linesAbove}");
	Console.WriteLine();

	List<string> verticalField = [];

	for (int i = 0; i < field[0].Length; i++) // make vertical field
	{
		string column = "";
		for (int j = field.Count - 1; j >= 0; j--)
		{
			column += field[j][i];
		}
		verticalField.Add(column);
	}

	linesAbove = ComputeMirror(verticalField);

	PrintField(verticalField);
	Console.WriteLine($"Lines above : {linesAbove}");

	if (linesAbove > 0)
	{
		total += linesAbove;
	}

	Console.WriteLine("************************");
}

Console.WriteLine(total);

int ComputeMirror(List<string> field)
{
	bool maybeMirror = false;
	int inverseI = 0;
	int linesAbove = 0;

	for (int i = 0; i < field.Count; i++)
	{
		string row = field[i];
		if (!maybeMirror)
		{
			if (i != 0) // don't compare if only one line
			{
				if (row == field[i - 1]) // rows mirrored
				{
					maybeMirror = true;
					inverseI = i - 1;
					linesAbove = i;
				}
			}
		}
		else // maybe mirror found
		{
			if (inverseI < 0)
				break;
			if (row != field[inverseI])
			{
				maybeMirror = false;
			}
		}
		inverseI--;
	}
	if (maybeMirror)
	{
		return linesAbove;
	}
	else return 0;
}

void PrintField(List<string> field)
{
	for (int i = 0; i < field.Count; i++)
	{
		string line = field[i];
		Console.WriteLine(i + "|" + line);
	}
}