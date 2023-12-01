string[] inputLines = File.ReadAllLines("input.txt");
int part1CalibrationTotal = 0;
int part2CalibrationTotal = 0;

Dictionary<string, string> numberStringToInt = new()
{
	{"one", "1"},
	{"two", "2"},
	{"three", "3"},
	{"four", "4"},
	{"five", "5"},
	{"six", "6"},
	{"seven", "7"},
	{"eight", "8"},
	{"nine", "9"},
};

foreach (string line in inputLines)
{
	List<char> part1LineValues = new();
	List<char> part2LineValues = new();
	string newLine = line;

	foreach (KeyValuePair<string, string> numberString in numberStringToInt)
	{
		newLine = newLine.Replace(numberString.Key, numberString.Key + numberString.Value + numberString.Key);
	}

	// for part 2
	Console.WriteLine(line);
	Console.WriteLine("Nouvelle ligne : " + newLine);

	foreach (char c in line)
	{
		if (char.IsDigit(c))
		{
			part1LineValues.Add(c);
		}
	}

	foreach (char c in newLine)
	{
		if (char.IsDigit(c))
		{
			part2LineValues.Add(c);
		}
	}
	string valueString = "";

	valueString = part1LineValues[0].ToString() + part1LineValues.Last();
	part1CalibrationTotal += Convert.ToInt32(valueString);

	valueString = part2LineValues[0].ToString() + part2LineValues.Last();
	part2CalibrationTotal += Convert.ToInt32(valueString);
	
	// for part 2
	Console.WriteLine("Valeur trouvée : " + valueString + Environment.NewLine);
}
Console.WriteLine($"Total calibration part 1: {part1CalibrationTotal}");
Console.WriteLine($"Total calibration part 2: {part2CalibrationTotal}");