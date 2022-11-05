using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsReader.Model.Domain
{
	public class GlobalInformation
	{

		private NewsGroupList _newsGroupList;
		private NewsObjects _newsObjects;

		private NewsGroupList _selectedNewsGroupList;
		private NewsGroupList _favoritNewsGroupList;

		public GlobalInformation()
		{
			_newsGroupList = new NewsGroupList();
			_newsObjects = new NewsObjects();
			_selectedNewsGroupList = new NewsGroupList();

			_favoritNewsGroupList = new NewsGroupList();
		}

		public void SetNewsObjects(NewsObjects newsObjects)
		{
			_newsObjects = newsObjects;
		}

		public void AddNewsObjects(NewsObjects newsObjects)
		{
			_newsObjects.AddObjects(newsObjects.GetObjects());
		}

		public NewsObjects GetNewsObjects()
		{
			return _newsObjects;
		}

		public void SetNewsGroupList(NewsGroupList newsGroupList)
		{
			_newsGroupList = newsGroupList;
		}

		public NewsGroupList GetNewsGroupList()
		{
			return _newsGroupList;
		}

		public void SetSelectedNewsGroupList(NewsGroupList selectedNewsGroupList)
		{
			_selectedNewsGroupList = selectedNewsGroupList;
		}

		public NewsGroupList GetSelectedNewsGroupList()
		{
			return _selectedNewsGroupList;
		}

		public void SetFavoritNewsGroupList(NewsGroupList favoritNewsGroupList)
		{
			_favoritNewsGroupList = favoritNewsGroupList;
		}

		public NewsGroupList GetFavoritNewsGroupList()
		{
			return _favoritNewsGroupList;
		}

	}
}
