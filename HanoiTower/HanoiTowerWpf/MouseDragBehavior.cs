using System.Windows;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;

namespace HanoiTowerWpf
{
	public class MouseDragBehavior : Behavior<FrameworkElement>
	{
		public static readonly DependencyProperty DeltaProperty =
			DependencyProperty.Register("Delta", typeof(Vector), typeof(MouseDragBehavior), new FrameworkPropertyMetadata(default(Vector), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDeltaChanged));

		public Vector Delta
		{
			get { return (Vector)GetValue(DeltaProperty); }
			set { SetValue(DeltaProperty, value); }
		}

		static void OnDeltaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var behavior = (MouseDragBehavior)d;
			var ue = behavior.AssociatedObject;
			var delta = (Vector)e.NewValue;
		}

		protected override void OnAttached()
		{
			base.OnAttached();

			// アタッチされたタイミングでは構造が確定していないため、Loaded イベントで登録します。
			var fe = AssociatedObject;
			fe.Loaded += (_, _) =>
			{
				var tt = (TranslateTransform)fe.FindName("DragTransform");
				tt.X = Delta.X;
				tt.Y = Delta.Y;

				var on = false;
				Vector sd;

				fe.MouseLeftButtonDown += (_, e) =>
				{
					if (on) return;
					sd = Delta - (Vector)e.GetPosition(null);
					on = true;
				};
				fe.MouseMove += (_, e) =>
				{
					if (!on) return;
					var d = sd + (Vector)e.GetPosition(null);
					tt.X = d.X;
					tt.Y = d.Y;
					Delta = d;
				};
				fe.MouseLeftButtonUp += (_, e) =>
				{
					if (!on) return;
					on = false;
				};
				fe.MouseLeave += (_, e) =>
				{
					if (!on) return;
					on = false;
				};
			};
		}
	}
}
