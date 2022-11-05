using NewsReader.Model.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsReader.Model.Domain
{
	public class NewsObjects
	{

		private List<NewsObject> _newsObjects;

		public void SetObjects(List<NewsObject> objects)
		{
			_newsObjects = objects;
		}

		public void AddObjects(List<NewsObject> objects)
		{
			if (_newsObjects == null)
			{
				_newsObjects = new List<NewsObject>();
			}

			foreach (var item in objects)
			{
				_newsObjects.Add(item);
			}
		}

		public void CreateObjects(List<string> response)
		{
			_newsObjects = new List<NewsObject>();

			foreach (string str in response)
			{
				_newsObjects.Add(CreateObject(str));
			}

		}

		public static NewsObject CreateObject(string response)
		{
			NewsObject newsObject = new NewsObject(
				InformationExtractor.ExtractInformation(InformationExtractor.Target.Path,response),
				InformationExtractor.ExtractInformation(InformationExtractor.Target.MIMEVersion, response),
				InformationExtractor.ExtractInformation(InformationExtractor.Target.UserAgent, response),
				InformationExtractor.ExtractInformation(InformationExtractor.Target.Newsgroups, response),
				InformationExtractor.ExtractInformation(InformationExtractor.Target.ContentLanguage, response),
				InformationExtractor.ExtractInformation(InformationExtractor.Target.From, response),
				InformationExtractor.ExtractInformation(InformationExtractor.Target.Subject, response),
				InformationExtractor.ExtractInformation(InformationExtractor.Target.ContentType, response),
				InformationExtractor.ExtractInformation(InformationExtractor.Target.ContentTransferEncoding, response),
				InformationExtractor.ExtractInformation(InformationExtractor.Target.Lines, response),
				InformationExtractor.ExtractInformation(InformationExtractor.Target.MessageID, response),
				InformationExtractor.ExtractInformation(InformationExtractor.Target.XCompainsTo, response),
				InformationExtractor.ExtractInformation(InformationExtractor.Target.NNTPPostingDate, response),
				InformationExtractor.ExtractInformation(InformationExtractor.Target.Organization, response),
				InformationExtractor.ExtractInformation(InformationExtractor.Target.Date, response),
				InformationExtractor.ExtractInformation(InformationExtractor.Target.InjectionDate, response),
				InformationExtractor.ExtractInformation(InformationExtractor.Target.InjectionDate, response),
				InformationExtractor.ExtractInformation(InformationExtractor.Target.XReceived, response),
				InformationExtractor.ExtractInformation(InformationExtractor.Target.XRef, response),
				InformationExtractor.ExtractContent(response)
				);

			return newsObject;
		}

		public void ClearObjects()
		{
			if (_newsObjects != null)
			{
				_newsObjects.Clear();
			}
		}

		public List<NewsObject> GetObjects()
		{
			return _newsObjects;
		}

	}
}
