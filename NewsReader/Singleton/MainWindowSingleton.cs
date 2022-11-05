
using NewsReader.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsReader.Singleton
{
	public sealed class MainViewModelSingleton : MainViewModel
	{
		private MainViewModelSingleton() { }
		private static MainViewModelSingleton instance = null;
		public static MainViewModelSingleton Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new MainViewModelSingleton();
				}
				return instance;
			}
		}
	}
}
