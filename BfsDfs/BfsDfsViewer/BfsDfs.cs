using Reactive.Bindings;

namespace BfsDfsViewer
{
	public class Cell
	{
		public ReactiveProperty<int?> Cost { get; } = new ReactiveProperty<int?>();
		public ReactiveProperty<string> Color { get; } = new ReactiveProperty<string>();
	}

	public class QueueBFS
	{
		readonly int h, w;
		public int Height => h;
		public int Width => w;
		public Cell[] Cells { get; }

		public QueueBFS(int h, int w)
		{
			this.h = h;
			this.w = w;
			Cells = Array.ConvertAll(new bool[h * w], _ => new Cell());
		}

		public void Execute(int sv)
		{
			Thread.Sleep(MainViewModel.TimeInterval);

			var q = new Queue<int>();
			Cells[sv].Cost.Value = 0;
			q.Enqueue(sv);

			Cells[sv].Color.Value = MainViewModel.Color_Queued;
			Thread.Sleep(MainViewModel.TimeInterval);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				var nc = Cells[v].Cost.Value + 1;

				Cells[v].Color.Value = MainViewModel.Color_Current;
				//Thread.Sleep(MainViewModel.TimeInterval);

				foreach (var nv in GetNexts(v))
				{
					if (Cells[nv].Cost.Value.HasValue) continue;
					Cells[nv].Cost.Value = nc;
					q.Enqueue(nv);

					Cells[nv].Color.Value = MainViewModel.Color_Queued;
					Thread.Sleep(MainViewModel.TimeInterval);
				}

				Cells[v].Color.Value = MainViewModel.Color_End;
				Thread.Sleep(MainViewModel.TimeInterval);
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

	public class StackDFS
	{
		readonly int h, w;
		public int Height => h;
		public int Width => w;
		public Cell[] Cells { get; }

		public StackDFS(int h, int w)
		{
			this.h = h;
			this.w = w;
			Cells = Array.ConvertAll(new bool[h * w], _ => new Cell());
		}

		public void Execute(int sv)
		{
			Thread.Sleep(MainViewModel.TimeInterval);

			var q = new Stack<int>();
			Cells[sv].Cost.Value = 0;
			q.Push(sv);

			Cells[sv].Color.Value = MainViewModel.Color_Queued;
			Thread.Sleep(MainViewModel.TimeInterval);

			while (q.Count > 0)
			{
				var v = q.Pop();
				var nc = Cells[v].Cost.Value + 1;

				Cells[v].Color.Value = MainViewModel.Color_Current;
				//Thread.Sleep(MainViewModel.TimeInterval);

				foreach (var nv in GetNexts(v))
				{
					if (Cells[nv].Cost.Value.HasValue) continue;
					Cells[nv].Cost.Value = nc;
					q.Push(nv);

					Cells[nv].Color.Value = MainViewModel.Color_Queued;
					Thread.Sleep(MainViewModel.TimeInterval);
				}

				Cells[v].Color.Value = MainViewModel.Color_End;
				Thread.Sleep(MainViewModel.TimeInterval);
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
}
