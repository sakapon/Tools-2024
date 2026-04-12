using Reactive.Bindings;

namespace HanoiTowerWpf201
{
	public class Disk(int id, int towerId, int index)
	{
		public int Id { get; } = id;
		public ReactiveProperty<int> TowerId { get; } = new ReactiveProperty<int>(towerId);
		public ReactiveProperty<int> IndexInTower { get; } = new ReactiveProperty<int>(index);

		public int Width => Id * 30 + 10;
	}
}
