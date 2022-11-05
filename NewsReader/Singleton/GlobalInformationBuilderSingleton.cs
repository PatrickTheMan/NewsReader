using NewsReader.Model.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsReader.Singleton
{
	public sealed class GlobalInformationBuilderSingleton : GlobalInformationBuilder
	{
		private GlobalInformationBuilderSingleton() { }
		private static GlobalInformationBuilderSingleton instance = null;
		public static GlobalInformationBuilderSingleton Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new GlobalInformationBuilderSingleton();
				}
				return instance;
			}
		}
	}
}
