using System.Reactive.Linq;
using Reactive.Bindings;

namespace HanoiTowerWpf
{
	public class Disk
	{
		public int Id { get; }
		public ReactiveProperty<int> TowerId { get; }
		public ReactiveProperty<int> Index { get; }

		public int Width => Id * 20;
		public ReadOnlyReactiveProperty<int> Left { get; }
		public ReadOnlyReactiveProperty<int> Top { get; }

		public Disk(int id, int towerId, int index)
		{
			Id = id;
			TowerId = new ReactiveProperty<int>(towerId);
			Index = new ReactiveProperty<int>(index);

			Left = TowerId.Select(x => x * 200).ToReadOnlyReactiveProperty();
			Top = Index.Select(x => x * 25).ToReadOnlyReactiveProperty();
		}
	}
}
