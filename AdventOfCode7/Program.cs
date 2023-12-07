string[] input = File.ReadAllLines("input.txt");

List<Hand> orderedHands = new List<Hand>();

char[] strengthOrder = new char[]
{
	'2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'
};

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

	handsGroups[currentHand.type].Add(currentHand);
}

foreach (var handGroup in handsGroups)
{
	List<Hand> orderedHandGroup = new();
	CustomHandSort(handGroup.Value);
	Console.WriteLine("Sorted group" + handGroup.Key);
	for (int i = 0; i < handGroup.Value.Count; i++)
	{
		Hand hand = handGroup.Value[i];
		Console.WriteLine(hand.hand);
	}
}

Console.WriteLine("coucou");

void CustomHandSort(List<Hand> handList)
{
	bool itemMoved = false;
	do
	{
		itemMoved = false;
		for (int i = 0; i < handList.Count - 1; i++)
		{
			var hand = handList[i].hand;
			var nextHand = handList[i + 1].hand;
			if (Array.IndexOf(strengthOrder, hand[0]) < Array.IndexOf(strengthOrder, nextHand[0]))
			{
				var lowerValue = handList[i];
				handList[i] = handList[i + 1];
				handList[i + 1] = lowerValue;
				itemMoved = true;
			}
			else if (Array.IndexOf(strengthOrder, hand[1]) < Array.IndexOf(strengthOrder, nextHand[1]))
			{
				var lowerValue = handList[i];
				handList[i] = handList[i + 1];
				handList[i + 1] = lowerValue;
				itemMoved = true;
			}
			else if (Array.IndexOf(strengthOrder, hand[2]) < Array.IndexOf(strengthOrder, nextHand[2]))
			{
				var lowerValue = handList[i];
				handList[i] = handList[i + 1];
				handList[i + 1] = lowerValue;
				itemMoved = true;
			}
			else if (Array.IndexOf(strengthOrder, hand[3]) < Array.IndexOf(strengthOrder, nextHand[3]))
			{
				var lowerValue = handList[i];
				handList[i] = handList[i + 1];
				handList[i + 1] = lowerValue;
				itemMoved = true;
			}
			else if (Array.IndexOf(strengthOrder, hand[4]) < Array.IndexOf(strengthOrder, nextHand[4]))
			{
				var lowerValue = handList[i];
				handList[i] = handList[i + 1];
				handList[i + 1] = lowerValue;
				itemMoved = true;
			}
		}
	} while (itemMoved);
}

Console.WriteLine("coucou");
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