string[] input = File.ReadAllLines("input.txt");

int totalPart1 = 0;

foreach (string line in input)
{
	int[] searchedLine = Array.ConvertAll(line.Split(' '), Convert.ToInt32);
	List<int> newLine = [];
	int extrapolatedValue = searchedLine.Last();
	bool allZeroes = false;
	
	while (!allZeroes)
	{
		for (int i = 0; i < searchedLine.Length - 1; i++)
		{
			//int max = Math.Max(searchedLine[i], searchedLine[i + 1]);
			//int min = Math.Min(searchedLine[i], searchedLine[i + 1]);
			newLine.Add(searchedLine[i + 1] - searchedLine[i]);
		}
		extrapolatedValue += newLine.LastOrDefault();
		int zeroCount = newLine.Where(number => number == 0).Count();
		if (newLine.Count() == zeroCount)
			allZeroes = true;
		else
		{
			searchedLine = [];
			searchedLine = newLine.ToArray();
			newLine = [];
		}
	}

	totalPart1 += extrapolatedValue;
}

Console.WriteLine($"Sum of all extrapolated values (part 1) : {totalPart1}");