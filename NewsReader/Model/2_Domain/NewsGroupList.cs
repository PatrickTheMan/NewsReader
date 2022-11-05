using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsReader.Model.Domain
{
	public class NewsGroupList
	{

		private List<string> _list;

		public void SetList(List<string> newsGroups)
		{
			_list = newsGroups;
		}

		public List<string> GetList()
		{
			if (_list == null)
			{
				_list = new List<string>();
			}

			return _list;
		}

	}
}
