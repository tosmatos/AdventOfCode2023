string[] input = File.ReadAllLines("input.txt");

char[] directions = input[0].ToCharArray();

int steps = 0;
ulong stepsPart2 = 0;

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

bool foundAllZ = false;
directionIndex = 0;

while (!foundAllZ) // for part 2
{
	stepsPart2++;

	if (directionIndex == directions.Length)
		directionIndex = 0;

	char direction = directions[directionIndex];
	if (searchedNodes[5][2] == 'A')
		PrintNode(searchedNodes[5]);

	//for (int i = 0; i < searchedNodes.Count; i++)
	Parallel.For(0, searchedNodes.Count, (i) =>
	{
		string searchedNode = searchedNodes[i];

		string nextNode = direction == 'L' ? nodes[searchedNode].Left : nodes[searchedNode].Right;
		searchedNodes[i] = nextNode;
	});

	if (searchedNodes[5][2] == 'Z')
		PrintNode(searchedNodes[5]);

	int zCount = searchedNodes.Where(node => node[2] == 'Z').Count();
	if (zCount == searchedNodes.Count)
		foundAllZ = true;
	directionIndex++;
}

Console.WriteLine($"Steps needed to reach ZZZ : {steps}");
Console.WriteLine($"Steps needed to reach all nodes that end with Z : {stepsPart2}");

void PrintNode(string node)
{
	Console.WriteLine($"{node} : ({nodes[node].Left}, {nodes[node].Right})");
}