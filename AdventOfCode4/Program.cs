string[] inputLines = File.ReadAllLines("input.txt");

// Dictionary ou les valeurs sont le nombre de cartes, 1 de base
Dictionary<string, int> inputLinesDict = inputLines.ToDictionary(key => key, value => 1);

// ToArray() pour pouvoir accéder avec index
(string Card, int Quantity)[] cards = inputLinesDict.Select(element => (Card: element.Key, Quantity: element.Value)).ToArray();

int part1TotalScore = 0;
int part2TotalScore = 0;

// Part 1
for (int i = 0; i < cards.Length; i++)
{
    string line = cards[i].Card;
    string card = line.Split(':')[1];
    string leftSide = card.Split('|')[0];
    string rightSide = card.Split('|')[1];

    string[] winningNumbersString = leftSide.Split(' ');
    string[] playedNumbersString = rightSide.Split(' ');
    
    winningNumbersString = winningNumbersString.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
    playedNumbersString = playedNumbersString.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

    int[] winningNumbers = Array.ConvertAll(winningNumbersString, Int32.Parse);
    int[] playedNumbers = Array.ConvertAll(playedNumbersString, Int32.Parse);

    int cardScore = 0;

    foreach(int number in playedNumbers)
    {
        if (winningNumbers.Contains(number))
        {
            cardScore = cardScore == 0 ? 1 : cardScore *= 2;
        }
    }

    part1TotalScore += cardScore;
}

// Part 2
for (int i = 0; i < cards.Length; i++)
{
	string line = cards[i].Card;
	string card = line.Split(':')[1];
	string leftSide = card.Split('|')[0];
	string rightSide = card.Split('|')[1];

	string[] winningNumbersString = leftSide.Split(' ');
	string[] playedNumbersString = rightSide.Split(' ');

	winningNumbersString = winningNumbersString.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
	playedNumbersString = playedNumbersString.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

	int[] winningNumbers = Array.ConvertAll(winningNumbersString, Int32.Parse);
	int[] playedNumbers = Array.ConvertAll(playedNumbersString, Int32.Parse);

	int cardsWon = 0;

	foreach (int number in playedNumbers)
	{
		if (winningNumbers.Contains(number))
		{
			cardsWon += 1;
		}
	}

	for (int j = 0; j < cards[i].Quantity; j++)
	{
		for (int k = 1; k <= cardsWon; k++)
		{
			cards[i + k].Quantity += 1;
		}
	}
}

foreach (var card in cards)
{
    part2TotalScore += card.Quantity;
}

Console.WriteLine($"Part 1 total score : {part1TotalScore}");
Console.WriteLine($"Part 2 total score : {part2TotalScore}");