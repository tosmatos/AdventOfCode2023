string[] input = File.ReadAllLines("input.txt");

int totalPart1 = 0;
int totalPart2 = 0;

bool part2 = true; // change to get results for part 1 or 2

List<Hand> orderedHands = new List<Hand>();

Dictionary<HandType, List<Hand>> handsGroups = new()
{
    {HandType.HighCard, new()},
    {HandType.OnePair, new()},
    {HandType.TwoPair, new()},
    {HandType.ThreeOfAKind, new()},
    {HandType.FullHouse, new()},
    {HandType.FourOfAKind, new()},
    {HandType.FiveOfAKind, new()},
};

// Loop to populate 
foreach (string play in input)
{
    Hand currentHand = new();
    currentHand.hand = play.Split(' ')[0];
    currentHand.bid = Convert.ToInt32(play.Split(' ')[1]);

    List<(char Card, int Quantity)> currentHandCards = new();

    for (int i = 0; i < currentHand.hand.Length; i++)
    {
        char c = currentHand.hand[i];
        if (!currentHandCards.Any(item => item.Card == c)) currentHandCards.Add((c, 1));
        else
        {
            var index = currentHandCards.FindIndex(item => item.Card == c);
            var quantity = currentHandCards[index].Quantity;
            quantity += 1;
            currentHandCards[index] = (currentHandCards[index].Card, quantity);
        }
    }

    if (currentHandCards.Count == 1) currentHand.type = HandType.FiveOfAKind;
    else if (currentHandCards.Count == 2)
    {
        if (currentHandCards[0].Quantity == 4 || currentHandCards[1].Quantity == 4)
            currentHand.type = HandType.FourOfAKind;
        else currentHand.type = HandType.FullHouse;
    }
    else if (currentHandCards.Count == 3)
    {
        if (currentHandCards[0].Quantity == 3 || currentHandCards[1].Quantity == 3 || currentHandCards[2].Quantity == 3)
            currentHand.type = HandType.ThreeOfAKind;
        else currentHand.type = HandType.TwoPair;
    }
    else if (currentHandCards.Count == 4) currentHand.type = HandType.OnePair;
    else currentHand.type = HandType.HighCard;

    if (part2)
    {
        if (currentHand.hand.Contains('J'))
        {
            int jCount = currentHand.hand.ToList().Where(card => card == 'J').Count();
            switch (jCount)
            {
                case 1:
                    if (currentHand.type == HandType.HighCard) currentHand.type = HandType.OnePair;
                    else if (currentHand.type == HandType.OnePair) currentHand.type = HandType.ThreeOfAKind;
                    else if (currentHand.type == HandType.TwoPair) currentHand.type = HandType.FullHouse;
                    else if (currentHand.type == HandType.ThreeOfAKind) currentHand.type = HandType.FourOfAKind;
                    else if (currentHand.type == HandType.FourOfAKind) currentHand.type = HandType.FiveOfAKind;
                    break;
                case 2:
                    if (currentHand.type == HandType.OnePair) currentHand.type = HandType.ThreeOfAKind;
                    else if (currentHand.type == HandType.TwoPair) currentHand.type = HandType.FourOfAKind;
                    else if (currentHand.type == HandType.FullHouse) currentHand.type = HandType.FiveOfAKind;
                    break;
                case 3:
                    if (currentHand.type == HandType.ThreeOfAKind) currentHand.type = HandType.FourOfAKind;
                    else if (currentHand.type == HandType.FullHouse) currentHand.type = HandType.FiveOfAKind;
                    break;
                case 4:
                    currentHand.type = HandType.FiveOfAKind;
                    break;
            }
        }
    }
    handsGroups[currentHand.type].Add(currentHand);
}

foreach (var handGroup in handsGroups)
{
    List<Hand> sortedHandGroup = handGroup.Value.OrderBy(hand => hand, new HandComparer(part2)).ToList();
    orderedHands.AddRange(sortedHandGroup);
}

if (part2)
{
    for (int i = 0; i < orderedHands.Count; i++)
    {
        totalPart2 += orderedHands[i].bid * (i + 1);
    }
} else
{
    for (int i = 0; i < orderedHands.Count; i++)
    {
        totalPart1 += orderedHands[i].bid * (i + 1);
    }
}

Console.WriteLine("Total part 1 " + totalPart1);
Console.WriteLine("Total part 2 " + totalPart2);

public class HandComparer : IComparer<Hand>
{
    char[] strengthOrder;

    public HandComparer(bool part2)
    {
        if (part2) strengthOrder = ['J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A'];
        else strengthOrder = ['2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'];
    }

    public int Compare(Hand x, Hand y)
    {
        return CompareHands(x.hand, y.hand);
    }

    private int CompareHands(string hand1, string hand2)
    {
        // For longueur de la main, comme ça si on trouve pas -1 ou +1 on regarde la carte suivante
        for (int i = 0; i < Math.Min(hand1.Length, hand2.Length); i++)
        {
            int index1 = Array.IndexOf(strengthOrder, hand1[i]);
            int index2 = Array.IndexOf(strengthOrder, hand2[i]);

            if (index1 < index2) return -1;
            else if (index1 > index2) return 1;
        }
        // On arrive ici que si la main est exactement pareille
        return 0;
    }
}

public enum HandType
{
    None,
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind,
}

public struct Hand
{
    public string hand;
    public int bid;
    public HandType type;
}