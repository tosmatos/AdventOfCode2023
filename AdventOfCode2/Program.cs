string[] inputLines = File.ReadAllLines("input.txt");

int idSum = 0;
int part2Sum = 0;


foreach (string line in inputLines)
{
	Console.WriteLine(line);
	int gameId = 0;
	gameId = Convert.ToInt32(line.Split(':')[0].Substring(5));
	string plays = line.Split(":")[1];

	Console.WriteLine($"Game ID : {gameId}");

	bool impossibleGame = false;

	foreach (string hand in plays.Split(';'))
	{
		foreach (string cube in hand.Split(','))
		{
			string cleanString = cube.Trim();
			int numberOfCubes = Convert.ToInt32(cleanString.Split(' ')[0]);
			string cubeColor = cleanString.Split(' ')[1];

			Console.WriteLine($"Cube color : {cubeColor}, number : {numberOfCubes}");

			if (cubeColor == "red" && numberOfCubes > 12
				|| cubeColor == "green" && numberOfCubes > 13
				|| cubeColor == "blue" && numberOfCubes > 14)
			{
				impossibleGame = true;
				Console.WriteLine("Impossible hand");
			}
		}
	}

	part2Sum += part2Count(plays);

	
	if (!impossibleGame)
	{
		idSum += gameId;
		Console.WriteLine($"idSum : {idSum}, gameId = {gameId}");
	}
	Console.WriteLine(Environment.NewLine);
}

Console.Write("idSum : " + idSum);
Console.WriteLine($"Part 2 sum : {part2Sum}");

int part2Count(string plays)
{
	int redMax = 1, greenMax = 1, blueMax = 1;

	foreach (string hand in plays.Split(';'))
	{
		foreach (string cube in hand.Split(","))
		{
			string cleanString = cube.Trim();
			int numberOfCubes = Convert.ToInt32(cleanString.Split(' ')[0]);
			string cubeColor = cleanString.Split(' ')[1];

			if (cubeColor == "red" && numberOfCubes > redMax)
			{
				redMax = numberOfCubes;
			}
			if (cubeColor == "green" && numberOfCubes > greenMax)
			{
				greenMax = numberOfCubes;
			}
			if (cubeColor == "blue" && numberOfCubes > blueMax)
			{
				blueMax = numberOfCubes;
			}
		}
	}
		

	return redMax * greenMax * blueMax;

}