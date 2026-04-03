using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace HanoiTowerWpf
{
	public class MouseDragBehavior : Behavior<UIElement>
	{
		public static readonly DependencyProperty DeltaProperty =
			DependencyProperty.Register("Delta", typeof(Point), typeof(MouseDragBehavior), new FrameworkPropertyMetadata(default(Point), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDeltaChanged));

		public Point Delta
		{
			get { return (Point)GetValue(DeltaProperty); }
			set { SetValue(DeltaProperty, value); }
		}

		static void OnDeltaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var behavior = (MouseDragBehavior)d;
			var ue = behavior.AssociatedObject;
			var delta = (Point)e.NewValue;
		}

		protected override void OnAttached()
		{
			base.OnAttached();
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
		}
	}
}
