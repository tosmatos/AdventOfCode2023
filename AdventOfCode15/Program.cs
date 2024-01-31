string input = File.ReadAllText("input.txt");

input = input.Replace("\n", String.Empty);

int currentValue = 0;
int totalValues = 0;

// Part 1 loop
foreach (char c in input)
{
	if (c == ',')
	{
		totalValues += currentValue;
		currentValue = 0;
		continue;
	}

	currentValue += (int)c;
	currentValue *= 17;
	currentValue %= 256;
}

totalValues += currentValue; // Account last step not having a comma after it
currentValue = 0;

Console.WriteLine($"Sum of HASH's : {totalValues}");

Dictionary<string, int> boxMap = new();
bool searchingLabel = true;
string label = "";

foreach (char c in input)
{
	if (!searchingLabel)
	{
		if (c == ',')
		{
			searchingLabel = true;
			continue;
		}
		continue;
	}

	if (c == '=' || c == '-')
	{
		searchingLabel = false;
		if (!boxMap.ContainsKey(label))
			boxMap.Add(label, currentValue);
		label = "";
		currentValue = 0;
		continue;
	}

	label += c;
	currentValue += (int)c;
	currentValue *= 17;
	currentValue %= 256;
}

List<Box> boxes = new();
searchingLabel = true;

foreach (var box in boxMap)
{
	if (!boxes.Any(b => b.BoxNumber == box.Value))
	{
		boxes.Add(new Box
		{
			BoxNumber = box.Value,
			Lenses = new List<KeyValuePair<string, int>>()
		});
	}
	//if (!boxes.ContainsKey(box.Value))
	//	boxes.Add(box.Value, new());
}

bool addedBox = false;

foreach (char c in input)
{
	if (addedBox)
	{
		addedBox = false;
		continue;
	}

	if (!searchingLabel)
	{
		List<KeyValuePair<string, int>> lenses = boxes.Find(b => b.BoxNumber == boxMap[label]).Lenses;
		if (c == ',') // means no number so symbol was -
		{
			if (lenses.Contains(lenses.Find(l => l.Key == label)))
				lenses.Remove(lenses.Find(l => l.Key == label));
			searchingLabel = true;
			label = "";
			continue;
		}
		// if not a comma, we know it's a number and the next one's a comma
		if (lenses.Contains(lenses.Find(l => l.Key == label)))
			lenses[lenses.IndexOf(lenses.Find(l => l.Key == label))] = new KeyValuePair<string, int>(label, int.Parse(c.ToString()));
		else lenses.Add(new KeyValuePair<string, int>(label, int.Parse(c.ToString())));
		searchingLabel = true;
		label = "";
		addedBox = true;
		continue;
	}

	if (c == '=' || c == '-')
	{
		searchingLabel = false;
		continue;
	}

	label += c;
}

int totalFocusingPower = 0;

// count final focusing power
foreach (Box box in boxes)
{
	if (box.Lenses.Count != 0)
	{
		int boxNumber = box.BoxNumber + 1;
		int slotCounter = 1;
		foreach (KeyValuePair<string, int> lens in box.Lenses)
		{
			int focusingPower = 0;
			focusingPower = boxNumber * slotCounter * lens.Value;
			totalFocusingPower += focusingPower;
			slotCounter++;
		}
	}
}

Console.WriteLine($"Total focusing power : {totalFocusingPower}" );

struct Box
{
	public int BoxNumber;
	public List<KeyValuePair<string, int>> Lenses;
}