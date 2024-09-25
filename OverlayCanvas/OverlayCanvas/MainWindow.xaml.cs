using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OverlayCanvas
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			MouseDoubleClick += (o, e) => TheCanvas.Strokes.Clear();
			MouseRightButtonUp += (o, e) => Close();

			Loaded += (o, e) =>
			{
				var att = TheCanvas.DefaultDrawingAttributes;
				att.Width = 5;
				att.Height = 5;
				att.Color = Color.FromRgb(238, 51, 51);
			};
		}
	}
}
