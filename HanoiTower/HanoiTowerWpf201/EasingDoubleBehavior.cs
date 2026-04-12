using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Microsoft.Xaml.Behaviors;

namespace HanoiTowerWpf201
{
	public class EasingDoubleBehavior : Behavior<DependencyObject>
	{
		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register(nameof(Value), typeof(double), typeof(EasingDoubleBehavior), new PropertyMetadata(double.NaN, OnValueChanged));

		public static readonly DependencyProperty EasedValueProperty =
			DependencyProperty.Register(nameof(EasedValue), typeof(double), typeof(EasingDoubleBehavior), new PropertyMetadata(double.NaN));

		public static readonly DependencyProperty TimeSpanProperty =
			DependencyProperty.Register(nameof(TimeSpan), typeof(TimeSpan), typeof(EasingDoubleBehavior), new PropertyMetadata(TimeSpan.Zero));

		public static readonly DependencyProperty FpsProperty =
			DependencyProperty.Register(nameof(Fps), typeof(double), typeof(EasingDoubleBehavior), new PropertyMetadata(60.0));

		public static readonly DependencyProperty EasingProperty =
			DependencyProperty.Register(nameof(Easing), typeof(IEasingFunction), typeof(EasingDoubleBehavior), new PropertyMetadata(null));

		public double Value
		{
			get { return (double)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		public double EasedValue
		{
			get { return (double)GetValue(EasedValueProperty); }
			set { SetValue(EasedValueProperty, value); }
		}

		public TimeSpan TimeSpan
		{
			get { return (TimeSpan)GetValue(TimeSpanProperty); }
			set { SetValue(TimeSpanProperty, value); }
		}

		public double Fps
		{
			get { return (double)GetValue(FpsProperty); }
			set { SetValue(FpsProperty, value); }
		}

		public IEasingFunction Easing
		{
			get { return (IEasingFunction)GetValue(EasingProperty); }
			set { SetValue(EasingProperty, value); }
		}

		static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((EasingDoubleBehavior)d).OnValueChanged((double)e.OldValue, (double)e.NewValue);
		}

		void OnValueChanged(double oldValue, double newValue)
		{
			if (double.IsNaN(oldValue))
			{
				EasedValue = newValue;
				return;
			}

			var startTime = DateTime.Now;

			var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1 / Fps) };
			timer.Tick += (_, _) =>
			{
				var t = (DateTime.Now - startTime).TotalSeconds / TimeSpan.TotalSeconds;
				if (t >= 1)
				{
					timer.Stop();
					EasedValue = newValue;
					return;
				}
				var v = Easing?.Ease(t) ?? t;
				EasedValue = oldValue + (newValue - oldValue) * v;
			};
			timer.Start();
		}
	}
}
