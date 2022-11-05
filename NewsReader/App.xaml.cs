using NewsReader.Singleton;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NewsReader
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void Application_Exit(object sender, ExitEventArgs e)
		{
			if (NNTPSingleton.Instance != null)
			{
				if (NNTPSingleton.Instance.Connected)
				{
					NNTPSingleton.Instance.Disconnect();
				}
			}
		}
	}
}
