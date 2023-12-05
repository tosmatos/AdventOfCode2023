string[] input = File.ReadAllLines("input.txt");

uint[] seeds = Array.ConvertAll(input[0].Split('\n')[0].Split(':')[1].Trim().Split(' '), UInt32.Parse);

List<List<(uint DestinationRange, uint SourceRange, uint RangeLength)>> maps = new();

bool gettingMap = false;

List<List<(uint, uint)>> correspondanceTables = new();

List<(uint, uint, uint)> currentMap = new();

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
			uint[] mapLine = Array.ConvertAll(input[i].Split(' '), UInt32.Parse);
			currentMap.Add((mapLine[0], mapLine[1], mapLine[2]));
		}
	}
}

if (gettingMap) maps.Add(currentMap);// ça veut dire qu'on choppais une ligne mais que la boucle à fini

// Populate correspondance list
foreach (List<(uint, uint, uint)> map in maps)
{
	List<(uint, uint)> currentCorrespondance = new();

	foreach ((uint DestinationRange, uint SourceRange, uint RangeLength) mapLine in map)
	{
		for (uint i = 0; i < mapLine.RangeLength; i++)
		{
			currentCorrespondance.Add((mapLine.SourceRange + i, mapLine.DestinationRange + i));
		}
	}
	correspondanceTables.Add(currentCorrespondance);
}

List<int> locationList = new();

uint searchedNumber = 0;
foreach (int seed in seeds)
{
	searchedNumber = (uint)seed;
	foreach (var correspondanceTable in correspondanceTables)
	{
		(uint, uint) foundCorrespondance = correspondanceTable.Find(x => x.Item1 == searchedNumber);
		if (foundCorrespondance != (0, 0)) // ça veut dire pas de correspondance trouvée
		{
			searchedNumber = foundCorrespondance.Item2;
		}
	}

	locationList.Add((int)searchedNumber);
}

Console.WriteLine($"Smallest location : {locationList.Min()}");