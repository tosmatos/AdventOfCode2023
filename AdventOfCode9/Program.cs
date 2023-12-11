string[] input = File.ReadAllLines("input.txt");

int total = 0;

foreach (string line in input)
{
	// Reverse searchedLine to get answer for part 2
	int[] searchedLine = Array.ConvertAll(line.Split(' '), Convert.ToInt32); //.Reverse().ToArray();
	List<int> newLine = [];
	int extrapolatedValue = searchedLine.Last();
	bool allZeroes = false;
	
	while (!allZeroes)
	{
		for (int i = 0; i < searchedLine.Length - 1; i++)
		{
			newLine.Add(searchedLine[i + 1] - searchedLine[i]);
		}

		extrapolatedValue += newLine.Last();
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
	total += extrapolatedValue;
}
Console.WriteLine($"Sum of all extrapolated values (part 1) : {total}");