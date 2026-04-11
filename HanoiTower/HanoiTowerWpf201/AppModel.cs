using Reactive.Bindings;

namespace HanoiTowerWpf101
{
	public class AppModel
	{
		const int NumberOfDisks = 6;

		readonly Stack<Disk>[] Towers =
		[
			new Stack<Disk>(Enumerable.Range(1, NumberOfDisks).Reverse().Select((id, index) => new Disk(id, 0, index))),
			new Stack<Disk>(),
			new Stack<Disk>(),
		];

		public Disk[] Disks { get; }
		public ReactiveProperty<int> Count { get; } = new ReactiveProperty<int>(0);

		public AppModel()
		{
			Disks = [.. Towers[0]];
			Task.Run(() =>
			{
				Thread.Sleep(1000);
				MoveTower(NumberOfDisks, 0, 2, 1);
			});
		}

		void MoveTower(int n, int from, int to, int via)
		{
			--n;
			if (n > 0) MoveTower(n, from, via, to);
			MoveDisk(from, to);
			if (n > 0) MoveTower(n, via, to, from);
		}

		void MoveDisk(int from, int to)
		{
			Thread.Sleep(400);
			var disk = Towers[from].Pop();
			disk.TowerId.Value = to;
			disk.IndexInTower.Value = Towers[to].Count;
			Towers[to].Push(disk);
			Count.Value++;
		}
	}
}
