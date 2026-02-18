namespace HanoiTowerConsole
{
	class Program
	{
		const int NumberOfDisks = 5;

		static readonly Stack<int>[] Towers =
		[
			new Stack<int>(Enumerable.Range(1, NumberOfDisks).Reverse()),
			new Stack<int>(),
			new Stack<int>(),
		];

		static void Main()
		{
			MoveTower(NumberOfDisks, 0, 2, 1);
		}

		static void MoveTower(int n, int from, int to, int via)
		{
			--n;
			if (n > 0) MoveTower(n, from, via, to);
			MoveDisk(from, to);
			if (n > 0) MoveTower(n, via, to, from);
		}

		static void MoveDisk(int from, int to)
		{
			Towers[to].Push(Towers[from].Pop());
			Console.WriteLine($"{Towers[0].Count} {Towers[1].Count} {Towers[2].Count}");
		}
	}
}
