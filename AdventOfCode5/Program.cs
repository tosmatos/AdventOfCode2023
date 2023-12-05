string[] input = File.ReadAllLines("input.txt");

long[] seeds = Array.ConvertAll(input[0].Split('\n')[0].Split(':')[1].Trim().Split(' '), Int64.Parse);

List<long> part2Seeds = new();

for (int i = 0;  i < seeds.Length; i++)
{
	if (i % 2 == 0)
	{
		long seedToCopy = seeds[i];
		part2Seeds.Add(seedToCopy);
		for (int j = 1; j < seeds[i + 1]; j++)
		{
			part2Seeds.Add(seedToCopy + j);
		}
	}
}

List<List<(long DestinationRange, long SourceRange, long RangeLength)>> maps = new();

bool gettingMap = false;

List<List<(long, long)>> correspondanceTables = new();

List<(long, long, long)> currentMap = new();

// put all maps in maps List
for (int i = 1; i < input.Length; i++)
{
	if (input[i].Contains("map"))
	{
		gettingMap = true;
		continue;
	}

	if (gettingMap)
	{
		if (string.IsNullOrEmpty(input[i]))
		{
			maps.Add(currentMap);
			gettingMap = false;
			currentMap = new();
		}
		else
		{
			long[] mapLine = Array.ConvertAll(input[i].Split(' '), Int64.Parse);
			currentMap.Add((mapLine[0], mapLine[1], mapLine[2]));
		}
	}
}
if (gettingMap) maps.Add(currentMap);// ça veut dire qu'on choppais une ligne mais que la boucle à fini

long smallestLocation = 9223372036854775807; // max long value
long searchedNumber = 0;
foreach (long seed in part2Seeds) // Replace List to get part 1 or 2
{
	searchedNumber = seed;
	foreach (List<(long DestinationRange, long SourceRange, long RangeLength)> map in maps)
	{
		foreach ((long DestinationRange, long SourceRange, long RangeLength) mapLine in map)
		{
			if (mapLine.SourceRange <= searchedNumber && searchedNumber < mapLine.SourceRange + mapLine.RangeLength)
			{
				searchedNumber = searchedNumber + (mapLine.DestinationRange - mapLine.SourceRange);
				break;
			}
		}
	}
	if (searchedNumber < smallestLocation) smallestLocation = searchedNumber;
}

Console.WriteLine($"Smallest location : {smallestLocation}");