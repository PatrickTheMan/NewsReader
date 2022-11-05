using NewsReader.Model.Domain;
using NewsReader.Singleton;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsReader.Model.Controller
{
	public class GlobalInformationBuilder : AbstractGlobalInformationBuilder
	{

		private GlobalInformation _globalInformation;

		private NewsGroupList _newsGroupList;
		private NewsObjects _newsObjects;

		public GlobalInformationBuilder()
		{
			_globalInformation = new GlobalInformation();
		}

		public override void BuildNewsGroupList()
		{
			_newsGroupList = new NewsGroupList();

			_newsGroupList.SetList(NNTPSingleton.Instance.GetNewsgroups());

			_globalInformation.SetNewsGroupList(_newsGroupList);
		}

		public override void BuildNewsObjects(string newsgroup)
		{
			if (_newsGroupList == null)
			{
				BuildNewsGroupList();
			}

			_newsObjects = new NewsObjects();

			_newsObjects.CreateObjects(NNTPSingleton.Instance.GetNews(newsgroup));

			_globalInformation.AddNewsObjects(_newsObjects);
		}

		public override GlobalInformation GetGlobalInformation()
		{
			return _globalInformation;
		}
		
	}
}
