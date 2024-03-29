﻿using Reactive.Bindings;

namespace BfsDfsViewer
{
	public class MainViewModel
	{
		public const int Height = 7;
		public const int Width = 9;
		public const int StartId = Height * Width / 2;

		public const int Time_Start = 800;
		public const int Time_Interval = 150;

		public const string Color_Fixed = "#FF9933";
		public const string Color_Current = "#FF3333";
		public const string Color_End = "#33AA33";

		public GridSearchBase QueueBFS { get; } = new QueueBFS(Height, Width);
		public GridSearchBase StackDFS { get; } = new StackDFS(Height, Width);
		public GridSearchBase RecursiveDFS { get; } = new RecursiveDFS(Height, Width);

		public ReactiveProperty<bool> IsReady { get; } = new ReactiveProperty<bool>(true);

		public MainViewModel()
		{
			// For design.
			//Start();
		}

		public void Start()
		{
			IsReady.Value = false;

			Task.Run(() =>
			{
				Task.WaitAll(
					Task.Run(() => QueueBFS.Execute(StartId)),
					Task.Run(() => StackDFS.Execute(StartId)),
					Task.Run(() => RecursiveDFS.Execute(StartId))
				);

				IsReady.Value = true;
			});
		}
	}
}
