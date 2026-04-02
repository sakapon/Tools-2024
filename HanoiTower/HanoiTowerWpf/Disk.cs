using System.Reactive.Linq;
using System.Reactive.Subjects;
using Reactive.Bindings;

namespace HanoiTowerWpf
{
	public class Disk
	{
		public int Id { get; }
		public ReactiveProperty<Tower> Tower { get; }
		public ReactiveProperty<int> IndexInTower { get; }

		public int Width => Id * 30 + 10;
		public ReadOnlyReactiveProperty<int> DeltaY { get; }

		public Subject<(double X, double Y)> MovedEvent { get; } = new Subject<(double, double)>();

		public Disk(int id, Tower tower, int index)
		{
			Id = id;
			Tower = new ReactiveProperty<Tower>(tower);
			IndexInTower = new ReactiveProperty<int>(index);

			DeltaY = IndexInTower.Select(x => x * -32).ToReadOnlyReactiveProperty();
		}
	}

	public class Tower(int id)
	{
		public int Id { get; } = id;
		public Stack<Disk> Disks { get; } = new Stack<Disk>();

		public ReactiveProperty<double> DeltaX { get; } = new ReactiveProperty<double>(id * 300);
		public ReactiveProperty<double> DeltaY { get; } = new ReactiveProperty<double>(0);
	}
}
