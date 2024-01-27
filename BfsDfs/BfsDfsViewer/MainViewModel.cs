namespace BfsDfsViewer
{
	public class MainViewModel
	{
		public const int Height = 11;
		public const int Width = 15;
		public const int StartId = Height * Width / 2;
		public const int TimeInterval = 100;

		public const string Color_Current = "#FF3333";
		public const string Color_Queued = "#FF9933";
		public const string Color_End = "#33AA33";

		public QueueBFS QueueBFS { get; } = new QueueBFS(Height, Width);
		public StackDFS StackDFS { get; } = new StackDFS(Height, Width);

		public MainViewModel()
		{
			Task.Run(() => Execute());
		}

		public void Execute()
		{
			QueueBFS.Execute(StartId);
		}
	}
}
