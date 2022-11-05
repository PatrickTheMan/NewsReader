using NewsReader.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NewsReader.Model.Controller
{
	public abstract class AbstractGlobalInformationBuilder
	{

		// Run automaticly with RetriveInformation
		public abstract void BuildNewsGroupList();

		public abstract void BuildNewsObjects(string newsgroup);

		// Get the globalinformation
		public abstract GlobalInformation GetGlobalInformation();

	}
}
