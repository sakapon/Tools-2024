namespace HanoiTowerWpf102
{
	public class Disk(int id)
	{
		public int Id { get; } = id;
		public int Width => Id * 30 + 10;
	}
}
