using System.Reactive.Linq;
using Reactive.Bindings;

namespace HanoiTowerWpf101
{
	public class Disk
	{
		public int Id { get; }
		public ReactiveProperty<int> TowerId { get; }
		public ReactiveProperty<int> IndexInTower { get; }

		public int Width => Id * 30 + 10;
		public ReadOnlyReactiveProperty<int> Left { get; }
		public ReadOnlyReactiveProperty<int> Top { get; }

		public Disk(int id, int towerId, int index)
		{
			Id = id;
			TowerId = new ReactiveProperty<int>(towerId);
			IndexInTower = new ReactiveProperty<int>(index);

			Left = TowerId.Select(x => x * 290).ToReadOnlyReactiveProperty();
			Top = IndexInTower.Select(x => (x + 1) * -32).ToReadOnlyReactiveProperty();
		}
	}
}
