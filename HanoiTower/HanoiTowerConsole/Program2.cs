namespace HanoiTowerConsole
{
	record struct Disk(int Id, int TowerId, int Index);

	class Program2
	{
		const int NumberOfDisks = 5;

		static readonly Stack<Disk>[] Towers =
		[
			new Stack<Disk>(Enumerable.Range(1, NumberOfDisks).Reverse().Select((id, index) => new Disk(id, 0, index))),
			new Stack<Disk>(),
			new Stack<Disk>(),
		];

		public static void Main2()
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
			var disk = Towers[from].Pop();
			disk.TowerId = to;
			disk.Index = Towers[to].Count;
			Towers[to].Push(disk);
		}
	}
}
