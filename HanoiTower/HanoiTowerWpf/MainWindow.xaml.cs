using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Reactive.Bindings.Extensions;

namespace HanoiTowerWpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		void DiskRectangle_Loaded(object sender, RoutedEventArgs e)
		{
			var rect = (Rectangle)sender;
			var disk = (Disk)rect.DataContext;
			var sb0 = (Storyboard)rect.FindResource("MovedStoryboard");

			disk.MovedEvent.ObserveOnUIDispatcher().Subscribe(d =>
			{
				var sb = sb0.Clone();
				var framesX = (DoubleAnimationUsingKeyFrames)sb.Children[0];
				var framesY = (DoubleAnimationUsingKeyFrames)sb.Children[1];
				framesX.KeyFrames[0].Value = d.X;
				framesY.KeyFrames[0].Value = d.Y;
				framesX.KeyFrames[1].KeyTime = TimeSpan.FromSeconds(d.Y >= 0 ? 0.4 : 0.2);
				framesY.KeyFrames[1].KeyTime = TimeSpan.FromSeconds(d.Y >= 0 ? 0.2 : 0.4);
				sb.Begin(rect);
			});
		}
	}
}