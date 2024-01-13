﻿using Reactive.Bindings;

namespace KotaniAntViewer
{
	public class MainViewModel
	{
		public const int Resolution = 50;
		public static readonly double[] Maxes = Enumerable.Range(0, 101).Select(n => Math.Sqrt(2 * (2L * n * n * n * n + 2L * n * n * n + 3L * n * n + 2 * n + 1)) / (2 * n)).ToArray();

		public int Size => Resolution + 1;
		public ReactiveProperty<int> N { get; } = new ReactiveProperty<int>(2);
		public Cell[] Cells { get; }
		public ReactiveProperty<Cell> SelectedCell { get; } = new ReactiveProperty<Cell>();

		public MainViewModel()
		{
			Cells = Enumerable.Range(0, Size * Size)
				.Select(v =>
				{
					var (i, j) = (v / Size, v % Size);
					var (x, y) = ((double)j / Resolution, 1 - (double)i / Resolution);
					var cell = new Cell(i, j, x, y);
					cell.Update(N.Value);
					return cell;
				})
				.ToArray();

			N.Subscribe(n => Array.ForEach(Cells, cell => cell.Update(n)));

			//SelectedCell.Value = Cells[0];
		}
	}

	public class Cell
	{
		public int Row { get; }
		public int Column { get; }
		public double X { get; }
		public double Y { get; }

		public Cell(int i, int j, double x, double y)
		{
			Row = i;
			Column = j;
			X = x;
			Y = y;
		}

		public bool IsSelected { get; private set; }
		public ReactiveProperty<double> Distance { get; } = new ReactiveProperty<double>();
		public double DistanceRate { get; private set; }
		public ReactiveProperty<string> Color { get; } = new ReactiveProperty<string>();

		public void Update(int n)
		{
			var Distance1 = X >= Y ? d1(n, X, Y) : d1(n, Y, X);
			var Distance2 = X >= Y ? d2(n, X, Y) : d2(n, Y, X);
			Distance.Value = Distance1 <= Distance2 ? Distance1 : Distance2;
			DistanceRate = (Distance.Value - n) / (MainViewModel.Maxes[n] - n);
			UpdateSelected(IsSelected);
		}

		public void UpdateSelected(bool isSelected)
		{
			IsSelected = isSelected;
			Color.Value = isSelected ? "#6699CC" : ToColor(DistanceRate);
		}

		static double d1(int n, double x, double y) => Math.Sqrt(x * x + (n + y) * (n + y));
		static double d2(int n, double x, double y) => Math.Sqrt((n + 1 - x) * (n + 1 - x) + (1 + y) * (1 + y));

		// 0 <= x < 1
		public static string ToColor(double x)
		{
			var c = (int)(256 * Math.Clamp(x, 0, 0.999));
			return $"#{c:X2}{c:X2}{c:X2}";
		}
	}
}
