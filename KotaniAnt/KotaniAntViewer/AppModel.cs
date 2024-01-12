using Reactive.Bindings;

namespace KotaniAntViewer
{
	public class AppModel
	{
		public const int Resolution = 20;
		public const int n = 2;

		static double d1(double x, double y) => Math.Sqrt(x * x + (n + y) * (n + y));
		static double d2(double x, double y) => Math.Sqrt((n + 1 - x) * (n + 1 - x) + (1 + y) * (1 + y));

		// 0 <= x < 1
		public static string ToColor(double x)
		{
			var c = (int)(256 * Math.Clamp(x, 0, 0.999));
			return $"#{c:X2}{c:X2}{c:X2}";
		}

		public int Size => Resolution + 1;
		public Cell[] Cells { get; }
		public ReactiveProperty<Cell> SelectedCell { get; } = new ReactiveProperty<Cell>();

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

			SelectedCell.Value = Cells[0];
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

		public string Color => AppModel.ToColor(Min - AppModel.n);
	}
}
