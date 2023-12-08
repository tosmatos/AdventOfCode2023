string[] input = File.ReadAllLines("input.txt");

List<(int Time, int Distance)> timeDistances = new();

List<string> times = input[0].Split(':')[1].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
List<string> distances = input[1].Split(':')[1].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

for (int i = 0; i < times.Count; i++) timeDistances.Add((Convert.ToInt32(times[i]), Convert.ToInt32(distances[i])));

long part2Time = Convert.ToInt64(times.Aggregate("", (a, b) => a + b));
long part2Distance = Convert.ToInt64(distances.Aggregate("", (a, b) => a + b));

List<int> numberWaysToWinAcrossRaces = GetNumberWaysToWinPart1();

int numberWaysToWinPart2 = 0;
for (int i = 0; i < part2Time; i++)
{
	int holdTime = i;
	long moveTime = part2Time - holdTime;
	long distanceTraveled = moveTime * holdTime;

	if (distanceTraveled > part2Distance)
	{
		numberWaysToWinPart2++;
	}
}

int waysToWinProduct = numberWaysToWinAcrossRaces.Aggregate(1, (a, b) => a * b);
Console.WriteLine("Ways to win product : " + waysToWinProduct);
Console.WriteLine("Ways to win accounting for bad kerning : " + numberWaysToWinPart2);

List<int> GetNumberWaysToWinPart1()
{
	List<int> numberWaysToWinAcrossRaces = new();

	int numberWaysToWin = 0;
	for (int i = 0; i < timeDistances.Count; i++)
	{
		numberWaysToWin = 0;
		(int Time, int Distance) timeDistance = timeDistances[i];
		for (int j = 0; j < timeDistance.Time; j++)
		{
			int holdTime = j;
			int moveTime = timeDistance.Time - holdTime;
			int distanceTraveled = moveTime * holdTime;

			if (distanceTraveled > timeDistance.Distance)
			{
				numberWaysToWin++;
			}
		}
		Console.WriteLine($"Race {i} : number of ways to win : {numberWaysToWin}");
		numberWaysToWinAcrossRaces.Add(numberWaysToWin);
	}

	return numberWaysToWinAcrossRaces;
}