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

		public IEasingFunction Easing { get; set; }
		public double Fps { get; set; } = 60;

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
