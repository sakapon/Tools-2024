using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace HanoiTowerWpf201
{
	/* 
     * Usage Example
     * 
     * (1) Add the ScriptFuncConverter object to <Window.Resources> in the XAML file.
     * <local:ScriptFuncConverter x:Key="Scale3Converter" ToFunc="x => 3 * x"/>
     * 
     * (2) Set the ScriptFuncConverter object to the Binding.Converter property.
     * <TextBlock Text="{Binding Width, Converter={StaticResource Scale3Converter}}"/>
     */

	/// <summary>
	/// Represents the value converter that uses the defined functions.
	/// </summary>
	[ValueConversion(typeof(object), typeof(object))]
	public class ScriptFuncConverter : DependencyObject, IValueConverter
	{
		public static readonly DependencyProperty ToFuncProperty =
			DependencyProperty.Register(nameof(ToFunc), typeof(string), typeof(ScriptFuncConverter), new PropertyMetadata(""));

		public static readonly DependencyProperty FromFuncProperty =
			DependencyProperty.Register(nameof(FromFunc), typeof(string), typeof(ScriptFuncConverter), new PropertyMetadata(""));

		/// <summary>
		/// Gets or sets the function to use in the <see cref="Convert"/> method.
		/// </summary>
		public string ToFunc
		{
			get { return (string)GetValue(ToFuncProperty); }
			set { SetValue(ToFuncProperty, value); }
		}

		/// <summary>
		/// Gets or sets the function to use in the <see cref="ConvertBack"/> method.
		/// </summary>
		public string FromFunc
		{
			get { return (string)GetValue(FromFuncProperty); }
			set { SetValue(FromFuncProperty, value); }
		}

		/// <summary>
		/// Converts a value using the function represented by the <see cref="ToFunc"/> property.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>A converted value.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DoFunc(ToFunc, value, parameter);
		}

		/// <summary>
		/// Converts a value using the function represented by the <see cref="FromFunc"/> property.
		/// </summary>
		/// <param name="value">The value produced by the binding target.</param>
		/// <param name="targetType">The type to convert to.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>A converted value.</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DoFunc(FromFunc, value, parameter);
		}

		static object DoFunc(string script, object value, object parameter)
		{
			if (string.IsNullOrWhiteSpace(script)) return value;

			var task = CSharpScript.EvaluateAsync<Func<int, object>>(script);
			task.Wait();
			var func = task.Result;
			return func((int)value);
		}
	}
}
