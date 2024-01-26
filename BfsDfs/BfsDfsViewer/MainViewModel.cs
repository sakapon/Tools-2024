using Reactive.Bindings;

namespace BfsDfsViewer
{
	public class MainViewModel
	{
		public const int Height = 15;
		public const int Width = 25;
		public const int Start = Height * Width / 2;
		public const int TimeInterval = 100;

		public const string Color_Queued = "#3399FF";
		public const string Color_End = "#FF9933";

		public BFS BFS { get; } = new BFS(Height, Width);

		public void Execute()
		{
			BFS.Execute(Start);
		}
	}

	public class BFS
	{
		readonly int h, w;
		public Cell[] Cells { get; }

		public BFS(int h, int w)
		{
			this.h = h;
			this.w = w;
			Cells = Enumerable.Range(0, h * w).Select(v => new Cell()).ToArray();
		}

		public void Execute(int sv)
		{
			Thread.Sleep(MainViewModel.TimeInterval);

			var q = new Queue<int>();
			Cells[sv].Cost.Value = 0;
			Cells[sv].Color.Value = MainViewModel.Color_Queued;
			q.Enqueue(sv);
			Thread.Sleep(MainViewModel.TimeInterval);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				Cells[v].Color.Value = MainViewModel.Color_End;
				Thread.Sleep(MainViewModel.TimeInterval);

				var nc = Cells[v].Cost.Value + 1;
				foreach (var nv in GetNexts(v))
				{
					if (Cells[nv].Cost.Value.HasValue) continue;
					Cells[nv].Cost.Value = nc;
					Cells[nv].Color.Value = MainViewModel.Color_Queued;
					q.Enqueue(nv);
					Thread.Sleep(MainViewModel.TimeInterval);
				}
			}
		}

		IEnumerable<int> GetNexts(int v)
		{
			var (i, j) = (v / w, v % w);
			if (j > 0) yield return v - 1;
			if (j + 1 < w) yield return v + 1;
			if (i > 0) yield return v - w;
			if (i + 1 < h) yield return v + w;
		}
	}

	public class Cell
	{
		public ReactiveProperty<int?> Cost { get; } = new ReactiveProperty<int?>();
		public ReactiveProperty<string> Color { get; } = new ReactiveProperty<string>();
	}
}
