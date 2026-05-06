using System;
using OpenSilver.Simulator;

namespace HanoiTowerOS201.Simulator
{
	internal static class Startup
	{
		[STAThread]
		static int Main(string[] args)
		{
			return SimulatorLauncher.Start(typeof(App));
		}
	}
}
