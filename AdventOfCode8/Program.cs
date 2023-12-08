string[] input = File.ReadAllLines("input.txt");

char[] directions = input[0].ToCharArray();

int steps = 0;

bool foundZZZ = false;

Dictionary<string, (string Left, string Right)> nodes = new();

List<string> searchedNodes = new();

for (int i = 2; i < input.Length; i++) // Start at two to ignore first two lines
{
	// Populate nodes dictionary
	string node, left, right;
	string leftRight;

	node = input[i].Split(" = ")[0]; // XXX
	leftRight = input[i].Split(" = ")[1]; // (XXX, XXX)
	left = leftRight.Substring(1, 3);
	right = leftRight.Substring(6, 3);

	if (node[2] == 'A')
		searchedNodes.Add(node);

	nodes.Add(node, (left, right));
}

int directionIndex = 0;
string searchedNode = "AAA";

while (!foundZZZ) //  while for part 1
{
	steps++;

	if (directionIndex == directions.Length)
		directionIndex = 0;

	char direction = directions[directionIndex];

	string nextNode = direction == 'L' ? nodes[searchedNode].Left : nodes[searchedNode].Right;

	if (nextNode == "ZZZ")
		foundZZZ = true;
	searchedNode = nextNode;
	directionIndex++;
}

List<int> searchedNodesSteps = new();

foreach (string node in searchedNodes)
{
	PrintNode(node);
	bool foundEndZ = false;
	searchedNode = node;
	int innerSteps = 0;
	directionIndex = 0;
	while (!foundEndZ)
	{
		innerSteps++;
		if (directionIndex == directions.Length)
			directionIndex = 0;

		char direction = directions[directionIndex];

		string nextNode = direction == 'L' ? nodes[searchedNode].Left : nodes[searchedNode].Right;

		if (nextNode[2] == 'Z')
			foundEndZ = true;
		searchedNode = nextNode;
		directionIndex++;
	}
	searchedNodesSteps.Add(innerSteps);
	Console.WriteLine($"Found end node in {innerSteps}. End node :");
	PrintNode(searchedNode);
}



Console.WriteLine($"Steps needed to reach ZZZ : {steps}");
Console.WriteLine($"Steps needed to reach all nodes that end with Z : {CalculateLCMList(searchedNodesSteps)}");

void PrintNode(string node)
{
	Console.WriteLine($"{node} : ({nodes[node].Left}, {nodes[node].Right})");
}

long CalculateGCD(long a, long b)
{
	while (b != 0)
	{
		long temp = b;
		b = a % b;
		a = temp;
	}
	return a;
}

long CalculateLCM(long a, long b)
{
	return (a * b) / CalculateGCD(a, b);
}

long CalculateLCMList(List<int> numbers)
{
	long lcm = CalculateLCM(numbers[0], numbers[1]);

	for (int i = 2; i < numbers.Count; i++)
		// because property LCM(a, b, c) == LCM(LCM(a, b), c)
		lcm = CalculateLCM(lcm, numbers[i]);

	return lcm;
}