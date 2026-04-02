using System.Text;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
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

			disk.MovedEvent.ObserveOnUIDispatcher().Subscribe(p =>
			{
				var sb = sb0.Clone();
				var framesX = (DoubleAnimationUsingKeyFrames)sb.Children[0];
				var framesY = (DoubleAnimationUsingKeyFrames)sb.Children[1];
				framesX.KeyFrames[0].Value = p.X;
				framesY.KeyFrames[0].Value = p.Y;
				sb.Begin(rect);
			});
		}
	}
}