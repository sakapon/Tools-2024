using System.Collections.ObjectModel;
using Reactive.Bindings;

namespace HanoiTowerOS102
{
	// OpenSilver では、非同期処理もすべて UI スレッドで動作します。
	// したがって、ReactiveCollection は不要です (ReactiveCollection のままでも可)。
	public class AppModel
	{
		const int NumberOfDisks = 5;
		const int Interval_ms = 300;

		public ObservableCollection<Disk>[] Towers { get; } =
		[
			[.. Enumerable.Range(1, NumberOfDisks).Reverse().Select(id => new Disk(id))],
			[],
			[],
		];

		public ReactiveProperty<int> Count { get; } = new ReactiveProperty<int>(0);

		public AppModel()
		{
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
			var disk = Towers[from].Last();
			Towers[from].RemoveAt(Towers[from].Count - 1);
			Towers[to].Add(disk);
			Count.Value++;
		}
	}

	public class Disk(int id)
	{
		public int Id { get; } = id;
		public int Width => Id * 30 + 10;
	}
}
