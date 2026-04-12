using System.Windows;
using System.Windows.Media.Animation;
using Microsoft.Xaml.Behaviors;

namespace HanoiTowerWpf201
{
	public class EasingDoubleBehavior : Behavior<DependencyObject>
	{
		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register(nameof(Value), typeof(double), typeof(EasingDoubleBehavior), new PropertyMetadata(double.NaN, OnValueChanged));

		public static readonly DependencyProperty EasedValueProperty =
			DependencyProperty.Register(nameof(EasedValue), typeof(double), typeof(EasingDoubleBehavior), new PropertyMetadata(double.NaN));

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

		public IEasingFunction Easing { get; set; }

		static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((EasingDoubleBehavior)d).OnValueChanged((double)e.OldValue, (double)e.NewValue);
		}

		void OnValueChanged(double oldValue, double newValue)
		{
			EasedValue = newValue;
		}
	}
}
