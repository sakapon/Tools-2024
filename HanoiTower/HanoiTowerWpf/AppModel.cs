using Reactive.Bindings;

namespace HanoiTowerWpf
{
	public class AppModel
	{
		const int NumberOfDisks = 5;

		public Tower[] Towers { get; } = [.. Enumerable.Range(0, 3).Select(id => new Tower(id))];
		public Disk[] Disks { get; }
		public ReactiveProperty<int> Count { get; } = new ReactiveProperty<int>(0);

		public AppModel()
		{
			var disks_r = Enumerable.Range(1, NumberOfDisks).Reverse().Select((id, index) => new Disk(id, Towers[0], index));
			foreach (var disk in disks_r)
				Towers[0].Disks.Push(disk);
			Disks = [.. Towers[0].Disks];

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
			Thread.Sleep(500);
			var disk = Towers[from].Disks.Pop();
			var (oldX, oldY) = (disk.Tower.Value.DeltaX.Value, disk.Tower.Value.DeltaY.Value + disk.DeltaY.Value);

			disk.Tower.Value = Towers[to];
			disk.IndexInTower.Value = Towers[to].Disks.Count;
			var (newX, newY) = (disk.Tower.Value.DeltaX.Value, disk.Tower.Value.DeltaY.Value + disk.DeltaY.Value);

			disk.MovedEvent.OnNext((oldX - newX, oldY - newY));
			Towers[to].Disks.Push(disk);
			Count.Value++;
		}
	}
}
