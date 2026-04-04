using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows;
using Reactive.Bindings;

namespace HanoiTowerWpf
{
	public class Disk
	{
		public int Id { get; }
		public ReactiveProperty<Tower> Tower { get; }
		public ReactiveProperty<int> IndexInTower { get; }

		public int Width => Id * 30 + 10;
		public ReadOnlyReactiveProperty<Vector> Delta { get; }

		public Subject<Vector> MovedEvent { get; } = new Subject<Vector>();

		public Disk(int id, Tower tower, int index)
		{
			Id = id;
			Tower = new ReactiveProperty<Tower>(tower);
			IndexInTower = new ReactiveProperty<int>(index);

			Delta = IndexInTower.Select(x => new Vector(0, (x + 1) * -32)).ToReadOnlyReactiveProperty();
		}
	}

	public class Tower(int id)
	{
		public int Id { get; } = id;
		public Stack<Disk> Disks { get; } = new Stack<Disk>();

		public ReactiveProperty<Vector> Delta { get; } = new ReactiveProperty<Vector>(new Vector(id * 300, 0));
	}
}
