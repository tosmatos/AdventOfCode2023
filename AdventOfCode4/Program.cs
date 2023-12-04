string[] inputLines = File.ReadAllLines("input.txt");

int totalScore = 0;

foreach (string line in inputLines)
{
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

    totalScore += cardScore;
}

Console.WriteLine($"Total score : {totalScore}");