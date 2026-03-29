namespace HanoiTowerWpf102
{
	public class Disk
	{
		public int Id { get; }
		public int Width => Id * 20;

		public Disk(int id)
		{
			Id = id;
		}
	}
}
