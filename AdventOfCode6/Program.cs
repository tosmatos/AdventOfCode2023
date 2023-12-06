string[] input = File.ReadAllLines("input.txt");

List<(int Time, int Distance)> timeDistances = new();

List<string> times = input[0].Split(':')[1].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
List<string> distances = input[1].Split(':')[1].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

for (int i = 0; i < times.Count; i++) timeDistances.Add((Convert.ToInt32(times[i]), Convert.ToInt32(distances[i])));

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

int waysToWinProduct = numberWaysToWinAcrossRaces.Aggregate(1, (a, b) => a * b);
Console.WriteLine("Ways to win product : " + waysToWinProduct);