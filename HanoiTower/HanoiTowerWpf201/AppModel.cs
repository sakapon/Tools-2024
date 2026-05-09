using Reactive.Bindings;

namespace HanoiTowerWpf201
{
	public class AppModel
	{
		const int NumberOfDisks = 6;
		const int Interval_ms = 400;

		readonly Stack<Disk>[] Towers =
		[
			new Stack<Disk>(Enumerable.Range(1, NumberOfDisks).Reverse().Select((id, index) => new Disk(id, 0, index))),
			[],
			[],
		];

		public Disk[] Disks { get; }
		public ReactiveProperty<int> Count { get; } = new ReactiveProperty<int>(0);

		public AppModel()
		{
			Disks = [.. Towers[0]];
			Task.Delay(1000)
				.ContinueWith(_ => MoveTower(NumberOfDisks, 0, 2, 1));
		}

		async Task MoveTower(int n, int from, int to, int via)
		{
			--n;
			if (n > 0) await MoveTower(n, from, via, to);
			await MoveDisk(from, to);
			if (n > 0) await MoveTower(n, via, to, from);
		}

		async Task MoveDisk(int from, int to)
		{
			await Task.Delay(Interval_ms);
			var disk = Towers[from].Pop();
			disk.TowerId.Value = to;
			disk.IndexInTower.Value = Towers[to].Count;
			Towers[to].Push(disk);
			Count.Value++;
		}
	}

	public class Disk(int id, int towerId, int index)
	{
		public int Id { get; } = id;
		public ReactiveProperty<int> TowerId { get; } = new ReactiveProperty<int>(towerId);
		public ReactiveProperty<int> IndexInTower { get; } = new ReactiveProperty<int>(index);
	}
}
