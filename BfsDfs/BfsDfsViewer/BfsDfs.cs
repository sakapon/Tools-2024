using Reactive.Bindings;

namespace BfsDfsViewer
{
	public class Cell
	{
		public ReactiveProperty<int?> Cost { get; } = new ReactiveProperty<int?>();
		public ReactiveProperty<string> Color { get; } = new ReactiveProperty<string>("");
	}

	public abstract class GridSearchBase
	{
		readonly int h, w;
		public int Height => h;
		public int Width => w;
		public Cell[] Cells { get; }

		public GridSearchBase(int h, int w)
		{
			this.h = h;
			this.w = w;
			Cells = Array.ConvertAll(new bool[h * w], _ => new Cell());
		}

		public void Execute(int sv)
		{
			Array.ForEach(Cells, c =>
			{
				c.Cost.Value = null;
				c.Color.Value = "";
			});
			Thread.Sleep(MainViewModel.Time_Start);

			Execute0(sv);
		}

		protected abstract void Execute0(int sv);
		protected IEnumerable<int> GetNexts(int v)
		{
			var (i, j) = (v / w, v % w);
			if (j > 0) yield return v - 1;
			if (i > 0) yield return v - w;
			if (j + 1 < w) yield return v + 1;
			if (i + 1 < h) yield return v + w;
		}
	}

	public class QueueBFS : GridSearchBase
	{
		public QueueBFS(int h, int w) : base(h, w) { }

		protected override void Execute0(int sv)
		{
			var q = new Queue<int>();
			Cells[sv].Cost.Value = 0;
			q.Enqueue(sv);

			Cells[sv].Color.Value = MainViewModel.Color_Queued;
			Thread.Sleep(MainViewModel.Time_Interval);

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
					Thread.Sleep(MainViewModel.Time_Interval);
				}

				Cells[v].Color.Value = MainViewModel.Color_End;
				Thread.Sleep(MainViewModel.Time_Interval);
			}
		}
	}

	public class StackDFS : GridSearchBase
	{
		public StackDFS(int h, int w) : base(h, w) { }

		protected override void Execute0(int sv)
		{
			var q = new Stack<int>();
			Cells[sv].Cost.Value = 0;
			q.Push(sv);

			Cells[sv].Color.Value = MainViewModel.Color_Queued;
			Thread.Sleep(MainViewModel.Time_Interval);

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
					Thread.Sleep(MainViewModel.Time_Interval);
				}

				Cells[v].Color.Value = MainViewModel.Color_End;
				Thread.Sleep(MainViewModel.Time_Interval);
			}
		}
	}

	public class RecursiveDFS : GridSearchBase
	{
		public RecursiveDFS(int h, int w) : base(h, w) { }

		protected override void Execute0(int sv)
		{
			Cells[sv].Cost.Value = 0;
			DFS(sv);

			void DFS(int v)
			{
				var nc = Cells[v].Cost.Value + 1;

				Cells[v].Color.Value = MainViewModel.Color_Current;
				Thread.Sleep(MainViewModel.Time_Interval);

				foreach (var nv in GetNexts(v))
				{
					if (Cells[nv].Cost.Value.HasValue) continue;
					Cells[nv].Cost.Value = nc;
					DFS(nv);
				}

				Cells[v].Color.Value = MainViewModel.Color_End;
				Thread.Sleep(MainViewModel.Time_Interval);
			}
		}
	}
}
