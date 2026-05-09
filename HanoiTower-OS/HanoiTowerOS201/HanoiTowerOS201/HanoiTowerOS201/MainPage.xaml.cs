using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HanoiTowerOS201
{
	public partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();

			// Enter construction logic here...
		}
	}

	public static class ViewFuncs
	{
		public static readonly Func<int, int> Disk_Id_Width = id => id * 30 + 10;
		public static readonly Func<int, int> Disk_TowerId_X = id => id * 290;
		public static readonly Func<int, int> Disk_Index_Y = i => (i + 1) * -32;
	}
}
