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

				fe.MouseLeftButtonDown += (_, e) =>
				{
					var p = e.GetPosition(null);
				};
				fe.MouseMove += (_, e) =>
				{
					var p = e.GetPosition(null);
				};
				fe.MouseLeftButtonUp += (_, e) =>
				{
				};
				fe.MouseLeave += (_, e) =>
				{
				};
			};
		}
	}
}
