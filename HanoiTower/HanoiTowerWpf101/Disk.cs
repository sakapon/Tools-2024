using System.Reactive.Linq;
using Reactive.Bindings;

namespace HanoiTowerWpf101
{
	public class Disk
	{
		public int Id { get; }
		public ReactiveProperty<int> TowerId { get; }
		public ReactiveProperty<int> Index { get; }

		public ReadOnlyReactiveProperty<int> Left { get; }
		public ReadOnlyReactiveProperty<int> Top { get; }

		public Disk(int id, int towerId, int index)
		{
			Id = id;
			TowerId = new ReactiveProperty<int>(towerId);
			Index = new ReactiveProperty<int>(index);

			Left = TowerId.Select(x => x * 100).ToReadOnlyReactiveProperty();
			Top = Index.Select(x => x * 20).ToReadOnlyReactiveProperty();
		}
	}
}
