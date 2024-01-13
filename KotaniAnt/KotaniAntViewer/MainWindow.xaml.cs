﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KotaniAntViewer
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

		private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
		{
			var vm = (MainViewModel)DataContext;
			var cell = vm.SelectedCell.Value;
			if (cell != null) cell.UpdateSelected(false);
			cell = (Cell)((FrameworkElement)sender).DataContext;
			cell.UpdateSelected(true);
			vm.SelectedCell.Value = cell;
		}
	}
}
