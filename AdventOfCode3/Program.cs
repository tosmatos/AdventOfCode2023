string[] inputLines = File.ReadAllLines("input.txt");

List<Char[]> schematic =  new List<Char[]>();
List<numberPlusAdjacentStar> numbersWithAdjacentStars = new();

int totalSum = 0;
int part2TotalSum = 0;

foreach (string line in inputLines)
{
	schematic.Add(line.ToCharArray());
}

// Part 1
for (int i = 0; i < schematic.Count; i++)
{
	bool numberFound = false;
	char[] schematicLine = schematic[i];
	string lineNumber = "";
	List<char> adjacents = new();
	List<(int, int)> starPositions = new();

	Console.Write($"{Environment.NewLine}Line {i} : ");
	for (int j = 0; j < schematicLine.Length; j++)
	{
		if (char.IsDigit(schematicLine[j]))
		{
			lineNumber += schematicLine[j];
			numberFound = true;
			adjacents.AddRange(getAdjacent(schematic, i, j));
			starPositions.AddRange(getStarPosition(schematic, i, j));
		}
		else
		{
			if (numberFound)
			{
				if (starPositions.Count > 0)
				{
					starPositions = starPositions.Distinct().ToList<(int, int)>();
					numbersWithAdjacentStars.Add(new numberPlusAdjacentStar(Convert.ToInt32(lineNumber), starPositions));
					starPositions = new();
				}
				foreach (char c in adjacents)
				{
					if (char.IsSymbol(c) && c != '.' || char.IsPunctuation(c) && c != '.')
					{
						Console.Write(lineNumber + ' ');
						totalSum += Convert.ToInt32(lineNumber);
						break;
					} // else { if (!char.IsDigit(c)) Console.Write(c); }
				}
			}
			numberFound = false;
			adjacents = new List<char>();
			lineNumber = "";
		}
	}
	if (numberFound)
	{
		if (starPositions.Count > 0)
		{
			starPositions = starPositions.Distinct().ToList<(int, int)>();
			numbersWithAdjacentStars.Add(new numberPlusAdjacentStar(Convert.ToInt32(lineNumber), starPositions));
		}
		foreach (char c in adjacents)
		{
			if (char.IsSymbol(c) && c != '.' || char.IsPunctuation(c) && c != '.')
			{
				Console.Write(lineNumber + ' ');
				totalSum += Convert.ToInt32(lineNumber);
				break;
			}// else { if (!char.IsDigit(c)) Console.Write(c); }
		}
	}
}

List<int> part2TotalList = new();

for (int i = 0; i < numbersWithAdjacentStars.Count; i++)
{
	List<int> numbersWithCommonStars = new();
	for (int j = 0; j < numbersWithAdjacentStars.Count; j++)
	{
		if (i == j) continue;

		foreach ((int, int) starPosition in numbersWithAdjacentStars[j].adjacentStars)
		{
			if (numbersWithAdjacentStars[i].adjacentStars.Contains(starPosition))
			{
				numbersWithCommonStars.Add(numbersWithAdjacentStars[i].number);
				numbersWithCommonStars.Add(numbersWithAdjacentStars[j].number);
			}
		}
	}
	if (numbersWithCommonStars.Count == 2)
	{
		if (!part2TotalList.Contains(numbersWithCommonStars[0] * numbersWithCommonStars[1]))
		{
			part2TotalList.Add(numbersWithCommonStars[0] * numbersWithCommonStars[1]);
		}
	}
}

part2TotalSum = part2TotalList.Sum();
Console.WriteLine(Environment.NewLine + "Total : " + totalSum);

List<char> getAdjacent(List<char[]> array, int i, int j)
{
	int n = array.Count - 1;
	// Assuming all lines have the same length, true in this challenge
	int m = array[0].Length;

	List<char> result = new List<char>();

	for (int dx = (i > 0 ? -1 : 0); dx <= (i < n ? 1 : 0); dx++)
	{
		for (int dy = (j > 0 ? -1 : 0); dy <= (j < n ? 1 : 0); dy++)
		{
			if (dx != 0 || dy != 0)
			{
				result.Add(array[i + dx][j + dy]);
			}
		}
	}

	return result;
}

List<(int, int)> getStarPosition(List<char[]> array, int i, int j)
{
	int n = array.Count - 1;
	// Assuming all lines have the same length, true in this challenge
	int m = array[0].Length;

	List<(int, int)> result = new List<(int, int)>();

	for (int dx = (i > 0 ? -1 : 0); dx <= (i < n ? 1 : 0); dx++)
	{
		for (int dy = (j > 0 ? -1 : 0); dy <= (j < n ? 1 : 0); dy++)
		{
			if (dx != 0 || dy != 0)
			{
				if (array[i + dx][j + dy] == '*')
				{
					if (!result.Contains((i + dx, j + dy)))
					{
						result.Add((i + dx, j + dy));
					}
				}
			}
		}
	}

	return result;
}

struct numberPlusAdjacentStar
{
	public int number;
	public List<(int, int)> adjacentStars;

	public numberPlusAdjacentStar(int number, List<(int, int)> adjacentStars)
	{
		this.number = number;
		this.adjacentStars = adjacentStars;
	}
}