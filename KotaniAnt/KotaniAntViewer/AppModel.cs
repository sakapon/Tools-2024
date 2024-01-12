namespace KotaniAntViewer
{
	public class AppModel
	{
		const int Resolution = 20;
		const int n = 2;

		static double d1(double x, double y) => Math.Sqrt(x * x + (n + y) * (n + y));
		static double d2(double x, double y) => Math.Sqrt((n + 1 - x) * (n + 1 - x) + (1 + y) * (1 + y));

		public int Size => Resolution + 1;
		public Cell[] Cells { get; }

		public AppModel()
		{
			Cells = Enumerable.Range(0, Size * Size)
				.Select(v =>
				{
					var (i, j) = (v / Size, v % Size);
					var (x, y) = ((double)j / Resolution, 1 - (double)i / Resolution);
					return new Cell
					{
						Row = i,
						Column = j,
						X = x,
						Y = y,
						Min1 = x >= y ? d1(x, y) : d1(y, x),
						Min2 = x >= y ? d2(x, y) : d2(y, x),
					};
				})
				.ToArray();
		}
	}

	public class Cell
	{
		public int Row { get; set; }
		public int Column { get; set; }

		public double X { get; set; }
		public double Y { get; set; }

		public double Min => Min1 <= Min2 ? Min1 : Min2;
		public double Min1 { get; set; }
		public double Min2 { get; set; }
	}
}
