namespace BfsDfsViewer
{
	public class MainViewModel
	{
		public const int Height = 9;
		public const int Width = 13;
		public const int StartId = Height * Width / 2;
		public const int TimeInterval = 100;

		public const string Color_Current = "#FF3333";
		public const string Color_Queued = "#FF9933";
		public const string Color_End = "#33AA33";

		public QueueBFS QueueBFS { get; } = new QueueBFS(Height, Width);
		public StackDFS StackDFS { get; } = new StackDFS(Height, Width);
		public RecursiveDFS RecursiveDFS { get; } = new RecursiveDFS(Height, Width);

		public MainViewModel()
		{
			Task.Run(() => QueueBFS.Execute(StartId));
			Task.Run(() => StackDFS.Execute(StartId));
			Task.Run(() => RecursiveDFS.Execute(StartId));
		}
	}
}
