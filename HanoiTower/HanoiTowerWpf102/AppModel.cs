using Reactive.Bindings;

namespace HanoiTowerWpf102
{
	public class AppModel
	{
		const int NumberOfDisks = 5;

		public ReactiveCollection<Disk>[] Towers { get; } =
		[
			[.. Enumerable.Range(1, NumberOfDisks).Reverse().Select(id => new Disk(id))],
			[],
			[],
		];

		public ReactiveProperty<int> Count { get; } = new ReactiveProperty<int>(0);

		public AppModel()
		{
			Task.Run(() => MoveTower(NumberOfDisks, 0, 2, 1));
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
			Thread.Sleep(300);
			var disk = Towers[from].Last();
			Towers[from].RemoveAtOnScheduler(Towers[from].Count - 1);
			Towers[to].AddOnScheduler(disk);
			Count.Value++;
		}
	}

	public class Disk(int id)
	{
		public int Id { get; } = id;
		public int Width => Id * 30 + 10;
	}
}
