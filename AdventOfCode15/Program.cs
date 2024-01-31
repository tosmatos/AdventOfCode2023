string input = File.ReadAllText("input.txt");

input = input.Replace("\n", String.Empty);

int currentValue = 0;
int totalValues = 0;

Dictionary<char, int> charPlusAscii = new();

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

	if (!charPlusAscii.ContainsKey(c))
		charPlusAscii.Add(c, c);
}

totalValues += currentValue; // Account last step not having a comma after it

Console.WriteLine($"Sum of HASH's : {totalValues}");