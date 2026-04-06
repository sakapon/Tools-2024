using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;

namespace HanoiTowerWpf
{
	public class MouseDragBehavior : Behavior<FrameworkElement>
	{
		public static readonly DependencyProperty DeltaProperty =
			DependencyProperty.Register(nameof(Delta), typeof(Vector), typeof(MouseDragBehavior), new FrameworkPropertyMetadata(default(Vector), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		public Vector Delta
		{
			get { return (Vector)GetValue(DeltaProperty); }
			set { SetValue(DeltaProperty, value); }
		}

		protected override void OnAttached()
		{
			base.OnAttached();

			// アタッチされたタイミングでは構造が確定していないため、Loaded イベントで登録します。
			var fe = AssociatedObject;
			fe.Loaded += (_, _) =>
			{
				var tt = new TranslateTransform();
				var bindingX = new Binding("Delta.X") { Source = this };
				BindingOperations.SetBinding(tt, TranslateTransform.XProperty, bindingX);
				var bindingY = new Binding("Delta.Y") { Source = this };
				BindingOperations.SetBinding(tt, TranslateTransform.YProperty, bindingY);

				var transform = fe.RenderTransform;
				if (transform is not TransformGroup tg)
				{
					fe.RenderTransform = tg = new TransformGroup();
					if (transform != null) tg.Children.Add(transform);
				}
				tg.Children.Add(tt);

				var on = false;
				Vector sd;

				// e.GetPosition(null) は、Window 要素を基準としたマウスの相対位置を取得します。
				fe.MouseLeftButtonDown += (_, e) =>
				{
					if (on) return;
					sd = Delta - (Vector)e.GetPosition(null);
					on = true;
					Debug.WriteLine($"Position: {e.GetPosition(null)}");
				};
				fe.MouseMove += (_, e) =>
				{
					if (!on) return;
					Delta = sd + (Vector)e.GetPosition(null);
				};
				fe.MouseLeftButtonUp += (_, _) =>
				{
					if (!on) return;
					on = false;
					Debug.WriteLine($"MouseLeftButtonUp");
				};
				fe.MouseLeave += (_, _) =>
				{
					if (!on) return;
					on = false;
					Debug.WriteLine($"MouseLeave");
				};
			};
		}
	}
}
